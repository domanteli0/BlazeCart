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
            this.addMapper("Mėsos ir paukštienos konservai", new() { ("(?i).*", into["Mėsos ir paukštienos konservai"]) });
            this.addMapper("Paštetai ir kiti gaminiai", new() { ("(?i).*", into["Paštetai ir kiti gaminiai"]) });
            this.addMapper("Virti, kepti ir sūdyti gaminiai", new() { ("(?i).*", into["Virti, kepti ir sūdyti gaminiai"]) });
            this.addMapper("Vytinti mėsos gaminiai", new() { ("(?i).*", into["Vytinti mėsos gaminiai"]) });
            this.addMapper("Karštai rūkyti gaminiai", new() { ("(?i).*", into["Karštai rūkyti gaminiai"]) });
            this.addMapper("Šaltai rūkyti gaminiai", new() { ("(?i).*", into["Šaltai rūkyti gaminiai"]) });
            this.addMapper("Virtos dešrelės", new() { ("(?i).*", into["Virtos dešrelės"]) });
            this.addMapper("Virtos dešros", new() { ("(?i).*", into["Virtos dešros"]) });
            this.addMapper("Vytintos dešros", new() { ("(?i).*", into["Vytintos dešros"]) });
            this.addMapper("Dešrelės griliui", new() { ("(?i).*", into["Dešrelės griliui"]) });
            this.addMapper("Marinuota mėsa", new() { ("(?i).*", into["Marinuota mėsa"]) });
            this.addMapper("Antiena", new() { ("(?i).*", into["Antiena"]) });
            this.addMapper("Kalakutiena", new() { ("(?i).*", into["Kalakutiena"]) });
            this.addMapper("Vištiena", new() { ("(?i).*", into["Vištiena"]) });
            this.addMapper("Aviena", new() { ("(?i).*", into["Aviena"]) });
            this.addMapper("Triušiena", new() { ("(?i).*", into["Triušiena"]) });
            this.addMapper("Jautiena", new() { ("(?i).*", into["Jautiena"]) });
            this.addMapper("Kiauliena", new() { ("(?i).*", into["Kiauliena"]) });
            this.addMapper("Saldžios bandelės", new() { ("(?i).*", into["Saldžios bandelės"]) });
            this.addMapper("Kiti duonos gaminiai", new() { ("(?i).*", into["Kiti duonos gaminiai"]) });
            this.addMapper("Trapučiai ir kiti paplotėliai", new() { ("(?i).*", into["Trapučiai ir kiti paplotėliai"]) });
            this.addMapper("Šakočiai ir skruzdėlynai", new() { ("(?i).*", into["Šakočiai ir skruzdėlynai"]) });
            this.addMapper("Pyragaičiai ir desertai", new() { ("(?i).*", into["Pyragaičiai ir desertai"]) });
            this.addMapper("Plokštainiai ir vyniotiniai", new() { ("(?i).*", into["Plokštainiai ir vyniotiniai"]) });
            this.addMapper("Tortai", new() { ("(?i).*", into["Tortai"]) });
            this.addMapper("Sumuštinių duona ir duonelės", new() { ("(?i).*", into["Sumuštinių duona ir duonelės"]) });
            this.addMapper("Batonas", new() { ("(?i).*", into["Batonas"]) });
            this.addMapper("Šviesi duona", new() { ("(?i).*", into["Šviesi duona"]) });
            this.addMapper("Tamsi duona", new() { ("(?i).*", into["Tamsi duona"]) });
            this.addMapper("Actas ir koncentruotos citrinų sultys", new() { ("(?i).*", into["Actas ir koncentruotos citrinų sultys"]) });
            this.addMapper("Kitas aliejus", new() { ("(?i).*", into["Kitas aliejus"]) });
            this.addMapper("Kokosų aliejus", new() { ("(?i).*", into["Kokosų aliejus"]) });
            this.addMapper("Rapsų aliejus", new() { ("(?i).*", into["Rapsų aliejus"]) });
            this.addMapper("Alyvuogių aliejus", new() { ("(?i).*", into["Alyvuogių aliejus"]) });
            this.addMapper("Saulėgrąžų aliejus", new() { ("(?i).*", into["Saulėgrąžų aliejus"]) });
            this.addMapper("Kiti padažai", new() { ("(?i).*", into["Kiti padažai"]) });
            this.addMapper("Sojų padažai", new() { ("(?i).*", into["Sojų padažai"]) });
            this.addMapper("Krienai", new() { ("(?i).*", into["Krienai"]) });
            this.addMapper("Garstyčios", new() { ("(?i).*", into["Garstyčios"]) });
            this.addMapper("Majoneziniai padažai", new() { ("(?i).*", into["Majoneziniai padažai"]) });
            this.addMapper("Pomidorų padažai ir pasta", new() { ("(?i).*", into["Pomidorų padažai ir pasta"]) });
            this.addMapper("Kečupai", new() { ("(?i).*", into["Kečupai"]) });
            this.addMapper("Cukrus, saldikliai ir druska", new() { ("(?i).*", into["Cukrus, saldikliai ir druska"]) });
            this.addMapper("Plikyti ryžiai", new() { ("(?i).*", into["Plikyti ryžiai"]) });
            this.addMapper("Basmati ir kvapieji ryžiai", new() { ("(?i).*", into["Basmati ir kvapieji ryžiai"]) });
            this.addMapper("Rudieji ir laukiniai ryžiai", new() { ("(?i).*", into["Rudieji ir laukiniai ryžiai"]) });
            this.addMapper("Trumpagrūdžiai ir apvaliagrūdžiai ryžiai", new() { ("(?i).*", into["Trumpagrūdžiai ir apvaliagrūdžiai ryžiai"]) });
            this.addMapper("Ilgagrūdžiai ryžiai", new() { ("(?i).*", into["Ilgagrūdžiai ryžiai"]) });
            this.addMapper("Miltiniai mišiniai", new() { ("(?i).*", into["Miltiniai mišiniai"]) });
            this.addMapper("Kiti miltai", new() { ("(?i).*", into["Kiti miltai"]) });
            this.addMapper("Pilno grūdo miltai", new() { ("(?i).*", into["Pilno grūdo miltai"]) });
            this.addMapper("Kvietiniai miltai", new() { ("(?i).*", into["Kvietiniai miltai"]) });
            this.addMapper("Kitos kruopos", new() { ("(?i).*", into["Kitos kruopos"]) });
            this.addMapper("Manų kruopos", new() { ("(?i).*", into["Manų kruopos"]) });
            this.addMapper("Perlinės kruopos", new() { ("(?i).*", into["Perlinės kruopos"]) });
            this.addMapper("Kvietinės ir miežinės kruopos", new() { ("(?i).*", into["Kvietinės ir miežinės kruopos"]) });
            this.addMapper("Grikiai", new() { ("(?i).*", into["Grikiai"]) });
            this.addMapper("Ilgieji ir plokštieji makaronai", new() { ("(?i).*", into["Ilgieji ir plokštieji makaronai"]) });
            this.addMapper("Grybai", new() { ("(?i).*", into["Grybai"]) });
            this.addMapper("Avokadai", new() { ("(?i).*", into["Avokadai"]) });
            this.addMapper("Kaulavaisiai", new() { ("(?i).*", into["Kaulavaisiai"]) });
            this.addMapper("Melionai", new() { ("(?i).*", into["Melionai"]) });
            this.addMapper("Egzotiniai vaisiai", new() { ("(?i).*", into["Egzotiniai vaisiai"]) });
         
            this.addForUnmapped(into["UNMAPPED"]);

            var items = root_cat
                .GetWithoutChildren()
                .ToList();

            this.executeMapper(items);
        }

    }
}

