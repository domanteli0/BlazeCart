using System.Linq;
using Models;
using Common;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public class BarboraCategoryMap : ACategoryMap, ICategoryMap
	{
		public BarboraCategoryMap(ILogger log) : base(log) { }

        public void Map(List<Category> root_cat, IDictionary<string, Category> into)
        {
            // NOTE: '.*' will match anything
            this.addMapper("Pieno gėrimai", new() { ("(?i).*", into["Pieno gėrimai"]) });
            this.addForUnmapped(into["UNMAPPED"]);

            // A hacky work-around since keys must be unique
            var a = root_cat
                .Find(c => c.NameLT!.Equals("Bakalėja"))!
                .SubCategories
                .Find(c => c.NameLT!.Equals("Kalėdiniai saldumynai"))!;
            _logger.LogInformation(a.ToString());
            a.SubCategories.RemoveAll(c => c.NameLT!.Equals("Saldainių rinkiniai"));

            var items = root_cat
                .GetWithoutChildren()
                .ToDictionary(c => c.NameLT!, c => c);

            this.executeMapper(items);
        }

    }
}

