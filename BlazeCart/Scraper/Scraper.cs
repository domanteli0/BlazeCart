using System;

namespace Scraper
{
    public abstract class Scraper
    {
        // All categories, including those who have subcategories
        public List<Category> AllCategories { get; private protected set; } = new List<Category>();
        // Only categories, which don't have any subcategories
        public List<Category> Categories { get; private protected set; } = new List<Category>();
        public List<Store> Stores { get; private protected set; } = new List<Store>();
        public List<Product> Products { get; private protected set; } = new List<Product>();

        protected HttpClient httpClient = new HttpClient();

        public Scraper() { }

        // re-fetches all data
        abstract public void scrape();
    }
}
