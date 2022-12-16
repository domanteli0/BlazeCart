using System;
using Models;

namespace CategoryMap.Implementations
{
	public interface ICategoryMap
	{
        /// <summary>
        /// Remaps all items within a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public void Map(List<Category> root_cats, IDictionary<string, Category> into);

        public void Map(Category root_cat, IDictionary<string, Category> into)
        {
            foreach (var cat in root_cat.SubCategories)
            {
                Map(cat, into);
            }
        }

	}
}

