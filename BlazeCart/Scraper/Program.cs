namespace Scraper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is just for demonstration purposes
            var b = new BarboraScraperWrapper();
            b.scrape();

            var a = new IKIScraper();
            a.init();
            a.Stores.ForEach(Console.WriteLine);
            a.UpdateAllItems(1);
            a.Items.ForEach(Console.WriteLine);
        }
    }
}
