using System;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace Scraper
{
    public class IKIScraper : Scraper
    {
        // Fields which are thought (as of when implemented) to always be non-null
        // are marked with null-forgiving operators (!)
        // If a field is thought to be able to be null,
        // then are marked with null-conditional operator (?)

        public IKIScraper() : base() { }

        /// <summary>
        /// Gets all data
        ///
        /// NOTE: Deletes previously collected data
        /// </summary>
        public override void Scrape()
        {
            Init();
            RefetchAllItems();
        }

        /// <summary>
        /// Collects all necessary data needed to get Item data
        ///
        /// NOTE: Deletes previously collected data
        /// </summary>
        public void Init()
        {
            // Gets the initial data JSON
            HttpResponseMessage responseInit;

            using (var requestInit = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/initial_data"))
            {
                requestInit.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                requestInit.Content = new StringContent("{\"locale\":\"lt\"}");
                requestInit.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                responseInit = httpClient.Send(requestInit);
            }
            StreamReader readerInit = new StreamReader(responseInit.Content.ReadAsStream());
            var JSONresponseInit = JObject.Parse(readerInit.ReadToEnd());

            Stores = ParseStoresJSON(JSONresponseInit).ToList();
            Categories = new List<Category>();
            AllCategories = ParseCategoriesJSON(JSONresponseInit.GetValue("categories")!).ToList();
        }

        /// <summary>
        /// Refetches all item data
        /// 
        /// WARNING: init() must be executed at least once before calling RefetchAllItems()
        /// NOTE: This method rebuilds Items list
        /// </summary>
        /// <param name="requestDelay">Specifies reqests</param>
        // TODO: ensure programatically that init() is called at least once
        public void RefetchAllItems(int requestDelay = 10)
        {
            Items = new List<Item>();
            foreach (var cat in Categories)
            {
                Thread.Sleep(requestDelay);
                foreach (var i in GetItemsBy(categoryID: cat.InternalID))
                {
                    try
                    {   
                        Items.AddAsSetByProperty(i, "InternalID");
                    }
                    catch (Newtonsoft.Json.JsonReaderException)
                    {
                        // TODO: Proper logging
                        Console.WriteLine("ERROR");
                    }
                }
            }
        }

        public void UpdateItemsBy(string categoryID = null, string storeID = null)
        {
            foreach (var item in GetItemsBy(categoryID, storeID))
                Items.UpdateOrAddByProperty(item, "InternalID");

        }

        // TODO: Convert to Task for async?
        /// <summary>
        /// Performs a reqest for specified categoryID or storeID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Currently only scraping by categoryID is supported</exception>
        /// <exception cref="ArgumentException">Gets thrown if both categoryId and storeID are null</exception>
        private IEnumerable<Item> GetItemsBy(string categoryID = null, string storeID = null)
        {
            var request = new HttpRequestMessage(
                    new HttpMethod("POST"),
                    "https://eparduotuve.iki.lt/api/search/view_products");

            if (categoryID is not null)
            {
                request.Content =
                    new StringContent("{\"limit\":60,\"params\":{\"type\":\"view_products\",\"categoryIds\":[\""
                    + categoryID +
                    "\"],\"filter\":{}}}");

            }
            // TODO: Implement these
            else if (storeID is not null)
                throw new NotImplementedException("Fetching items by storeID is not yet supported");
            else if (storeID is not null && categoryID is not null)
                throw new NotImplementedException("Fetching items by storeID and categoryID is not yet supported");
            else
                throw new ArgumentException("Either categoryID or storeID must be not null");

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = this.httpClient.Send(request);

            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            JObject JSONresponse = JObject.Parse(reader.ReadToEnd());            

            return ParseItemsJSON(JSONresponse);
        }

        // Note: this method mutates Categories, Stores properties
        private IEnumerable<Category> ParseCategoriesJSON(JToken data)
        {
            foreach (var cat in data)
            {
                var listing = new Category()
                {
                    InternalID = cat["id"]!.ToString(),
                    NameLT = cat["name"]!["lt"]!.ToString(),
                    NameEN = cat["name"]!["en"]!.ToString(),
                };

                var cat_ = cat["subcategories"]!;
                if (cat_.Count() > 0)
                {
                    listing.SubCategories = ParseCategoriesJSON(cat_).ToList();
                }
                else
                    Categories.Add(listing);


                // Not all stores are described in chains/stores JSON list
                // thus IDs of other stores must be collected at this step
                foreach (var storeId in cat["storeIds"]!)
                {
                    String id = storeId.ToString();
                    Stores.AddAsSetByProperty(new Store()
                    {
                        Merch = Store.Merchendise.IKI,
                        InternalID = id,
                    }, "InternalID");
                }

                yield return listing;
            }
        }

        private IEnumerable<Store> ParseStoresJSON(JObject data)
        {
            foreach (JToken cat_ in data["chains"]!.Last!["stores"]!)
            {
                var newStore = new Store
                {
                    Merch = Store.Merchendise.IKI,
                    InternalID = cat_["id"]!.ToString(),
                    Name = cat_["name"]!.ToString(),
                    Address = cat_["streetAndBuilding"]!.ToString(),
                    Longitude = cat_["location"]!["geopoint"]!["longitude"]!.ToString(),
                    Latitude = cat_["location"]!["geopoint"]!["latitude"]!.ToString(),
                };
                yield return newStore;
            }
        }

        private IEnumerable<Item> ParseItemsJSON(JObject data)
        {
            foreach (var jtoken in data["data"]!)
            {
                var newItem = new Item()
                {
                    InternalID = jtoken["id"]!.ToString(),
                    NameLT = jtoken["name"]!["lt"]!.ToString(),
                    NameEN = jtoken["name"]!["en"]!.ToString(),
                    Description = jtoken["description"]?["lt"]?.ToString(),
                    // TODO: Sometimes there are duplicate products.
                    // However some of them a price of 0,
                    // therefore if a duplicate with a price of non-zero is found
                    // the price should be updated
                    Price = ((int)jtoken["prc"]!["p"]!.ToObject<float>() * 100),
                    DiscountPrice = (int?)jtoken["prc"]!["s"]?.ToObject<float?>() * 100,
                    LoyaltyPrice = (int?)jtoken["prc"]!["l"]?.ToObject<decimal?>() * 100,
                    Barcodes = jtoken["barcodes"]!.ToObject<List<String>>(),
                    // TODO:
                    // * Referencing items with `Store`, `Category` instances and vice versa (Back-referencing)
                    // Units of measurement and available ammounts
                };

                try
                {
                    var photo = jtoken["photoUrl"]?.ToObject<Uri>();
                    if (photo is not null)
                        newItem.Images.Add(photo);
                }
                catch (System.UriFormatException) { }

                try
                {
                    var thumb = jtoken["thumbUrl"]?.ToObject<Uri>();
                    if (thumb is not null)
                        newItem.Images.Add(thumb);
                }
                catch (System.UriFormatException) { }

                yield return newItem;
            }
        }
    }
}

