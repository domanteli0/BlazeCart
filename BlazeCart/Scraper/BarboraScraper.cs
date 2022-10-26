using System;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using Models;

namespace Scraper
{
    public class BarboraScraper : Scraper
    {
        public BarboraScraper() : base() { }

        public override void Scrape()
        {
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://barbora.lt/"))
            {
                response = httpClient.Send(request);
            }

            StreamReader reader = new StreamReader(response.Content.ReadAsStream());

            var doc_ = new HtmlDocument();
            doc_.LoadHtml(reader.ReadToEnd());
            var doc = doc_.DocumentNode;

            var catList = new List<CategoryPath>();

            foreach(var catHtml in doc.CssSelect("li.b-categories-root-category"))
            {
                var url = "https://barbora.lt" + catHtml.CssSelect("a").First().GetAttributeValue("href");
                Console.WriteLine(url);
                
                //Console.WriteLine(catHtml.CssSelect("a").First().InnerText); // <- Category name

                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    response = httpClient.Send(request);
                }

                reader = new StreamReader(response.Content.ReadAsStream());

                var catDoc_ = new HtmlDocument();
                catDoc_.LoadHtml(reader.ReadToEnd());
                var catDoc = catDoc_.DocumentNode;

                var cat = new CategoryPath() { NameLT = catHtml.CssSelect("a").First().InnerText.Trim() };

                foreach(var i in catDoc.CssSelect("div.b-single-category--box"))
                {

                    // TODO: Check if there are multiple pages and scrape them if there are
                    foreach(var ii in i.CssSelect("a.b-single-category--grandchild"))
                    {

                        cat.Childern.Add(
                                new CategoryPath() { NameLT = ii.InnerText.Trim() }
                            );
                        //Console.WriteLine(ii.InnerText); // <- Granchild Category Name

                        url = "https://barbora.lt" + ii.GetAttributeValue("href");
                        Console.WriteLine(url);
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                        {
                            response = httpClient.Send(request);
                        }

                        var reader_ = new StreamReader(response.Content.ReadAsStream());

                        var itemDoc_ = new HtmlDocument();
                        itemDoc_.LoadHtml(reader_.ReadToEnd());
                        var itemDoc = itemDoc_.DocumentNode;


                        var iii = itemDoc.SelectNodes("/html/body/div[1]/div/div[3]/div/div[3]/div[2]/script").First();

                        foreach (var itemJSON in JArray.Parse(
                                iii.InnerText.FindFirstRegexMatch("(?<=var products = ).*(?=;)"))
                            )
                        {
                            var item = new Item()
                            {
                                InternalID = itemJSON["id"]!.ToString(),
                                NameLT = itemJSON["title"]!.ToString(),
                                Price = (int)(itemJSON["price"]!.ToObject<float>() * 100),
                                PricePerUnitOfMeasure =
                                    (int)(itemJSON["comparative_unit_price"]!.ToObject<float>() * 100),

                            };

                            item.Images.Add(itemJSON["big_image"]!.ToObject<Uri>()!);

                            Items.Add(item);
                        }
                    }
                }
                catList.Add(cat);
            }
        }
    }
}

