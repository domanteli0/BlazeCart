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
                ("(?i)be laktozės", into["Jogurtai ir desertai be laktozės"]),
                ("(?i)natūralus", into["Jogurtai be pagardų"])
            });
            this.addMapper("Grietinė", new()
            {
                ("(?i).*", into["Grietinė"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i)jogurtinis gėrimas", into["Geriamieji jogurtai"]),
                ("(?i)geriamasis", into["Geriamieji jogurtai"]),
                ("(?i)geriamas", into["Geriamieji jogurtai"]),
                ("(?i)be laktozės", into["Jogurtai ir desertai be laktozės"])
            });
            this.addMapper("Geriamasis jogurtas", new()
            {
                ("(?i).*", into["Geriamieji jogurtai"])
            });
            this.addMapper("Varškės Sūreliai", new()
            {
                ("(?i).*", into["Varškės sūreliai"])
            });
            this.addMapper("Kefyras, pasukos, rūgpienis", new()
            {
                ("(?i)kefyras", into["Kefyras ir kefyro gėrimai"]),
                ("(?i)kefyro", into["Kefyras ir kefyro gėrimai"]),
                ("(?i)rūgpienis", into["Rūgpienis"]),
                ("(?i)rauginto", into["Raugintos pasukos"])

            });
            this.addMapper("Sviestas, margarinas, riebalai", new()
            {
                ("(?i)sviestas", into["Sviestas"]),
                ("(?i)margarinas", into["Margarinas"]),
                ("(?i)tepinys", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepieji", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepiųjų", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepus", into["Tepieji riebalų mišiniai"]),
                ("(?i)tepusis", into["Tepieji riebalų mišiniai"])
            });
            this.addMapper("Minkšti sūriai", new()
            {
                ("(?i)dešrelės", into["Sūrio užkandžiai ir sūrio lazdelės"]),
                ("(?i)užkandis", into["Sūrio užkandžiai ir sūrio lazdelės"]),
                ("(?i)lydytas", into["Lydyti sūriai"]),
                ("(?i)tepamas", into["Tepamieji sūriai"]),
                ("(?i)tepamasis", into["Tepamieji sūriai"]),
                ("(?i)pelėsinis", into["Pelėsiniai sūriai"]),
                ("(?i)fetos", into["Fetos ir brinzos sūriai"]),
                ("(?i)maskarponės", into["Maskarponės ir rikotos sūriai"]),
                ("(?i)buratta", into["Mocarelos ir buratos sūriai"]),
                ("(?i)mozzarella", into["Mocarelos ir buratos sūriai"]),
                ("(?i)sūris", into["Fermentiniai sūriai"]),

            });
            this.addMapper("Puskiečiai ir kieti sūriai", new()
            {
                ("(?i).*", into["Kietieji sūriai"])
            });
            this.addMapper("Varškės sūriai", new()
            {
                ("(?i).*", into["Varškės sūriai"])
            });
            this.addMapper("Varškė", new()
            {
                ("(?i).*", into["Varškė"])
            });
            this.addMapper("Majonezas", new()
            {
                ("(?i).*", into["Majonezas"])
            });
            this.addMapper("Varškė Desertai, Užtepėlės", new()
            {
                ("(?i)grūdėta", into["Grūdėta varškė"]),
                ("(?i)sūrelis", into["Varškės sūreliai"]),
                ("(?i)varškytė", into["Desertinė varškė"]),
                ("(?i)tepamoji", into["Tepamoji varškė"]),
                ("(?i)užtepėlė", into["Tepamoji varškė"]),
                ("(?i)figomis", into["Desertinė varškė"]),
                ("(?i)mangais", into["Desertinė varškė"]),
                ("(?i)šilauogėmis", into["Desertinė varškė"]),
                ("(?i)slyvomis", into["Desertinė varškė"]),
                ("(?i)braškėmis", into["Desertinė varškė"]),
            });
            this.addMapper("Produktai be laktozės", new()
            {
                ("(?i)pienas", into["Pienas ir gėrimai be laktozės"])
               
            });
            this.addForUnmapped(into["UNMAPPED"]);

			var items = root_cat
				.GetWithoutChildren()
				.ToDictionary(c => c.NameLT!, c => c);

			this.executeMapper(items);
		}
	}
}

