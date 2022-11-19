using Models;

namespace Api.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemsAsync();

        Task<Item> GetItemByIdAsync(Guid id);
        Task<List<Item>> GetItemsByNameAsync(string name);

        Task<List<Item>> GetItemsByCategoryAsync(Category category);

        bool IsItemActiveAsync(Guid id);

        Task<double> GetItemPrice(Guid id);
    }
}
