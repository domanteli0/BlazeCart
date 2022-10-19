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
        public override void scrape()
        {
            init();
            UpdateAllItems();
        }

        /// <summary>
        /// Collects all necessary data needed to get Item data
        ///
        /// NOTE: Deletes previously collected data
        /// </summary>
        public void init()
        {
            hardReset();

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

            // Gets the list of stores from initial_data JSON
            foreach (JToken cat_ in JSONresponseInit["chains"]!.Last!["stores"]!)
            {
                var temp = new Store
                {
                    Merch = Store.Merchendise.IKI,
                    InternalID = cat_["id"]!.ToString(),
                    Name = cat_["name"]!.ToString(),
                    Address = cat_["streetAndBuilding"]!.ToString(),
                    Longitude = cat_["location"]!["geopoint"]!["longitude"]!.ToString(),
                    Latitude = cat_["location"]!["geopoint"]!["latitude"]!.ToString(),
                };
                Stores.Add(temp);
            }


            // Gets needed category data from initial_data JSON
            // toCat is declared and assigned at different points to do recurtion, see:
            // https://stackoverflow.com/questions/4611549/recursion-with-func
            Func<JToken, List<Category>> toCat = null!;
            toCat = jtoken =>
            {
                var l = new List<Category>();

                foreach (var cat in jtoken)
                {
                    var listing = new Category()
                    {
                        InternalID = cat["id"]!.ToString(),
                        NameLT = cat["name"]!["lt"]!.ToString(),
                        NameEN = cat["name"]!["en"]!.ToString(),
                    };

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

                    l.Add(listing);
                    AllCategories.Add(listing);

                    var cat_ = cat["subcategories"]!;
                    if (cat_.Count() > 0)
                    {
                        listing.SubCategories = toCat(cat_);
                    }
                    else
                        Categories.Add(listing);
                }

                return l;
            };

            toCat(JSONresponseInit.GetValue("categories")!);
        }

        /// <summary>
        /// Updates all items
        /// 
        /// WARNING: init() must be executed at least once before calling storeData()
        /// </summary>
        // TODO: ensure programatically that init() is called at least once
        // TODO?: Instead of checking for changes, rebuild all data, might be faster
        public void UpdateAllItems(int requestDelay = 10)
        {
            foreach (var cat in Categories)
            {
                try
                {
                    FetchItems(categoryID: cat.InternalID).ForEach( e =>
                    {
                        Items.UpdateOrAddByProperty(e, "InternalID");
                    });
                } catch (Newtonsoft.Json.JsonReaderException)
                {
                    // TODO: Proper logging
                    Console.WriteLine("ERROR");
                    continue;
                }
            }
        }

        // TODO: Convert to Task for async?
        // TODO?: Turn into IEnumerable
        /// <summary>
        /// Performs a reqest for specified categoryID or storeID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Currently only scraping by categoryID is supported</exception>
        /// <exception cref="ArgumentException">Gets thrown if both categoryId and storeID are null</exception>
        private List<Item> FetchItems(string categoryID = null, string storeID = null, int requestDelay = 10)
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

            var itemList = new List<Item>();

            foreach (var jtoken in JSONresponse["data"]!)
            {
                // This is done to prevent exesive requests and getting blocked
                Thread.Sleep(requestDelay);

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
                    // TODO:
                    // * Referencing items with `Store`, `Category` instances and vice versa (Back-referencing)
                    // Units of measure and available ammounts
                };

                var bar = jtoken["barcodes"]!.ToObject<List<String>>();
                if (bar is not null)
                    newItem.Barcodes = bar;

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

                itemList.AddAsSetByProperty(newItem, "InternalID");
            }

            return itemList;
        }

    }
}

