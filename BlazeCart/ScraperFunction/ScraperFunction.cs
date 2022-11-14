using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Models;
using Scraper;
using DB;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScraperFunction
{
    public class ScraperFunction
    {
        [FunctionName("ScraperFunction")]
        public async Task Run(
            [TimerTrigger("0 0 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log
        )
        {
            log.LogInformation($"`ScraperFunction` Timer trigger function executed at: {DateTime.Now}");

            DbContextFactory ctxFac = null;
            try
            {
                ctxFac = new DbContextFactory("AzureDB");
            } catch (Exception e)
            {
                log.LogInformation(e.ToString());
                throw e;
            }

            using (ScraperDbContext db = ctxFac.CreateDbContext(null))
            {
                db.Database.Migrate();

                await db.Items.ForEachAsync(i => { db.Remove(i); } );

                var bScraper = new BarboraScraper();
                var iScraper = new IKIScraper();

                var tasks = new List<Task>();
                tasks.Add(Task.Run(async () =>
                {
                    await bScraper.Scrape();
                    log.LogInformation($"Barbora item Count: {bScraper.Items.Count} at: {DateTime.Now}");
                }));

                tasks.Add(Task.Run(async () =>
                {
                    await iScraper.Scrape();
                    log.LogInformation($"IKI item Count: {iScraper.Items.Count} at: {DateTime.Now}");
                }));

                await Task.WhenAll(tasks);

                bScraper.Items.ForEach(i => db.Items.Add(i));
                iScraper.Items.ForEach(i => db.Items.Add(i));

                db.SaveChanges();
                log.LogInformation($"All items updated successfully");
            }

        }
    }
}

