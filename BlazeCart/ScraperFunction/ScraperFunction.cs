using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Abstractions;

using Scraper;
using DB;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScraperFunction
{
    public class ScraperFunction
    {
        public readonly ScraperDbContext DbCtx;

        public ScraperFunction(ScraperDbContext dbCtx)
        {
            DbCtx = dbCtx;
        }

        [FunctionName("ScraperFunction")]
        public async Task Run(
            [TimerTrigger("0 0 * * * *", RunOnStartup = true)]TimerInfo myTimer,
            ILogger log
        )
        {
            log.LogInformation($"`ScraperFunction` Timer trigger function executed at: {DateTime.UtcNow}");

            //var bScraper = new BarboraScraper();
            var iScraper = new IKIScraper();

            var tasks = new List<Task>();
            //tasks.Add(Task.Run(async () =>
            //{
            //    await bScraper.Scrape();
            //    log.LogInformation($"Barbora item Count: {bScraper.Items.Count} at: {DateTime.Now}");
            //}));

            tasks.Add(Task.Run(async () =>
            {
                await iScraper.Scrape();
                log.LogInformation($"IKI item Count: {iScraper.Items.Count} at: {DateTime.UtcNow}");
            }));

            await Task.WhenAll(tasks);
            log.LogInformation($"Scraping finished at: {DateTime.UtcNow}");

            DbCtx.Database.Migrate();

            await DbCtx.Items.ForEachAsync(i => { DbCtx.Remove(i); });

            //DbCtx.Items.AddRange(bScraper.Items);
            DbCtx.Items.AddRange(iScraper.Items);

            DbCtx.SaveChanges();
            log.LogInformation($"All items updated successfully to DB at: {DateTime.UtcNow}");
        }
    }
}

