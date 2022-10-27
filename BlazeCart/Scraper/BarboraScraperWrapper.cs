using System;
using System.Diagnostics;

namespace Scraper
{
    public class BarboraScraperWrapper : Scraper
    {
        public BarboraScraperWrapper() : base() { }

        /// <summary>
        /// The file will be saved to $PROJECT_LOCATION/BlazeCart/Scraper/BarboraScraper/test.json
        /// </summary>
        // TODO?: Serialize data
        public override void Scrape()
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
    }
}

