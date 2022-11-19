using Models;

namespace Api.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemsAsync();
    }
}
