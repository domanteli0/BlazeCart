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

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            var records =  await _context.Items.Where(i => i.Id == id).FirstOrDefaultAsync();
            return records;
        }

        public async Task<List<Item>> GetItemsByNameAsync(string name)
        {
            var records = await _context.Items.Where(i => i.NameLT == name).ToListAsync();
            return records;
        }

        public async Task<List<Item>> GetItemsByCategoryAsync(Category category)
        {
            var records = await _context.Items.Where(i => i.Category.Id == category.Id).ToListAsync();
            return records;
        }

        public bool IsItemActiveAsync(Guid id)
        {
            var res = _context.Items.Any(i => i.Id == id);
            return res;
        }

        public async Task<double> GetItemPrice(Guid id)
        {
            var record = await _context.Items.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (record != null)
            {
                double price = record.Price;
                return price;
            }

            return 0;
        }

    }
}
