using Models;

namespace Api.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetRangeOfItemsAsync(int index, int count);
        Task<List<Item>> GetAllItemsAsync();

        Task<Item> GetItemByIdAsync(Guid id);
        Task<List<Item>> GetItemsByNameAsync(string name);

        bool IsItemActiveAsync(Guid id);

    }
}
