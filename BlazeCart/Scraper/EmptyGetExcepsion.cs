using System;
namespace Scraper
{
    public class EmptyGetExcepsion : Exception
    {
        public Uri Uri { get; }
        public EmptyGetExcepsion(Uri uri) : base()
        {
            Uri = uri;
        }
    }
}

