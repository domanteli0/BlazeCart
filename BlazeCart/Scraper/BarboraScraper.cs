using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Models;
using System;
using Common;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using Polly;

namespace Scraper
{
    public class BarboraScraper : Scraper
    {
        private protected override Merchendise.Merch _merch
            { get { return Merchendise.Merch.BARBORA; } }

        private HttpSender<HtmlNode> _httpSender;

        public BarboraScraper(HttpClient httpClient, ILogger<Scraper> logger) :
            base(httpClient, logger) { 
                _httpSender = new HttpSender<HtmlNode>(
                    _httpClient,
                    (res) => {
                        var reader_ = new StreamReader(res.Content.ReadAsStream());

                        var itemDoc_ = new HtmlDocument();
                        itemDoc_.LoadHtml(reader_.ReadToEnd());
                        return itemDoc_.DocumentNode;
                    }
                );
            }

        public override async Task Scrape()
        {
            clean();
            var retryPolicy = Policy
                .Handle<ArgumentNullException>()
                .Or<System.InvalidOperationException>()
                // .Or<Exception>()
                .WaitAndRetry(
                    5, 
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, sleepDuration, attemptNumber, context) => 
                        _logger.LogInformation($"Failed to get items from {context["category"]}; Retrying")
                );
            
            var fallbackPolicy = Policy
                .Handle<ArgumentNullException>()
                .Or<System.InvalidOperationException>()
                .Fallback(() => _logger.LogInformation("Failed to obtain data."));

            var policy = Policy.Wrap(retryPolicy, fallbackPolicy);

            Categories.AddRange(GetCategories());
                
            var wait_between_cats = 500; // in milis 
            
            await Parallel.ForEachAsync(
                Categories.GetWithoutChildren(), 
                async (cat, _) => {
                    List<Item>? items = null;
                    var context = new Context();
                    context["category"] = cat;
                    
                    await policy.Execute(async (_) => 
                        {
                            items = (await GetItems(cat)).ToList();

                            if (items is not null)
                            {
                                if (cat is null) throw new Exception("This shouldn't happen");
                                
                                _logger.LogInformation($"CAT:{cat.ToStringNullSafe()}");

                                items.ForEach(item => item.Category = cat );
                                cat .Items.AddRange(items);
                                this.Items.AddRange(items);

                                _logger.LogInformation($"Successfully retrieved from {cat.Uri} with {items.Count()} items");
                            }
                        }, 
                        context
                    );

                    await Task.Delay(wait_between_cats);
                }
            );

            // back-referencing
            //this.Items.ForEach((i) => i.Category.Items.Add(i));
            _logger.LogInformation("BARBORA COUNT: " + Items.Count);
            setMerch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An inumeraion of async functions which fetch a category</returns>
        private IEnumerable<Category> GetCategories()
        {
            var doc = _httpSender.Send(
                new HttpRequestMessage(new HttpMethod("GET"), "https://barbora.lt/")
            );

            // Category loop
            var ret = doc
                .CssSelect("li.b-categories-root-category")
                .Take(1)
                .Select((HtmlNode catHtml) => {
                    var url = "https://barbora.lt" + catHtml.CssSelect("a").First().GetAttributeValue("href");

                    var cat = new Category()
                    {
                        InternalID = catHtml.GetAttributeValue("data-b-cat-id"),
                        NameLT = catHtml.CssSelect("a").First().InnerText.Trim(),
                        Uri = new Uri(url),
                        Merch = _merch,
                    };

                    var catDoc = _httpSender.Send(
                        new HttpRequestMessage(new HttpMethod("GET"), url)
                    );
                    return new {cat, catDoc};
                    // return ret.cat;
                });
            
            ret
                // Child category loop
                .SelectMany(t => {
                    return t.catDoc
                        .CssSelect("div.b-single-category--box")
                        .Select(catDoc => new {t.cat, catDoc});
                })
                .SelectMany(t => {
                    var cat = t.cat;
                    var i = t.catDoc;
                    
                    _logger.LogInformation("LOGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                    Category catChild = null;
                    try {
                        catChild = new Category()
                        {
                            NameLT = i.CssSelect("a.b-single-category--child").First().InnerText.Trim(),
                            Uri = new Uri("https://barbora.lt/" + i.CssSelect("a.b-single-category--child").First().GetAttributeValue("href")),
                            Merch = _merch,
                        };
                        cat.SubCategories.Add(catChild);
                    } catch (Exception) { }

                    return i
                        .CssSelect("a.b-single-category--grandchild")
                        .Select(ii => new {catChild, ii})
                        .Where(t => t.catChild is not null);
                })
                // Grandchild category loop
                .ToList()
                .ForEach(t => {

                    var catChild = t.catChild;
                    var ii = t.ii;

                    var catGrandChild = new Category() {
                        NameLT = ii.InnerText.Trim(),
                        Uri = new Uri("https://barbora.lt" + ii.GetAttributeValue("href")),
                        Merch = _merch,
                    };

                    catChild.SubCategories.Add(catGrandChild);
                });

            return ret.Select((t) => t.cat);
        }

        /// <summary>
        /// This request might fail, in that case `null` is returned
        /// </summary>
        private async Task<List<Item>?> GetItems(Category category)
        {
            var itemDoc = await _httpSender.SendAsync(
                new HttpRequestMessage(new HttpMethod("GET"), category.Uri)
            );

            try
            {
                var itemJSON = itemDoc
                    .SelectNodes("/html/body/div[1]/div/div[3]/div/div[3]/div[2]/script")?
                    .First().InnerText
                    .FindFirstRegexMatch("(?<=var products = ).*(?=;)");
                
                var ret = ProccesItems(itemJSON).ToList();
                ret.ForEach(i => i.Category = category );
                return ret;
            }
            catch (ArgumentNullException)
            {
                _logger.LogInformation($"Barbora probably responded with 500");
                throw;
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
                        (int)(itemJSON["comparative_unit_price"]!.ToObject<float>() * 100),
                    Merch = _merch,
                };

                yield return item;
            }
        }
    }
}

