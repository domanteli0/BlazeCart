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
            a.Stores.ForEach(e => { Console.WriteLine(e); } );
            a.Categories.ForEach(e => { Console.WriteLine(e); } );
            a.RefetchAllItems(1);
            a.Items.ForEach(Console.WriteLine);

            Console.WriteLine("Store count: {0}", a.Stores.Count);
            Console.WriteLine("Category count: {0}", a.Categories.Count);
            Console.WriteLine("Item count: {0}", a.Items.Count);
        }
    }
}
