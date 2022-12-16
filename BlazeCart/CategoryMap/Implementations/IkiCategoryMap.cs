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
				("(?i)gėrimas", into["Pieno gėrimai"]),
				("(?i)sojų", into["Sojų pienas"]),
				("(?i)ryžių", into["Augalinis pienas"]),
				("(?i)sutirštintas", into["Sutirštintas pienas"]),
				("(?i)avižų", into["Augalinis pienas"]),
                ("(?i)kokosų", into["Augalinis pienas"]),
                ("(?i)migdolų", into["Augalinis pienas"]),
				("(?i)pienas", into["Pasterizuotas pienas"]),
				("(?i)natūralus", into["Pasterizuotas pienas"])

            });
			this.addMapper("Kiaušiniai", new()
			{
				("(?i)vištų kiaušiniai", into["Vištų kiaušiniai"]),
                ("(?i)putpelių kiaušiniai", into["Putpelių kiaušiniai"]),
                ("(?i)kiaušiniai", into["Vištų kiaušiniai"])
            });
			this.addMapper("Jogurtas", new()
			{
				("(?i)persikais", into["Jogurtai su pagardais"]),
				("(?i)braškių",  into["Jogurtai su pagardais"]),
                ("(?i)vyšnių",  into["Jogurtai su pagardais"]),
                ("(?i)abrikosų",  into["Jogurtai su pagardais"]),
                ("(?i)braškėmis",  into["Jogurtai su pagardais"]),
                ("(?i)spanguolėmis",  into["Jogurtai su pagardais"]),
                ("(?i)mangais",  into["Jogurtai su pagardais"]),
                ("(?i)persikų",  into["Jogurtai su pagardais"]),
                ("(?i)bananų",  into["Jogurtai su pagardais"]),
                ("(?i)kriaušių",  into["Jogurtai su pagardais"]),
                ("(?i)vyšniomis",  into["Jogurtai su pagardais"]),
                ("(?i)figomis",  into["Jogurtai su pagardais"]),
                ("(?i)agrastais",  into["Jogurtai su pagardais"]),
                ("(?i)šilauogėmis",  into["Jogurtai su pagardais"]),
                ("(?i)bananais",  into["Jogurtai su pagardais"]),
                ("(?i)kiviais",  into["Jogurtai su pagardais"]),
                ("(?i)vanile",  into["Jogurtai su pagardais"]),
                ("(?i)ananasais",  into["Jogurtai su pagardais"]),
                ("(?i)mėlynėmis",  into["Jogurtai su pagardais"]),
                ("(?i)obuoliais",  into["Jogurtai su pagardais"]),
                ("(?i)slyvomis",  into["Jogurtai su pagardais"]),
                ("(?i)apelsinų",  into["Jogurtai su pagardais"]),
                ("(?i)miško uogomis",  into["Jogurtai su pagardais"]),

                ("(?i)natūralus", into["Jogurtai be pagardų"])
            });
            this.addMapper("Grietinė", new()
            {
                ("(?i)grietinė", into["Grietinė"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i)jogurtinis gėrimas", into["Geriamieji jogurtai"]),
                ("(?i)geriamasis", into["Geriamieji jogurtai"]),
                ("(?i)geriamas", into["Geriamieji jogurtai"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i).*", into["Geriamieji jogurtai"])
            });
            this.addMapper("Varškės Sūreliai", new()
            {
                ("(?i).*", into["Varškės sūreliai"])
            });
            this.addForUnmapped(into["UNMAPPED"]);

			var items = root_cat
				.GetWithoutChildren()
				.ToDictionary(c => c.NameLT!, c => c);

			this.executeMapper(items);
		}
	}
}

