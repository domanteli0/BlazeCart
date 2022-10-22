using System;
using System.Text;

namespace Scraper
{
    public struct Store
    {
        public enum Merchendise { IKI, MAXIMA }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public Merchendise Merch { get; set; }
        public string InternalID { get; set; }
    }
}
