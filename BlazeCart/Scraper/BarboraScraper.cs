using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using Microsoft.Extensions.Logging;
using Models;
using System;
using Common;
using System.Threading;
using System.Net.Http;

namespace Scraper
{
    public class BarboraScraper : Scraper
    {
        private protected override Merchendise.Merch _merch
            { get { return Merchendise.Merch.BARBORA; } }

        public BarboraScraper(HttpClient httpClient, ILogger<Scraper> logger) :
            base(httpClient, logger) { }

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

                    // TODO: refactor this mess
                    // probably with Polly <https://github.com/App-vNext/Polly>
                    IEnumerable<Item>? items = null;
                    try
                    {
                        items = await GetFetchableItems(cat.Uri!);
                    }
                    catch (EmptyGetExcepsion ex)
                    {
                        try
                        {
                            var tries = 3;
                            while (tries > 0)
                            {
                                items = await GetFetchableItems(ex.Uri!);
                                if (items is null)
                                    _logger.LogError(
                                        $"Failed to retrieve {ex.Uri}" +
                                        ((tries > 0) ? "Retrying" : "")
                                    );
                                else
                                {
                                    _logger.LogInformation($"Successfully retrieved from {cat.Uri}");
                                    Items.AddRange(items);
                                    break;
                                }
                                Thread.Sleep(100);
                                tries -= 1;
                            }
                        }
                        catch (EmptyGetExcepsion) { }

                    } finally
                    {
                        if (items is not null)
                            cat.Items = items.ToList();
                    }
                    semaphore.Release();
                }));

            }
            await Task.WhenAll(tasks);

            // back-referencing
            Items.ForEach((i) => i.Category.Items.Add(i));
            setMerch();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>An inumeraion of async functions which fetch a category</returns>
        private IEnumerable<Func<Task<Category>>> GetFetchableCategories()
        {
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://barbora.lt/"))
            {
                response = _httpClient.Send(request);
                _logger.LogInformation($"Sent request to {request.RequestUri}");
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
                        response = await _httpClient.SendAsync(request);
                        _logger.LogInformation($"Sent request to {request.RequestUri}");
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
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), category))
            {
                response = await _httpClient.SendAsync(request);
                _logger.LogInformation($"Sent request to {request.RequestUri}");
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
                _logger.LogInformation($"Barbora probably responded with 500");
                throw new EmptyGetExcepsion(category);
            }
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

