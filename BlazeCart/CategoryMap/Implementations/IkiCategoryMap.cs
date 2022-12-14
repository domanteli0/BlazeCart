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

        public void Map(IList<Category> root_cat, IDictionary<string, Category> into)
		{
			foreach (var cat in root_cat.GetWithoutChildren())
			{
				foreach (var item in cat.Items)
				{
					switch (item.Category.NameLT)
					{
						case "Pienas":
							if (item.NameLT.ContainsPattern("(?i)Pieno gėrimai"))
								map_item(
									item,
                                    into["Pieno gėrimai"]
								);
							else if (item.NameLT.ContainsPattern("(?i)Sojų"))
								map_item(
									item,
                                    into["Sojų pienas"]
								);

							break;
                        default:
                            //_logger.LogInformation(item.NameLT + " wasn't mapped");
                            map_category(cat, into["UNMAPPED"]);
                            break;
                    };
				}

			}
        }
	}
}

