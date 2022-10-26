using System;
using Scraper;

namespace Models
{
    public class CategoryPath
    {
        public string InternalID { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public CategoryPath Parent { get; set; }
        public List<CategoryPath> Childern { get; set; } = new List<CategoryPath>();

        public CategoryPath()
        {
        }
    }
}

