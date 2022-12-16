using System.Net.Http;
using System.Net.Http.Headers;
using Models;
using Common;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;

namespace Scraper
{

    public class IKIScraper : Scraper
    {
        private protected override Merchendise.Merch _merch { get { return Merchendise.Merch.IKI; } }
        // Fields which are thought (as of when implemented) to always be non-null
        // are marked with null-forgiving operators (!)
        // If a field is thought to be able to be null,
        // then are marked with null-conditional operator (?)

        // TODO: Sometimes there are duplicate products.
        // However some of them a price of 0,
        // therefore if a duplicate with a price of non-zero is found
        // the price should be updated
        // TODO:
        // * Referencing items with `Store`, `Category` instances and vice versa (Back-referencing)
        // Units of measurement and available ammounts

        public IKIScraper(HttpClient httpClient, ILogger<Scraper> logger) : base(httpClient, logger) { }

        /// <summary>
        /// Gets all data
        ///
        /// NOTE: Deletes previously collected data
        /// </summary>
        // TODO: Scrape Store data
        public override async Task Scrape()
        {
            await GetCategories();
            await RefetchAllItems();
            setMerch();
        }

        public async Task GetCategories()
        {
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/search/categories"))
            {
                request.Content = new StringContent("{\"params\":{\"type\":\"categories\",\"show\":true},\"slim\":true}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                response = await _httpClient.SendAsync(request);
            }
            
            StreamReader readerInit = new StreamReader(response.Content.ReadAsStream());
            var str = readerInit.ReadToEnd();

            var JSONresponseInit = JObject.Parse(str);

            //_logger.LogInformation(JSONresponseInit.GetValue("data")!.ToString());

            Categories = ParseCategoriesJSON(JSONresponseInit.GetValue("data")!).ToList();
        }

        /// <summary>
        /// Refetches all item data
        /// 
        /// WARNING: init() must be executed at least once before calling RefetchAllItems()
        /// NOTE: This method rebuilds Items list
        /// </summary>
        private async Task RefetchAllItems()
        {
            Items = new List<Item>();
            foreach (var cat in Categories.GetWithoutChildren())
            {
                _logger.LogInformation(cat.ToString());
                foreach (var i in await GetItemsBy(category: cat))
                {
                    _logger.LogInformation(i.ToString());
                    Items.AddAsSetByProperty(i, "InternalID");
                    cat.Items.AddAsSetByProperty(i, "InternalID");
                }
                break;
            }
        }

        private async Task UpdateItemsBy(Category? category = null, string? storeID = null)
        {
            foreach (var item in await GetItemsBy(category, storeID))
            {
                Items.UpdateOrAddByProperty(item, "InternalID");
            } 
        }

        /// <summary>
        /// Performs a reqest for specified categoryID or storeID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Currently only scraping by categoryID is supported</exception>
        /// <exception cref="ArgumentException">Gets thrown if both categoryId and storeID are null</exception>
        private async Task<IEnumerable<Item>> GetItemsBy(Category? category = null, string? storeID = null)
        {
            var request = new HttpRequestMessage(
                new HttpMethod("POST")
                , "https://eparduotuve.iki.lt/api/search/view_products"
            );


            request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
            request.Headers.TryAddWithoutValidation("Host", "eparduotuve.iki.lt");

            if (category is not null)
            {
                request.Content = new StringContent(
                    "{\"limit\":1000,\"params\":{\"type\":\"view_products\",\"categoryIds\":[\"" + category.InternalID + "\"],\"filter\":{}}}");
            }
            // TODO: Implement these
            else if (storeID is not null)
                throw new NotImplementedException("Fetching items by storeID is not yet supported");
            else if (storeID is not null && category is not null)
                throw new NotImplementedException("Fetching items by storeID and categoryID is not yet supported");
            else
                throw new ArgumentException("Either categoryID or storeID must be not null");

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await _httpClient.SendAsync(request);

            _logger.LogInformation($"Sent request to {request.RequestUri}");

            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            JObject JSONresponse = JObject.Parse(reader.ReadToEnd());

            var ret = ParseItemsJSON(JSONresponse, category);
            return ret;
        }

        /// <summary>
        /// Note: this method mutates Stores property
        /// </summary>
        private IEnumerable<Category> ParseCategoriesJSON(JToken data, Category? parent = null)
        {
            foreach (var cat in data)
            {
                var listing = new Category() {
                    NameEN = cat["name"]!["en"]!.ToString(),
                    InternalID = cat["id"]!.ToString(),
                    NameLT = cat["name"]!["lt"]!.ToString(),
                };

                var cat_ = cat["subcategories"]!;
                if (cat_.Count() > 0)
                    listing.SubCategories = ParseCategoriesJSON(cat_, listing).ToList();


                // Not all stores are described in chains/stores JSON list
                // thus IDs of other stores must be collected at this step
                foreach (var storeId in cat["storeIds"]!)
                {
                    Stores.AddAsSetByProperty(
                        new Store() { InternalID = storeId.ToString() },
                        "InternalID"
                    );
                }

                yield return listing;
            }
        }

        private IEnumerable<Store> ParseStoresJSON(JObject data)
        {
            foreach (JToken cat_ in data["chains"]!.Last!["stores"]!)
            {
                var newStore = new Store()
                {
                    InternalID = cat_["id"]!.ToString(),
                    Name = cat_["name"]!.ToString(),
                    Address = cat_["streetAndBuilding"]!.ToString(),
                    Longitude = cat_["location"]!["geopoint"]!["longitude"]!.ToString(),
                    Latitude = cat_["location"]!["geopoint"]!["latitude"]!.ToString(),
                };
                yield return newStore;
            }
        }

        private IEnumerable<Item> ParseItemsJSON(JObject data, Category category)
        {
            List<Item> ret = new();
            foreach (var jtoken in data["data"]!)
            {
                if (
                    jtoken["prc"] is null
                    || jtoken["conversionValue"] is null
                    || jtoken["name"] is null
                    || jtoken["id"] is null
                    || jtoken["description"] is null
                    || jtoken["description"]!["lt"] is null
                )
                {
                    continue;
                }

                Uri? photo = null;
                try
                {
                    photo = jtoken["photoUrl"]?.ToObject<Uri>();
                }
                catch (System.UriFormatException) { }

                if (photo is null)
                {
                    try
                    {
                        photo = jtoken["thumbUrl"]?.ToObject<Uri>();
                    }
                    catch (System.UriFormatException) { }
                }

                try
                {
                    var newItem = new Item(internalID: jtoken["id"]!.ToString())
                    {
                        NameLT = jtoken["name"]!["lt"]!.ToString(),
                        Price = (int)(jtoken["prc"]!["p"]!.ToObject<float>() * 100),
                        Image = photo,
                        MeasureUnit = Item.ParseUnitOfMeasurement(jtoken["conversionMeasure"]!.ToString()),
                        NameEN = jtoken["name"]!["en"]!.ToString(),
                        Description = jtoken["description"]?["lt"]?.ToString(),
                        DiscountPrice = (int?)(jtoken["prc"]!["s"]?.ToObject<float?>() * 100),
                        LoyaltyPrice = (int?)(jtoken["prc"]!["l"]?.ToObject<decimal?>() * 100),
                        Ammount = jtoken["conversionValue"]!.ToObject<float>(),
                        Category = category,

                        //barcodes: jtoken["barcodes"]!.ToObject<List<String>>()
                    };

                    newItem.FillPerUnitOfMeasureByAmmount();

                    ret.Add(newItem);
                }
                catch (System.ArgumentException) { }
                catch (System.InvalidOperationException) { }
                catch (System.NullReferenceException) { }

            }

            return ret;
        }
    }
}

