using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Scraper;
using DB;

namespace ScraperFunction
{
    public class ScraperFunction
    {
        private readonly ScraperDbContext _dbCtx;
        private readonly ICollection<IScraper> _scraperRepo;

        public ScraperFunction(ScraperDbContext dbCtx, IScraper []scraperRepo)
        {
            _dbCtx = dbCtx;
            _scraperRepo = scraperRepo;
        }

        [FunctionName("ScraperFunction")]
        public async Task Run(
            [TimerTrigger("0 0 * * * *", RunOnStartup = true)]TimerInfo myTimer,
            ILogger log
        )
        {
            log.LogInformation($"`ScraperFunction` Timer trigger function executed at: {DateTime.UtcNow}");


            var tasks = new List<Task>();
            foreach (var scraper in _scraperRepo)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await scraper.Scrape();
                    log.LogInformation(
                        $"Item Count: {scraper.Items.Count} from {scraper.GetType().Name} at: {DateTime.UtcNow}"
                    );
                }));
            }

            await Task.WhenAll(tasks);
            log.LogInformation($"Scraping finished at: {DateTime.UtcNow}");

            _dbCtx.Database.Migrate();

            await _dbCtx.Items.ForEachAsync(i => { _dbCtx.Remove(i); });
            await _dbCtx.Categories.ForEachAsync(i => { _dbCtx.Remove(i); });

            foreach (var scraper in _scraperRepo)
            {
                _dbCtx.Items.AddRange(scraper.Items);
                _dbCtx.Categories.AddRange(scraper.Categories);
            }

            _dbCtx.SaveChanges();
            log.LogInformation($"All items updated successfully to DB at: {DateTime.UtcNow}");
        }
    }
}

