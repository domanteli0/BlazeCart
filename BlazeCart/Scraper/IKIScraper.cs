using System.Net.Http;
using System.Net.Http.Headers;
using Models;
using Common;
using static Common.CollectionExtensions;
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

        // TODO:
        // * Referencing items with `Store`, `Category` instances and vice versa (Back-referencing)
        // * Units of measurement and available ammounts

        private HttpSender<JObject> _httpSender;

        public IKIScraper(HttpClient httpClient, ILogger<Scraper> logger) : base(httpClient, logger) 
        {
            _httpSender = new (_httpClient, (response) => {
                StreamReader readerInit = new StreamReader(response.Content.ReadAsStream());
                var str = readerInit.ReadToEnd();

                return JObject.Parse(str);
            }); 
         }

        /// <summary>
        /// Gets all data
        ///
        /// NOTE: Deletes previously collected data
        /// </summary>
        // TODO: Scrape Store data
        public override async Task Scrape()
        {
            clean();
            Categories.AddRange(await GetCategories());
            Items.AddRange(RefetchAllItems(Categories).ToList());
            setMerch();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/search/categories");
            request.Content = new StringContent("{\"params\":{\"type\":\"categories\",\"show\":true},\"slim\":true}");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            var JSONresponseInit = await _httpSender.SendAsync(request);

            return ParseCategoriesJSON(JSONresponseInit.GetValue("data")!);
        }

        /// <summary>
        /// Refetches all item data
        /// </summary>
        // TODO: Convert to PLINQ and remove blocking `.Result` call
        private List<Item> RefetchAllItems(IEnumerable<Category> categories)
        {
            return categories
                .GetWithoutChildren()
                // #if DEBUG
                // .Take(1)
                // #endif
                .SelectMany(cat => {
                    _logger.LogInformation(cat.ToString());
                    
                    var cat_items = GetItemsBy(category: cat).Result.ToList();
                    cat.Items.AddRange(cat_items);
                    return cat_items;
                })
                .Where((Item item) => !item.Price.Equals(0))
                .DistinctBy((Item item) => { return item.InternalID; })
                .ToList(); // ToList forces evaluation of a lazy iterator
        }
 
        private async Task UpdateItemsBy(Category? category = null, string? storeID = null)
        {
            Items = (await GetItemsBy(category, storeID))
                .Concat(Items)
                .Where((Item item) => !item.Price.Equals(0))
                .DistinctBy(item => item.InternalID)
                .ToList();
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

            JObject JSONresponse = await _httpSender.SendAsync(request);

            return ParseItemsJSON(JSONresponse, category);
        }

        /// <summary>
        /// Note: this method mutates Stores property
        /// </summary>
        private IEnumerable<Category> ParseCategoriesJSON(JToken data, Category? parent = null)
        {
            // Not all stores are described in chains/stores JSON list
            // thus IDs of other stores must be collected at this step
            Stores = data
                .SelectMany(cat => cat["storeIds"]!)
                .Select(storeId => new Store() { InternalID = storeId.ToString() })
                .Concat(Stores)
                .GroupBy(store => store.InternalID)
                .Select(grp => {
                    var fst = grp.First();
                    return grp.FindFirstOr((s) => s.Name is not null, fst);
                })
                .ToList();

            // Parses category data
            foreach (var cat in data) 
            {
                var listing = new Category() {
                    NameEN = cat["name"]!["en"]!.ToString(),
                    InternalID = cat["id"]!.ToString(),
                    NameLT = cat["name"]!["lt"]!.ToString(),
                };

                // Check for categories recursively
                var cat_ = cat["subcategories"]!;
                if (cat_.Count() > 0)
                    listing.SubCategories = ParseCategoriesJSON(cat_, listing).ToList();
                
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
            return data["data"]!
                .Where(IsValidToken)
                .TrySelect<Exception, JToken, Item>(jtoken => {
                    var newItem = new Item(internalID: jtoken["id"]!.ToString())
                    {
                        NameLT = jtoken["name"]!["lt"]!.ToString(),
                        Price = (int)(jtoken["prc"]!["p"]!.ToObject<float>() * 100),
                        Image = jtoken["photoUrl"]?.ToObject<Uri>(),
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
                    return newItem;
                });
        }

        private bool IsValidToken(JToken jtoken)
        {
            if (
                jtoken["prc"] is null
                || jtoken["conversionValue"] is null
                || jtoken["name"] is null
                || jtoken["id"] is null
                || jtoken["description"] is null
                || jtoken["description"]!["lt"] is null
                || jtoken["name"] is null
            )
            {
                return false;
            }

            try
            {
                jtoken["thumbUrl"]?.ToObject<Uri>();
                jtoken["photoUrl"]?.ToObject<Uri>();
            }
            catch (System.UriFormatException) { return false; }

            return true;
        }
    }
}

