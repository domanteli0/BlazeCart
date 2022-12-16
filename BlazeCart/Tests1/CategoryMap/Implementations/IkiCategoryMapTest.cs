using System;
using CategoryMap.Implementations;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Map = CategoryMap;
using Models;
using CategoryMap;

namespace Tests1.CategoryMap.Implementations
{
	public class IkiCategoryMapTest
    {
		private IkiCategoryMap _categoryMap;

		public IkiCategoryMapTest()
		{
			_categoryMap =
				new(
					A.Fake<Logger<Map.Implementations.IkiCategoryMap>>()
				);
        }

		[Fact]
		public void DoesNotAccessDictionaryWithInvalidKey()
		{
			//var static_keys = Map.StaticCategoryTree.GetCategoryDict().Select(kvp => kvp.Key);

			_categoryMap.Map(new List<Category>(), StaticCategoryTree.GetCategoryDict());
			var barbora_keys = _categoryMap._map_store.Select(i => i.Item1);

			foreach (var barb_key in barbora_keys)
			{
				Console.WriteLine(barb_key);
				var _ = Map.StaticCategoryTree.GetCategoryDict()[barb_key];
            }

            // If it doesn't throw an Exception it passes.
            Assert.True(true);
        }
	}
}

