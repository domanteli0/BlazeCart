using System;
using System.Linq;
using Models;

namespace CategoryMap
{
	public static class StaticCategoryTree
	{
        private static Dictionary<string, Category> CategoryDict = new()
		{
			// Pieno produktai
			{ "Pasterizuotas pienas", new() { NameLT = "Pasterizuotas pienas" } },
			{ "Pieno gėrimai", new() { NameLT = "Pieno gėrimai" } },
			{ "Ilgo galiojimo pienas", new() { NameLT = "Ilgo galiojimo pienas" } },
			{ "Sutirštintas pienas", new() { NameLT = "Sutirštintas pienas" } },
			{ "Sojų pienas", new() { NameLT = "Sojų pienas" } },
			{ "Augalinis pienas", new() { NameLT = "Augalinis pienas" } },
			{"Sviestas", new() { NameLT =  "Sviestas"} },
			{"Tepieji riebalų mišiniai", new() { NameLT = "Tepeji riebalų mišiniai" } },
			{"Margarinas", new() {NameLT = "Margarinas"} },
			{"Augaliniai sūrio pakaitalai", new() { NameLT = "Augaliniai sūrio pakaitalai" } },
			{"Augaliniai majonezo pakaitalai", new(){ NameLT = "Augaliniai majonezo pakaitalai" } },
			{"Augaliniai gėrimai", new() { NameLT = "Augaliniai gėrimai"} },
			{"Augaliniai grietinėlės pakaitalai", new() { NameLT = "Augaliniai grietinėlės pakaitalai" } },
			{"Augaliniai jogurto ir desertų pakaitalai", new() {NameLT = "Augaliniai jogurto ir desertų pakaitalai" } },
			{"Augaliniai sviesto pakaitalai", new() { NameLT = "Augaliniai sviesto pakaitalai" } },
			{"Pienas ir gėrimai be laktozės", new() { NameLT = "Pienas ir gėrimai be laktozės" } },
			{"Grietinė be laktozės", new() { NameLT = "Grietinė be laktozės" } },
			{"Jogurtai ir desertai be laktozės", new() { NameLT = "Jogurtai ir desertai be laktozės" } },
			{"Sūriai be laktozės", new() { NameLT = "Sūriai be laktozės"} },
			{"Sviestas ir margarinas be laktozės", new() { NameLT = "Sviestas ir margarinas be laktozės" } },
			{"Varškė be laktozės", new() { NameLT = "Varškė be laktozės" } },
			{"Grietinė", new() { NameLT = "Grietinė" } },
			{"Grietinėlė", new() { NameLT = "Grietinėlė" } },
			{"Fermentiniai sūriai", new() { NameLT = "Fermentiniai sūriai" } },
			{"Kietieji sūriai", new() { NameLT = "Kietieji sūriai" } },
			{"Pelėsiniai sūriai", new() { NameLT = "Pelėsiniai sūriai" } },
			{"Lydyti sūriai", new() { NameLT = "Lydyti sūriai" } },
			{"Varškės sūriai", new() { NameLT = "Varškės sūriai" } },
			{"Ožkų ir avių pieno sūriai", new() { NameLT = "Ožkų ir avių pieno sūriai" } },
			{"Mocarelos ir buratos sūriai", new() { NameLT = "Mocarelos ir buratos sūriai" } },
			{"Maskarponės ir rikotos sūriai", new() { NameLT = "Maskarponės ir rikotos sūriai" } },
			{"Fetos ir brinzos sūriai", new() { NameLT = "Fetos ir brinzos sūriai" } },
			{"Tepamieji sūriai", new() { NameLT = "Tepamieji sūriai" } },
			{"Sūriai, skirti kepti", new() { NameLT = "Sūriai, skirti kepti" } },
			{"Sūrio užkandžiai ir sūrio lazdelės", new() {NameLT = "Sūrio užkandžiai ir sūrio lazdelės" } },
			{"Majonezas", new() { NameLT = "Majonezas"} },
			{"Kefyras ir kefyro gėrimai", new() { NameLT = "Kefyras ir kefyro gėrimai" } },
			{"Rūgpienis", new() { NameLT = "Rūgpienis"} },
			{"Raugintos pasukos", new() { NameLT = "Raugintos pasukos" } },
			{"Jogurtai be pagardų", new() { NameLT = "Jogurtai be pagardų" } },
			{"Jogurtai su pagardais", new() { NameLT = "Jogurtai su pagardais" } },
			{"Geriamieji jogurtai", new() { NameLT = "Gėriamieji jogurtai" } },
			{"Desertiniai batonėliai", new() { NameLT = "Desertiniai batonėliai" } },
			{"Desertai", new() { NameLT = "Desertai" } },
			{"Varškė", new() { NameLT = "Varškė" } },
			{"Grūdėta varškė", new() { NameLT = "Grūdėta varškė" } },
			{"Tepamoji varškė", new() { NameLT = "Tepamoji varškė" } },
			{"Desertinė varškė", new() { NameLT = "Desertinė varškė" } },
			{"Varškės sūreliai", new() { NameLT = "Varškės sūreliai" } },
			{"Vištų kiaušinai", new() { NameLT = "Vištų kiaušiniai" } },
			{"Putpelių kiaušiniai", new() { NameLT = "Putpelių kiaušiniai"} },





			// TODO:
			// Daržovės

			// Vaisiai

			// Mėsa

			// Kiaušiniai

			// ...

			//{ "Kiti", new() { NameLT = "Kiti" } },

			// This category contains items, for which no appropiate category was found
			{ "UNMAPPED", new() { NameLT = "UNMAPPED" } },
		};

		public static Dictionary<string, Category> GetCategoryDict() =>
			CategoryDict.ToDictionary(
				c => (string)c.Key.Clone(), c => (Category)c.Value.Clone()
			);
	}
}

