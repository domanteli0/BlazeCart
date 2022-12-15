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

