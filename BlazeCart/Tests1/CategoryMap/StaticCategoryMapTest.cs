using System;
using CategoryMap;
namespace Tests.CategoryMap
{
	public class StaticCategoryMapTest
	{
		public StaticCategoryMapTest()
		{
		}

		[Fact]
		public void DuplicateKey()
		{
			// If it doesn't throw an Exception it passes.
			var test = StaticCategoryTree.GetCategoryDict();

        }
	}
}

