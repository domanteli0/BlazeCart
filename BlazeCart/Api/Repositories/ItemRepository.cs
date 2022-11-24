using DB;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ScraperDbContext _context;

        public ItemRepository(ScraperDbContext context)
        {
            _context = context;
        }
        public async Task <IEnumerable<Item>> GetRangeOfItemsAsync(int index, int count)
        {
            var res = await _context.Items.ToListAsync();
            var records = res.GetRange(index, count);
            return records;
        }

        public async Task <Item> GetItemByIdAsync(Guid id)
        {
            var records =  await _context.Items.Where(i => i.Id == id).FirstOrDefaultAsync();
            return records;
        }

        public async Task<List<Item>> GetItemsByNameAsync(string name)
        {
            var records = await _context.Items.Where(i => i.NameLT == name).ToListAsync();
            return records;
        }


        public bool IsItemActiveAsync(Guid id)
        {
            var res = _context.Items.Any(i => i.Id == id);
            return res;
        }


    }
}
