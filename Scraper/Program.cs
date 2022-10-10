using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Scraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var storeList = new List<Store>();
            var productList = new List<Procuct>();

            #region initi_data
            // Gets the initial data JSON
            HttpResponseMessage response;

            // IMPORTANT:
            // Genereted with <https://curl.olsh.me/>, slightly edited
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/initial_data"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                    request.Content = new StringContent("{\"locale\":\"lt\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    response = await httpClient.SendAsync(request);
                }
            }

            StreamReader reader = new StreamReader( response.Content.ReadAsStream() );
            var JSONresponse = JObject.Parse(reader.ReadToEnd());
            #endregion

            #region stores
            // Gets the list of stores from initial_data JSON
            foreach (JToken cat_ in JSONresponse.GetValue("chains").Last.SelectToken("stores"))
            {
                storeList.Add(new Store {
                    Merch = Store.Merchendise.IKI,
                    InternalID = cat_.SelectToken("id").ToString(),
                    Name = cat_.SelectToken("name").ToString(),
                    Address = cat_.SelectToken("streetAndBuilding").ToString(),
                    Longitude =
                        cat_.SelectToken("location").SelectToken("geopoint").SelectToken("longitude").ToString(),
                    Latitude =
                        cat_.SelectToken("location").SelectToken("geopoint").SelectToken("latitude").ToString(),
                    } );
            }
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
                        InternalID = cat.SelectToken("id").ToString(),
                        NameLT = cat.SelectToken("name").SelectToken("lt").ToString(),
                        NameEN = cat.SelectToken("name").SelectToken("en").ToString(),
                    };

                    l.Add(listing);
                    catListFull.Add(listing);

                    var cat_ = cat.SelectToken("subcategories");
                    if (cat_.Count() > 0)
                    {
                        listing.SubCategories = toCat(cat_);
                    }
                    else
                        catListDeepestOnly.Add(listing);
                }

                return l;
            };

            toCat(JSONresponse.GetValue("categories"));

            Console.WriteLine("Full Only: {0}", catListFull.Count);
            // catListFull.ForEach(cat => Console.WriteLine(cat));
            Console.WriteLine("Deepest Only: {0}", catListDeepestOnly.Count);
            // catListDeepestOnly.ForEach(cat => Console.WriteLine(cat));
            #endregion

            // HttpClient client = new HttpClient();

            // // IMPORTANT:
            // // Genereted with <https://curl.olsh.me/>

            // // var response = await client.GetAsync("https://eparduotuve.iki.lt/api/search/view_products");

            // // Console.WriteLine(response);


            // // var handler = new HttpClientHandler();
            // HttpResponseMessage response;
            // // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            // // handler.AutomaticDecompression = ~DecompressionMethods.All;

            // // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
            // // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
            // // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            // using (var httpClient = new HttpClient())
            // {
            //     using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://eparduotuve.iki.lt/api/search/view_products"))
            //     {
            //         request.Headers.TryAddWithoutValidation("authority", "eparduotuve.iki.lt");
            //         request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
            //         request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");

            //         request.Content = new StringContent("{\"limit\":2,\"params\":{\"type\":\"view_products\",\"isActive\":true,\"isApproved\":true,\"storeIds\":[],\"sort\":\"karma\",\"categoryIds\":[],\"hasLoyaltyCard\":false,\"filter\":{},\"isUsingStock\":true}}");
            //         request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            //         response = await httpClient.SendAsync(request);
            //     }
            // }

            // // IMPORTNAT: end of autogenerated code

            // StreamReader reader = new StreamReader( response.Content.ReadAsStream() );
            // var JSONresponse = JObject.Parse(reader.ReadToEnd());
            // // Console.WriteLine(reader.ReadToEnd());
            // var a = JSONresponse.GetValue("data");
            // Console.WriteLine(a);
            // // foreach (var a in JSONresponse.GetValue("data"))
            // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
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
            public string? InternalID { get; set; }

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
            public enum conversionMeasure { VNT, KG, L}
            public string? NameEN { get; set; }
            public string? NameLT { get; set; }
            public string? Description { get; set; }
            public conversionMeasure ConcM { get; set; }
            public float Ammount { get; set; }

            // Price in cents
            public int Price { get; set; }
            public int DiscountPrice { get; set; }

            // Price in cents with loyanty card discounts
            public int LoyaltyPrice { get; set; }

            // URIs pointing to an image of that product
            public List<Uri>? Images { get; set; }
            public List<Category>? Category { get; set; }

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
