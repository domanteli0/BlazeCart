using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using Models;
using System;
using System.Threading;

namespace Scraper
{
    public class BarboraScraper : Scraper
    {
        public BarboraScraper() : base() { }

        public override async Task Scrape()
        {
            clean();
            var noOfCons = 10;
            var semaphore = new SemaphoreSlim(noOfCons, noOfCons);
            var tasks = new List<Task>();

            foreach (var cat in GetFetchableCategories())
            {
                tasks.Add(Task.Run(async () =>
                {
                    semaphore.Wait();
                    var c = await cat();
                    Categories.Add(c);
                    semaphore.Release();
                }));
            }
            await Task.WhenAll(tasks);

            semaphore = new SemaphoreSlim(noOfCons, noOfCons);
            foreach (var cat in Categories.GetWithoutChildren())
            {
                tasks.Add(Task.Run(async () =>
                {
                    semaphore.Wait();

                    var tries = 3;
                    while(tries > 0)
                    {
                        var items = await GetFetchableItems(cat.Uri!);
                        if (items is null)
                            // TODO: Proper logging
                            Console.WriteLine($"Failed to retrieve {cat.Uri}");
                        else
                        {
                            Items.AddRange(items);
                            tries = 0;
                        }
                        Thread.Sleep(100);
                        tries -= 1;
                    }

                    semaphore.Release();
                }));

            }
            await Task.WhenAll(tasks);
        }

        /// <returns>An inumeraion of async functions which fetch a category</returns>
        private IEnumerable<Func<Task<Category>>> GetFetchableCategories()
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
            foreach (var catHtml in doc.CssSelect("li.b-categories-root-category"))
            {
                yield return async () =>
                {
                    var url = "https://barbora.lt" + catHtml.CssSelect("a").First().GetAttributeValue("href");

                    var cat = new Category()
                    {
                        InternalID = catHtml.GetAttributeValue("data-b-cat-id"),
                        NameLT = catHtml.CssSelect("a").First().InnerText.Trim(),
                        Uri = new Uri(url)
                    };

                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        response = await httpClient.SendAsync(request);
                    }

                    reader = new StreamReader(response.Content.ReadAsStream());

                    var catDoc_ = new HtmlDocument();
                    catDoc_.LoadHtml(reader.ReadToEnd());
                    var catDoc = catDoc_.DocumentNode;

                    // Category child loop
                    foreach (var i in catDoc.CssSelect("div.b-single-category--box"))
                    {
                        Category catChild = new Category()
                        {
                            NameLT = i.CssSelect("a.b-single-category--child").First().InnerText.Trim(),
                            Parent = cat,
                            Uri = new Uri("https://barbora.lt/" + i.CssSelect("a.b-single-category--child").First().GetAttributeValue("href"))

                        };
                        cat.SubCategories.Add(catChild);

                        // TODO: Check if there are multiple pages and scrape them if there are
                        // Category grandchild loop
                        foreach (var ii in i.CssSelect("a.b-single-category--grandchild"))
                        {
                            var catGrandChild = new Category() {
                                NameLT = ii.InnerText.Trim(),
                                Uri = new Uri("https://barbora.lt" + ii.GetAttributeValue("href")),
                                Parent = catChild,

                            };

                            catChild.SubCategories.Add(catGrandChild);
                        }
                    }

                    return cat;
                };
            }
        }

        /// <summary>
        /// This request might fail, in that case `null` is returned
        /// </summary>
        private async Task<IEnumerable<Item>?> GetFetchableItems(Uri category)
        {
            HttpResponseMessage response;
            // TODO: Proper logging
            Console.WriteLine(category);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), category))
            {
                response = await httpClient.SendAsync(request);
            }

            var reader_ = new StreamReader(response.Content.ReadAsStream());

            var itemDoc_ = new HtmlDocument();
            itemDoc_.LoadHtml(reader_.ReadToEnd());
            var itemDoc = itemDoc_.DocumentNode;

            try
            {
                var itemJSON = itemDoc
                    .SelectNodes("/html/body/div[1]/div/div[3]/div/div[3]/div[2]/script")
                    .First().InnerText
                    .FindFirstRegexMatch("(?<=var products = ).*(?=;)");

                return ProccesItems(itemJSON);
            }
            catch (ArgumentNullException)
            {
                // TODO: proper logging
                Console.WriteLine($"Barbora probably responded with 500.\n");
            }

            return null;
        }

        private IEnumerable<Item> ProccesItems(string json)
        {
            foreach (var itemJSON in JArray.Parse(json))
            {
                Item item = new Item(internalID: itemJSON["id"]!.ToString())
                {
                    NameLT = itemJSON["title"]!.ToString(),
                    Price = (int)(itemJSON["price"]!.ToObject<float>() * 100),
                    Image = itemJSON["big_image"]!.ToObject<Uri>()!,
                    MeasureUnit = Item.ParseUnitOfMeasurement(itemJSON["comparative_unit"]!.ToString()),
                    PricePerUnitOfMeasure =
                        (int)(itemJSON["comparative_unit_price"]!.ToObject<float>() * 100)
                };

                yield return item;
            }
        }
    }
}

