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
            this.addMapper("Grybai", new()
            {
                ("(?i)pievagrybiai", into["Grybai"]),
                ("(?i)grybai", into["Grybai"]),
                ("(?i)kreivabudės", into["Grybai"]),
                ("(?i)baravykas", into["Grybai"]),
                ("(?i)voveraitės", into["Grybai"]),
            });

            this.addMapper("Daržovės", new()
            {
                ("(?i)morkos", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)morkytės", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)bulvės", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)bulvių", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)kopūstai", into["Bulvės, morkos ir kopūstai"]),
                ("(?i)agurkai", into["Pomidorai ir agurkai"]),
                ("(?i)pomidorai", into["Pomidorai ir agurkai"]),
                ("(?i)pomidoriukai", into["Pomidorai ir agurkai"]),
                ("(?i)svogūnai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)svogūnų", into["Svogūnai, porai ir česnakai"]),
                ("(?i)laiškai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)porai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)česnakai", into["Svogūnai, porai ir česnakai"]),
                ("(?i)petražolės", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)petražolių", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)mėtos", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)mėta", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)bazilikai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)šalavijai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)rozmarinai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)rozmarinas", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)kalendros", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)čiobreliai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)čiobrelis", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)krapai", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)krapų", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)žalumynų", into["Prieskoninės daržovės ir žolelės"]),
                ("(?i)paprika", into["Paprikos ir baklažanai"]),
                ("(?i)paprikos", into["Paprikos ir baklažanai"]),
                ("(?i)baklažanai", into["Paprikos ir baklažanai"]),
                ("(?i)avokadai", into["Avokadai"]),
                ("(?i)salotų mišinys", into["Salotos ir jų mišiniai"]),
                ("(?i)daigai", into["Salotos ir jų mišiniai"]),
                ("(?i)salotos", into["Salotos ir jų mišiniai"]),
                ("(?i)špinatai", into["Salotos ir jų mišiniai"]),
                ("(?i)gražgarstė", into["Salotos ir jų mišiniai"]),
                ("(?i)būrokėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)imbierai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)imbieras", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)burokėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)salierai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikėliai", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikėlių", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)ridikas", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)kaliaropės", into["Būrokėliai ir kiti šakniavaisiai"]),
                ("(?i)kukurūzai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)kukurūzų", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)žirniai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)žirneliai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)pupupelės", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)smidrai", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)smidrų", into["Kukurūzai, žirniai, pupelės ir smidrai"]),
                ("(?i)moliūgai", into["Moliūgai ir cukinijos"]),
                ("(?i)moliūgas", into["Moliūgai ir cukinijos"]),
                ("(?i)cukinija", into["Moliūgai ir cukinijos"]),
                ("(?i)cukinijos", into["Moliūgai ir cukinijos"]),
                ("(?i)brokoliai", into["Brokoliai"]),
                ("(?i)paprika", into["Paprikos ir baklažanai"]),
            });

            this.addMapper("Vaisiai ir uogos", new()
            {
                ("(?i)bananai", into["Bananai"]),
                ("(?i)mandarinai", into["Citrusiniai vaisiai"]),
                ("(?i)citrinos", into["Citrusiniai vaisiai"]),
                ("(?i)greipfrutai", into["Citrusiniai vaisiai"]),
                ("(?i)klementinai", into["Citrusiniai vaisiai"]),
                ("(?i)apelsinai", into["Citrusiniai vaisiai"]),
                ("(?i)kiviai", into["Egzotiniai vaisiai"]),
                ("(?i)pasiflora", into["Egzotiniai vaisiai"]),
                ("(?i)pasiflorai", into["Egzotiniai vaisiai"]),
                ("(?i)mangai", into["Egzotiniai vaisiai"]),
                ("(?i)mangų", into["Egzotiniai vaisiai"]),
                ("(?i)datulės", into["Egzotiniai vaisiai"]),
                ("(?i)papajos", into["Egzotiniai vaisiai"]),
                ("(?i)ananasas", into["Egzotiniai vaisiai"]),
                ("(?i)kokoso", into["Egzotiniai vaisiai"]),
                ("(?i)kokosas", into["Egzotiniai vaisiai"]),
                ("(?i)persimonai", into["Egzotiniai vaisiai"]),
                ("(?i)granatai", into["Egzotiniai vaisiai"]),
                ("(?i)kinkanai", into["Egzotiniai vaisiai"]),
                ("(?i)obuoliai", into["Obuoliai ir kriaušės"]),
                ("(?i)kertuotis", into["Egzotiniai vaisiai"]),
                ("(?i)obuolys", into["Obuoliai ir kriaušės"]),
                ("(?i)kriaušės", into["Obuoliai ir kriaušės"]),
                ("(?i)kriaušė", into["Obuoliai ir kriaušės"]),
                ("(?i)vynuogės", into["Vynuogės ir uogos"]),
                ("(?i)gervuogės", into["Vynuogės ir uogos"]),
                ("(?i)avietės", into["Vynuogės ir uogos"]),
                ("(?i)šilauogės", into["Vynuogės ir uogos"]),
                ("(?i)spanguolės", into["Vynuogės ir uogos"]),
                ("(?i)melionai", into["Melionai"]),
                ("(?i)melionas", into["Melionai"]),
                ("(?i)arbūzas", into["Melionai"]),
                ("(?i)arbūzai", into["Melionai"]),
                ("(?i)persikas", into["Kaulavaisiai"]),
                ("(?i)abrikosas", into["Kaulavaisiai"]),
                ("(?i)slyva", into["Kaulavaisiai"]),
                ("(?i)slyvos", into["Kaulavaisiai"]),
            });


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
                .ToList();

            this.executeMapper(items);
		}
	}
}

