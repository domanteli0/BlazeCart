using System;
using System.Net;
using Scr = Scraper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Scraper;

namespace Tests1.Scraper
{
	public class IKIScarperTest
	{
		private Scr.IKIScraper _scraper;

		public IKIScarperTest()
		{
			_scraper = new(
				new HttpClient(),
				A.Fake<Logger<Scr.Scraper>>()
			);

        }

        [Fact]
        public async Task DuplicateTest()
        {
            // NOTE: This test should take a long time
            // (aprox. 3min, but depending on the machine may take longer)
            await _scraper.Scrape();

            foreach (var store in _scraper.Stores)
                Assert.Single(_scraper.Stores.FindAll(
                    s => s.InternalID!.Equals(s.InternalID))
                );

            foreach (var cat in _scraper.Categories)
                Assert.Single(_scraper.Categories.FindAll(
                    s => s.InternalID!.Equals(cat.InternalID))
                );

            foreach (var item in _scraper.Items)
                Assert.Single(_scraper.Items.FindAll(
                    s => s.InternalID.Equals(item.InternalID))
                );
        }
    }
}

