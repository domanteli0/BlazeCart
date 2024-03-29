﻿using Models;

namespace Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        bool IsCategoryActive(Guid id);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<List<Category>> GetCategoriesByNameAsync(string name);
        Task<List<Item>> GetItemsByCategoryIdAsync(Guid id);
        Task<IEnumerable<Category>> GetRangeOfCategoriesAsync(int index, int count);
        Task<List<Item>> GetRangeOfItemsByCategoryIdAsync(Guid id, int index, int count);

    }
}
