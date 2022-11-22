using System.Diagnostics;
using Microsoft.Extensions.Configuration;
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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var dbCtxFac = new DbContextFactory(
                configuration.GetConnectionString(configuration.GetSection("DB").Value!)
            );

            var dbCtx = dbCtxFac.CreateDbContext(null);

            Console.WriteLine(dbCtx);


            var a = new IKIScraper(new HttpClient());
            var b = new BarboraScraper(new HttpClient());

            List<Task> tasks = new();
            //tasks.Add(Task.Run(async () =>
            //{
            //    await b.Scrape();
            //    await dbCtx.Items.AddRangeAsync(b.Items);
            //    await dbCtx.Categories.AddRangeAsync(b.Categories);
            //}));

            tasks.Add(Task.Run(async () =>
            {
                await a.Scrape();
                await dbCtx.Items.AddRangeAsync(a.Items);
                await dbCtx.Categories.AddRangeAsync(a.Categories);
            }));

            await Task.WhenAll(tasks);

            dbCtx.SaveChanges();
        }
    }
}
