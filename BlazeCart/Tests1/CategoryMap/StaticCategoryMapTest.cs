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
			var test = StaticCategoryTree.GetCategoryDict();

			// If it doesn't throw an Exception it passes.
			Assert.True(true);
        }
	}
}

