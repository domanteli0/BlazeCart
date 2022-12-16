using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;
using Models;
using DB;
using Common;

namespace Scraper
{
    internal class Program
    {
        // In case, ScraperFunction ceases to work
        // This can be used to populate the DB
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var dbCtxFac = new DbContextFactory(
                configuration.GetConnectionString(configuration.GetSection("DB").Value!)
            );

            var dbCtx = dbCtxFac.CreateDbContext(null);

            var factory = LoggerFactory.Create(b => b.AddConsole());
            var logger = factory.CreateLogger<Scraper>();

            var iScraper = new IKIScraper(new HttpClient(), logger);
            var bScraper = new BarboraScraper(new HttpClient(), logger);


            await dbCtx.Items.ForEachAsync(i => { dbCtx.Remove(i); });
            await dbCtx.Categories.ForEachAsync(i => { dbCtx.Remove(i); });
            dbCtx.SaveChanges();

            List<Task> tasks = new();
            tasks.Add(Task.Run(async () =>
            {
                await bScraper.Scrape();
            }));

            tasks.Add(Task.Run(async () =>
            {
                await iScraper.Scrape();
            }));

            await Task.WhenAll(tasks);
            Console.WriteLine($"BARBORA: {bScraper.Items.Count}");
            Console.WriteLine($"IKI: {iScraper.Items.Count}\n");

            Console.WriteLine($"BARBORA: {bScraper.Categories.Count}");
            Console.WriteLine($"IKI: {iScraper.Categories.Count}\n");

            Console.WriteLine($"BARBORA: {bScraper.Categories.GetWithoutChildren(). SelectMany(cat => cat.Items).Count()}");
            
            Console.WriteLine($"IKI: {iScraper.Categories.GetWithoutChildren(). SelectMany(cat => cat.Items).Count()}\n");

            //Console.WriteLine($"BARBORA_CATS: {bScraper.Categories.SelectR(c => c).Count()}");
            //Console.WriteLine($"IKI_CATS: {iScraper.Categories.SelectR(c => c).Count()}");

            await dbCtx.Categories.AddRangeAsync(bScraper.Categories);
            //await dbCtx.Categories.AddRangeAsync(bScraper.Categories.GetWithoutChildren());

            //dbCtx.Items.Add(new () { InternalID = "TEWST", NameLT = "TEST", Category = bScraper.Categories.First(), Merch = Merchendise.Merch.IKI});
            //dbCtx.Items.Add(new () { InternalID = "TEST", NameLT = "TEwST", Category = bScraper.Categories.First(), Merch = Merchendise.Merch.BARBORA});

            await dbCtx.Categories.AddRangeAsync(iScraper.Categories);
            //await dbCtx.Items.AddRangeAsync(iScraper.Items);

            //iScraper.Items.ForEach(i => logger.LogInformation(i.Merch.ToString()));

            dbCtx.SaveChanges();
        }
    }
}
