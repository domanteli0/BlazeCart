namespace Scraper
{
    internal class Program
    {
        // TODO:
        // * Refactor, currently this is more in a PoC stage
        //   * Error handling (remove all null-forgiving operators?)
        // * Back referencing products with `Store` instance

        static void Main(string[] args)
        {
            var a = new IKIScraper();
            a.init();
            a.Stores.ForEach(Console.WriteLine);
        }
    }
}
