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
            this.addMapper("Pasterizuotas pienas", new() { ("(?i).*", into["Pasterizuotas pienas"]) });
            this.addMapper("Pieno gėrimai", new() { ("(?i).*", into["Pieno gėrimai"]) });
            this.addMapper("Ilgo galiojimo pienas", new() { ("(?i).*", into["Ilgo galiojimo pienas"]) });
            this.addMapper("Sutirštintas pienas", new() { ("(?i).*", into["Sutirštintas pienas"]) });
            this.addMapper("Sojų pienas", new() { ("(?i).*", into["Sojų pienas"]) });
            this.addMapper("Augalinis pienas", new() { ("(?i).*", into["Augalinis pienas"]) });
            this.addMapper("Sviestas", new() { ("(?i).*", into["Sviestas"]) });
            this.addMapper("Tepieji riebalų mišiniai", new() { ("(?i).*", into["Tepieji riebalų mišiniai"]) });
            this.addMapper("Margarinas", new() { ("(?i).*", into["Margarinas"]) });
            this.addMapper("Pienas ir gėrimai be laktozės", new() { ("(?i).*", into["Pienas ir gėrimai be laktozės"]) });
            this.addMapper("Jogurtai ir desertai be laktozės", new() { ("(?i).*", into["Jogurtai ir desertai be laktozės"]) });
            this.addMapper("Grietinė", new() { ("(?i).*", into["Grietinė"]) });
            this.addMapper("Fermentiniai sūriai", new() { ("(?i).*", into["Fermentiniai sūriai"]) });
            this.addMapper("Kietieji sūriai", new() { ("(?i).*", into["Kietieji sūriai"]) });
            this.addMapper("Pelėsiniai sūriai", new() { ("(?i).*", into["Pelėsiniai sūriai"]) });
            this.addMapper("Lydyti sūriai", new() { ("(?i).*", into["Lydyti sūriai"]) });
            this.addMapper("Varškės sūriai", new() { ("(?i).*", into["Varškės sūriai"]) });
            this.addMapper("Mocarelos ir buratos sūriai", new() { ("(?i).*", into["Mocarelos ir buratos sūriai"]) });
            this.addMapper("Maskarponės ir rikotos sūriai", new() { ("(?i).*", into["Maskarponės ir rikotos sūriai"]) });
            this.addMapper("Fetos ir brinzos sūriai", new() { ("(?i).*", into["Fetos ir brinzos sūriai"]) });
            this.addMapper("Tepamieji sūriai", new() { ("(?i).*", into["Tepamieji sūriai"]) });
            this.addMapper("Sūrio užkandžiai ir sūrio lazdelės", new() { ("(?i).*", into["Sūrio užkandžiai ir sūrio lazdelės"]) });
            this.addMapper("Majonezas", new() { ("(?i).*", into["Majonezas"]) });
            this.addMapper("Kefyras ir kefyro gėrimai", new() { ("(?i).*", into["Kefyras ir kefyro gėrimai"]) });
            this.addMapper("Rūgpienis", new() { ("(?i).*", into["Rūgpienis"]) });
            this.addMapper("Raugintos pasukos", new() { ("(?i).*", into["Raugintos pasukos"]) });
            this.addMapper("Jogurtai be pagardų", new() { ("(?i).*", into["Jogurtai be pagardų"]) });
            this.addMapper("Jogurtai su pagardais", new() { ("(?i).*", into["Jogurtai su pagardais"]) });
            this.addMapper("Geriamieji jogurtai", new() { ("(?i).*", into["Geriamieji jogurtai"]) });
            this.addMapper("Jogurtai be pagardų", new() { ("(?i).*", into["Jogurtai be pagardų"]) });
            this.addMapper("Varškė", new() { ("(?i).*", into["Varškė"]) });
            this.addMapper("Grūdėta varškė", new() { ("(?i).*", into["Grūdėta varškė"]) });
            this.addMapper("Tepamoji varškė", new() { ("(?i).*", into["Tepamoji varškė"]) });
            this.addMapper("Desertinė varškė", new() { ("(?i).*", into["Desertinė varškė"]) });
            this.addMapper("Varškės sūreliai", new() { ("(?i).*", into["Varškės sūreliai"]) });
            this.addMapper("Vištų kiaušinai", new() { ("(?i).*", into["Vištų kiaušinai"]) });
            this.addMapper("Putpelių kiaušiniai", new() { ("(?i).*", into["Putpelių kiaušiniai"]) });
            this.addMapper("Pomidorai ir agurkai", new() { ("(?i).*", into["Pomidorai ir agurkai"]) });
            this.addMapper("Paprikos ir baklažanai", new() { ("(?i).*", into["Paprikos ir baklažanai"]) });
            this.addMapper("Bulvės, morkos ir kopūstai", new() { ("(?i).*", into["Bulvės, morkos ir kopūstai"]) });
            this.addMapper("Svogūnai, porai ir česnakai", new() { ("(?i).*", into["Svogūnai, porai ir česnakai"]) });
            this.addMapper("Būrokėliai ir kiti šakniavaisiai", new() { ("(?i).*", into["Būrokėliai ir kiti šakniavaisiai"]) });
            this.addMapper("Salotos ir jų mišiniai", new() { ("(?i).*", into["Salotos ir jų mišiniai"]) });
            this.addMapper("Prieskoninės daržovės ir žolelės", new() { ("(?i).*", into["Prieskoninės daržovės ir žolelės"]) });
            this.addMapper("Kukurūzai, žirniai, pupelės ir smidrai", new() { ("(?i).*", into["Kukurūzai, žirniai, pupelės ir smidrai"]) });
            this.addMapper("Moliūgai ir cukinijos", new() { ("(?i).*", into["Moliūgai ir cukinijos"]) });
            this.addMapper("Brokoliai", new() { ("(?i).*", into["Brokoliai"]) });
            this.addMapper("Bananai", new() { ("(?i).*", into["Bananai"]) });
            this.addMapper("Citrusiniai vaisiai", new() { ("(?i).*", into["Citrusiniai vaisiai"]) });
            this.addMapper("Obuoliai ir kriaušės", new() { ("(?i).*", into["Obuoliai ir kriaušės"]) });
            this.addMapper("Vynuogės ir uogos", new() { ("(?i).*", into["Vynuogės ir uogos"]) });
            this.addForUnmapped(into["UNMAPPED"]);

            var items = root_cat
                .GetWithoutChildren()
                .ToList();

            this.executeMapper(items);
        }

    }
}

