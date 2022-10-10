using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Scraper
{
    internal class Program
    {
        // TODO:
        // * Refactor, currently this is more in a PoC stage
        //   * Error handling (remove all null-forgiving operators?)
        // * Back referencing products with `Store` instance
        //
        // Fields which are thought (as of when implemented) to always be non-null
        // are marked with null-forgiving operators (!)
        // If a field is thought to be able to be null,
        // then are marked with null-conditional operator (?)

        static async Task Main(string[] args)
        {
            var storeList = new List<Store>();

            #region initial_data
            // Gets the initial data JSON
            HttpResponseMessage responseInit;

            // IMPORTANT:
            // Genereted with <https://curl.olsh.me/>, slightly edited
            using (var httpClient = new HttpClient())
            {
                using (var requestInit = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/initial_data"))
                {
                    requestInit.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                    requestInit.Content = new StringContent("{\"locale\":\"lt\"}");
                    requestInit.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    responseInit = await httpClient.SendAsync(requestInit);
                }
            }

            StreamReader readerInit = new StreamReader( responseInit.Content.ReadAsStream() );
            var JSONresponseInit = JObject.Parse(readerInit.ReadToEnd());

            #endregion

            #region stores
            // Gets the list of stores from initial_data JSON
            foreach (JToken cat_ in JSONresponseInit["chains"]!.Last!["stores"]!)
            {
                storeList.Add(new Store {
                    Merch = Store.Merchendise.IKI,
                    InternalID = cat_["id"]!.ToString(),
                    Name = cat_["name"]!.ToString(),
                    Address = cat_["streetAndBuilding"]!.ToString(),
                    Longitude = cat_["location"]!["geopoint"]!["longitude"]!.ToString(),
                    Latitude = cat_["location"]!["geopoint"]!["latitude"]!.ToString(),
                    }
                );
            }
            Console.WriteLine("BEFORE: {0}", storeList.Count);
            // storeList.ForEach(store => Console.WriteLine(store));
            #endregion

            #region categories
            // Gets needed category data from initial_data JSON
            // All Catagories, including those who have subcategories
            var catListFull = new List<Category>();
            // Only Categories which don't have subcategories;
            var catListDeepestOnly = new List<Category>();

            // toCat is declared and assigned at different points to do recurtion, see:
            // https://stackoverflow.com/questions/4611549/recursion-with-func
            Func<JToken, List<Category>> toCat = null!;
            toCat = jtoken => {
                var l = new List<Category>();

                foreach(var cat in jtoken)
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
                        if(storeList.Any( s => { return s.InternalID.Equals(id); } ))
                            storeList.Add(
                                new Store() {
                                    Merch = Store.Merchendise.IKI,
                                    InternalID = id,
                                }
                            );
                    }

                    l.Add(listing);
                    catListFull.Add(listing);

                    var cat_ = cat["subcategories"]!;
                    if (cat_.Count() > 0)
                    {
                        listing.SubCategories = toCat(cat_);
                    }
                    else
                        catListDeepestOnly.Add(listing);
                }

                return l;
            };

            // storeList

            foreach(var store in storeList)
            {
                Debug.Assert(storeList.FindAll( s => s.InternalID.Equals(store.InternalID)).Count == 1);
            }

            toCat(JSONresponseInit.GetValue("categories")!);
            Console.WriteLine("AFTER: {0}", storeList.Count);

            Console.WriteLine("Full Only: {0}", catListFull.Count);
            // catListFull.ForEach(cat => Console.WriteLine(cat));
            Console.WriteLine("Deepest Only: {0}", catListDeepestOnly.Count);
            // catListDeepestOnly.ForEach(cat => Console.WriteLine(cat));
            #endregion

            #region Products

            var productList = new List<Procuct>();
            // Autogenerated with <https://curl.olsh.me/>, slightly modified
            // curl 'https://eparduotuve.iki.lt/api/search/view_products' \
            // -H 'content-type: application/json' \
            // --data-raw '{"limit":6,"params":{"type":"view_products","isActive":true,"storeIds":["CvKfTzV4TN5U8BTMF1Hl_408"]}}'
            // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                foreach (var cat in catListDeepestOnly)
                {
                    var request = new HttpRequestMessage(
                        new HttpMethod("POST"),
                        "https://eparduotuve.iki.lt/api/search/view_products");
                    request.Content =
                        new StringContent("{\"limit\":60,\"params\":{\"type\":\"view_products\",\"categoryIds\":[\""
                        + cat.InternalID +
                        "\"],\"filter\":{}}}");
                        // new StringContent("{\"limit\":60,\"params\":{\"type\":\"view_products\",\"categoryIds\":[\""
                        // + "gaIG5IdjVi36hwBvDkCX" +
                        // "\"],\"filter\":{}}}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    StreamReader reader = new StreamReader( response.Content.ReadAsStream() );
                    var str = reader.ReadToEnd();
                    JObject JSONresponseProd;
                    try {
                        JSONresponseProd = JObject.Parse(str);
                    } catch (Newtonsoft.Json.JsonReaderException) {
                        // TODO: Logging
                        continue;
                    }

                    foreach(var jtoken in JSONresponseProd["data"]!)
                    {
                        // Console.WriteLine("---");
                        // Console.WriteLine(jtoken);

                        // This is done to prevent exesive request and getting blocked
                        Thread.Sleep(10);

                        var newProd = new Procuct() {
                            InternalID = jtoken["id"]!.ToString(),
                            NameLT = jtoken["name"]!["lt"]!.ToString(),
                            NameEN = jtoken["name"]!["en"]!.ToString(),
                            Description = jtoken["description"]?["lt"]?.ToString(),
                            Price = (jtoken["prc"]!["p"]!.ToObject<decimal>()),
                            DiscountPrice = jtoken["prc"]!["s"]?.ToObject<decimal?>(),
                            LoyaltyPrice = jtoken["prc"]!["l"]?.ToObject<decimal?>(),
                            // TODO:
                            // Categories =
                            // Units of measure and available ammounts
                        };

                        var bar = jtoken["barcodes"]!.ToObject<List<String>>();
                        if (bar is not null)
                            newProd.Barcodes = bar;

                        try {
                            var photo = jtoken["photoUrl"]?.ToObject<Uri>();
                            if (photo is not null)
                                newProd.Images.Add(photo);
                        } catch (System.UriFormatException) {}

                        try {
                            var thumb = jtoken["thumbUrl"]?.ToObject<Uri>();
                            if (thumb is not null)
                                newProd.Images.Add(thumb);
                        } catch (System.UriFormatException) {}

                        productList.Add(newProd);
                    }
                }
            }

            productList.ForEach(p => Console.WriteLine(p));
            #endregion
        }

        public class Store
        {
            // TODO?: Use inheritance instead of this enum
            public enum Merchendise { IKI, MAXIMA}
            public string? Name { get; set;}
            public string? Address { get; set;}
            public string? Latitude { get; set;}
            public string? Longitude { get; set; }
            public Merchendise Merch { get; set; }
            public string InternalID { get; set; }

            // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
            public override string ToString()
            {
                return GetType().GetProperties()
                    .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                    .Aggregate(
                        new StringBuilder(),
                        (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                        sb => sb.ToString());
            }
        }

        // TODO: Subcategories, storeIds, chainIds
        public class Category
        {
            public string? InternalID { get; set; }
            public string? NameEN { get; set; }
            public string? NameLT { get; set; }
            public List<Category>? SubCategories { get; set; }

            // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
            public override string ToString()
            {
                return GetType().GetProperties()
                    .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                    .Aggregate(
                        new StringBuilder(),
                        (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                        sb => sb.ToString());
            }
        }

        public class Procuct
        {
            public enum unitOfMeasure { VNT, KG, L}
            public string? InternalID {get; set; }
            public string? NameEN { get; set; }
            public string? NameLT { get; set; }
            public string? Description { get; set; }
            public unitOfMeasure MeasureUnit { get; set; }
            public float Ammount { get; set; }

            // Price in cents
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }

            // Price in cents with loyanty card discounts
            public decimal? LoyaltyPrice { get; set; }

            // URIs pointing to an image of that product
            public List<Uri> Images { get; set; }
            public List<Category> Categories { get; set; }
            public List<String> Barcodes {get; set; }
            public List<Store> AvailableAt {get; set; }

            public Procuct() {
                Images = new List<Uri>();
                Categories = new List<Category>();
                Barcodes = new List<String>();
                AvailableAt = new List<Store>();
            }

            // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
            public override string ToString()
            {
                return GetType().GetProperties()
                    .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                    .Aggregate(
                        new StringBuilder(),
                        (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                        sb => sb.ToString());
            }
        }
    }
}
