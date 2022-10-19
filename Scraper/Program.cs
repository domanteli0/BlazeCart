using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Scraper;
using static System.Formats.Asn1.AsnWriter;
using System;

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
            a.scrape();
        }
    }
}
