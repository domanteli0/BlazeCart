using System.Diagnostics;
using Models;

namespace Scraper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is just for demonstration purposes

            var a = new IKIScraper();
            a.Scrape();
            a.Stores.GetRange(0, 10).ForEach( s => { Console.WriteLine(s); });
            Console.WriteLine(a.AllCategories[1].CategoryTree());
            a.Items.GetRange(0, 10).ForEach(Console.WriteLine);

            var b = new BarboraScraper();
            b.Scrape();
            Console.WriteLine(b.Categories[0].CategoryTree());
            b.Items.GetRange(0, 10).ForEach(Console.WriteLine);
        }
    }
}
