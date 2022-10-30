using System.Diagnostics;
using Models;

namespace Scraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // This is just for demonstration purposes
            var tasks = new List<Task>();
            tasks.Add(Task.Run(async () =>
            {
                var b = new BarboraScraper();
                await b.Scrape();
                //b.Categories.ForEach(c => Console.WriteLine(c.Tree()));
                //b.Items.GetRange(0, 10).ForEach(Console.WriteLine);
                Console.WriteLine($"Barbora item Count: {b.Items.Count}");
            }));

            tasks.Add(Task.Run(async () =>
            {
                var a = new IKIScraper();
                await a.Scrape();
                //a.Stores.GetRange(0, 10).ForEach(s => { Console.WriteLine(s); });
                //Console.WriteLine(a.Categories[1].Tree());
                //a.Items.GetRange(0, 10).ForEach(Console.WriteLine);
                Console.WriteLine($"IKI item Count: {a.Items.Count}");
            }));

            await Task.WhenAll(tasks);
        }
    }
}
