using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Models;
using DB;

namespace Scraper
{
    internal class Program
    {
        // In case, ScraperFunction ceases to work
        // This can be used to populate the DB
        static async Task Main(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddUserSecrets<Program>()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var dbCtxFac = new DbContextFactory(
            //    configuration.GetConnectionString(configuration.GetSection("DB").Value!)
            //);

            //var dbCtx = dbCtxFac.CreateDbContext(null);

            var factory = LoggerFactory.Create(b => b.AddConsole());
            var logger = factory.CreateLogger<Scraper>();

            var iScraper = new IKIScraper(new HttpClient(), logger);
            var bScraper = new BarboraScraper(new HttpClient(), logger);

            List<Task> tasks = new();
            tasks.Add(Task.Run(async () =>
            {
                await bScraper.Scrape();
                //await dbCtx.Items.AddRangeAsync(bScraper.Items);
                //await dbCtx.Categories.AddRangeAsync(bScraper.Categories);
            }));

            tasks.Add(Task.Run(async () =>
            {
                await iScraper.Scrape();
                //await dbCtx.Items.AddRangeAsync(iScraper.Items);
                //await dbCtx.Categories.AddRangeAsync(iScraper.Categories);
            }));

            await Task.WhenAll(tasks);

            //dbCtx.SaveChanges();
        }
    }
}
