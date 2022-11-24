using System;
using System.Diagnostics;

namespace Scraper
{
    public class BarboraScraperWrapper
    {
        public BarboraScraperWrapper() { }

        /// <summary>
        /// The file will be saved to $PROJECT_LOCATION/BlazeCart/Scraper/BarboraScraper/test.json
        /// </summary>
        // TODO?: Serialize data

        public void scrape()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WorkingDirectory = "BarboraScraper/BarboraScraper/",
                FileName = "scrapy",
                Arguments = "crawl barbora -o test.json",
            };
            Process proc = new Process()
            {
                StartInfo = startInfo,
            };
            proc.Start();
        }

        public Task Scrape()
        {
            throw new NotImplementedException("Use `scrape`");
        }
    }
}

