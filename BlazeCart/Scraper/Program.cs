using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;
using Models;
using DB;
using Common;

// TODO: REMOVE
using CategoryMap;
using CategoryMap.Implementations;

namespace Scraper
{
    internal class Program
    {
        // In case, ScraperFunction ceases to work
        // This can be used to populate the DB
        static async Task Main(string[] args)
        {
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddUserSecrets<Program>()
            //     .AddJsonFile("appsettings.json")
            //     .Build();

            // var dbCtxFac = new DbContextFactory(
            //     configuration.GetConnectionString(configuration.GetSection("DB").Value!)
            // );

            // var dbCtx = dbCtxFac.CreateDbContext(null);

            var factory = LoggerFactory.Create(b => b.AddConsole());
            var logger = factory.CreateLogger<Scraper>();
            logger.LogInformation("STARTING");

            var iScraper = new IKIScraper(new HttpClient(), logger);
            logger.LogInformation("STARTING");
            var bScraper = new BarboraScraper(new HttpClient(), logger);

            logger.LogInformation("STARTING");

            // await dbCtx.Items.ForEachAsync(i => { dbCtx.Remove(i); });
            // await dbCtx.Categories.ForEachAsync(i => { dbCtx.Remove(i); });
            // dbCtx.SaveChanges();
            // logger.LogInformation("DELETED");

            List<Task> tasks = new();
            tasks.Add(Task.Run(async () =>
            {
                await bScraper.Scrape();
            }));

            // tasks.Add(Task.Run(async () =>
            // {
            //     await iScraper.Scrape();
            // }));

            await Task.WhenAll(tasks);
            logger.LogInformation($"Barbora count: {bScraper.Items.Count}");
            logger.LogInformation("SCRAPED");

            // var catMapDict = StaticCategoryTree.GetCategoryDict();
            // var iMap = new IkiCategoryMap(factory.CreateLogger<IkiCategoryMap>());
            // var bMap = new BarboraCategoryMap(factory.CreateLogger<BarboraCategoryMap>());

            // bMap.Map(bScraper.Categories, catMapDict);
            // iMap.Map(iScraper.Categories, catMapDict);
            // logger.LogInformation("MAPPED");
            
            // await dbCtx.Categories.AddRangeAsync(catMapDict.ToListOfValues());

            // logger.LogInformation($"BARBORA, GOT: {bScraper.Items.Count}");
            // logger.LogInformation($"IKI, GOT: {iScraper.Items.Count}");
            // logger.LogInformation($"DB, GOT: {dbCtx.Items.Count()} [MAY BE INACCURATE]");

            // dbCtx.SaveChanges();
        }
    }
}
