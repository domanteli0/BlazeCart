using CategoryMap;

namespace Tests1.CategoryMap
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

			Assert.True(true);
        }
	}
}

