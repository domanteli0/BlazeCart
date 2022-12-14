using System;
using Models;
using Common;
using Microsoft.Extensions.Logging;

namespace CategoryMap.Implementations
{
	public class BarboraCategoryMap : ACategoryMap, ICategoryMap
	{
		public BarboraCategoryMap(ILogger log) : base(log) { }

        public void Map(IList<Category> root_cats, IDictionary<string, Category> into)
        {
            foreach (var cat in root_cats.GetWithoutChildren())
            {
                switch (cat.NameLT)
                {
                    case "Pieno gėrimai":
                        map_category(cat, into["Pieno gėrimai"]);
                        break;
                    default:
                        _logger.LogInformation(cat.NameLT + " wasn't mapped");
                        map_category(cat, into["UNMAPPED"]);
                        break;
                }
            }
        }

    }
}

