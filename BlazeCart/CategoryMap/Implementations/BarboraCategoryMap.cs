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
            this.addMapper("Kitos krupos", new() { ("(?i).*", into["Kitos krupos"]) });
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

