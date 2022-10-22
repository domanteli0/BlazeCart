using System;
using System.Text;

namespace Scraper
{
    public struct Category
    {
        public string InternalID { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public List<Category>? SubCategories { get; set; }
    }
}
