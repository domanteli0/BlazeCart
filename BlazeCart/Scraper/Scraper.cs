using System;
using System.Net;
using Models;

namespace Scraper
{
    public abstract class Scraper
    {
        public List<Category> Categories { get; private protected set; } = new List<Category>();
        public List<Store> Stores { get; private protected set; } = new List<Store>();
        public List<Item> Items { get; private protected set; } = new List<Item>();

        protected HttpClient httpClient = new HttpClient();

        public Scraper() { }
        abstract public Task Scrape();

        private protected void clean()
        {
            Categories = new List<Category>();
            Stores = new List<Store>();
            Items = new List<Item>();
        }
    }
}
