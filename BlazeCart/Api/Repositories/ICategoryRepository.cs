using Models;

namespace Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();

        bool IsCategoryActive(Guid id);
        Task<Category> GetCategoryByIdAsync(Guid id);
        List<Item> GetItemsByCategoryIdAsync(Guid id);

    }
}
