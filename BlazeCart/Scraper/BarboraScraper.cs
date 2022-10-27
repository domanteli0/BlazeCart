using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using System.Diagnostics;
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

            // Category loop
            foreach(var catHtml in doc.CssSelect("li.b-categories-root-category"))
            {
                var url = "https://barbora.lt" + catHtml.CssSelect("a").First().GetAttributeValue("href");
                Console.WriteLine(url);

                var cat = new Category(
                    internalID: catHtml.GetAttributeValue("data-b-cat-id"),
                    nameLT: catHtml.CssSelect("a").First().InnerText.Trim()
                    );

                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    response = httpClient.Send(request);
                }

                reader = new StreamReader(response.Content.ReadAsStream());

                var catDoc_ = new HtmlDocument();
                catDoc_.LoadHtml(reader.ReadToEnd());
                var catDoc = catDoc_.DocumentNode;

                // Category child loop
                foreach(var i in catDoc.CssSelect("div.b-single-category--box"))
                {
                    Category catChild = new Category(
                        nameLT: i.CssSelect("a.b-single-category--child").First().InnerText.Trim(),
                        parent: cat
                        );
                    cat.SubCategories.Add(catChild);

                    // TODO: Check if there are multiple pages and scrape them if there are
                    // Category grandchild loop
                    foreach(var ii in i.CssSelect("a.b-single-category--grandchild"))
                    {
                        var catGrandChild = new Category(
                            nameLT: ii.InnerText.Trim(),
                            parent: catChild
                            );

                        catChild.SubCategories.Add(catGrandChild);

                        url = "https://barbora.lt" + ii.GetAttributeValue("href");
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                        {
                            response = httpClient.Send(request);
                        }

                        var reader_ = new StreamReader(response.Content.ReadAsStream());

                        var itemDoc_ = new HtmlDocument();
                        itemDoc_.LoadHtml(reader_.ReadToEnd());
                        var itemDoc = itemDoc_.DocumentNode;

                        var iii = itemDoc.SelectNodes("/html/body/div[1]/div/div[3]/div/div[3]/div[2]/script").First();

                        // Item looop
                        foreach (var itemJSON in JArray.Parse(
                                iii.InnerText.FindFirstRegexMatch("(?<=var products = ).*(?=;)")))
                        {
                            Item item = new Item(
                                internalID: itemJSON["id"]!.ToString(),
                                nameLT: itemJSON["title"]!.ToString(),
                                price: (int)(itemJSON["price"]!.ToObject<float>() * 100),
                                image: itemJSON["big_image"]!.ToObject<Uri>()!,
                                measureUnit: itemJSON["comparative_unit"]!.ToString(),
                                pricePerUnitOfMeasure:
                                    (int)(itemJSON["comparative_unit_price"]!.ToObject<float>() * 100)
                            );

                            Items.Add(item);
                        }
                    }
                }

                Categories.Add(cat);
            }
        }
    }
}

