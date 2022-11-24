using System;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Scraper
{
	public interface IScraper
	{
        public List<Category> Categories { get; }
        public List<Store> Stores { get; }
        public List<Item> Items { get; }

        public Task Scrape();
    }
}

