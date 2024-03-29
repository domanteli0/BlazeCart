﻿using Models;
using Microsoft.Extensions.Logging;
using Common;

namespace Scraper
{
    public abstract class Scraper : IScraper
    {
        private protected abstract Merchendise.Merch _merch { get; }
        public List<Category> Categories { get; private protected set; } = new List<Category>();
        public List<Store> Stores { get; private protected set; } = new List<Store>();
        public List<Item> Items { get; private protected set; } = new List<Item>();

        private protected HttpClient _httpClient;
        private protected ILogger<Scraper> _logger;

        public Scraper(HttpClient httpClient, ILogger<Scraper> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        abstract public Task Scrape();

        private protected void setMerch()
        {
            Categories.ForEachR(el => el.Merch = _merch);
            Stores.ForEach(el => el.Merch = _merch);
            Items.ForEach(el => el.Merch = _merch);

            // In theory, not necessery, in practive - don't remove
            Categories
                .GetWithoutChildren()
                .ToList()
                .ForEach(cat =>
                    cat.Items.ForEach(i => i.Merch = cat.Merch)
                );
        }

        private protected void clean()
        {
            Categories = new List<Category>();
            Stores = new List<Store>();
            Items = new List<Item>();
        }
    }
}
