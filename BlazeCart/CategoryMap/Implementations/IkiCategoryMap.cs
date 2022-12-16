using System;
using Models;
using Common;
using CategoryMap;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public class IkiCategoryMap : ACategoryMap, ICategoryMap
	{
		public IkiCategoryMap(ILogger logger) : base(logger) { }

		public void Map(List<Category> root_cat, IDictionary<string, Category> into)
		{
			this.addMapper("Pienas", new() {
				("(?i)gėrimai", into["Pieno gėrimai"]),
				("(?i)sojų", into["Sojų pienas"])
			});

			this.addForUnmapped(into["UNMAPPED"]);

			var items = root_cat
				.GetWithoutChildren()
				.ToDictionary(c => c.NameLT!, c => c);

			this.executeMapper(items);
		}
	}
}

