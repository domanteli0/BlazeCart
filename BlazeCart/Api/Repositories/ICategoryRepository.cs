using Models;

namespace Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();

        bool IsCategoryActiveAsync(Guid id);
    }
}
