using DB;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ScraperDbContext _context;

        public ItemRepository(ScraperDbContext context)
        {
            _context = context;
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            var records = await _context.Items.ToListAsync();
            return records;
        }
    }
}
