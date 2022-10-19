using System;
using System.Text;

namespace Scraper
{
    // TODO: Subcategories, storeIds, chainIds
    public class Category
    {
        public string InternalID { get; set; }
        public string? NameEN { get; set; }
        public string? NameLT { get; set; }
        public List<Category>? SubCategories { get; set; }

        // <https://stackoverflow.com/questions/4023462/how-do-i-automatically-display-all-properties-of-a-class-and-their-values-in-a-s>
        public override string ToString()
        {
            return GetType().GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
                .Aggregate(
                    new StringBuilder(),
                    (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                    sb => sb.ToString());
        }
    }
}
