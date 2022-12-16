using DB;
using Microsoft.EntityFrameworkCore;
using Models;
using Api.Services;
namespace Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ScraperDbContext _context;
        private readonly IAlgorithmService _algorithmService;

        public ItemRepository(ScraperDbContext context, IAlgorithmService algorithmService)
        {
            _context = context;
            _algorithmService = algorithmService;
            
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
        public async Task<Item> GetCheapestItem(string name, string category, double price, double amount, int merch, int comparedMerch)
        {
            double min = price;
            Item cheapestItem = new();

            var records = await _context.Items.Where(i => i.Category.NameLT == category && i.Merch == (Merchendise.Merch)merch).ToListAsync();
            for (int i = 0; i < records.Count; i++)
            {
                if (IsInvalidItemFilter(records[i]))
                {
                    records.Remove(records[i]);
                    i--;
                }
            }

            List<Item> sortedList = records.OrderBy(x => x.NameLT).ToList();

            Item comparedItem = new();
            comparedItem.NameLT = name;
            comparedItem.Price = (int)(price * 100);
            comparedItem.Ammount = (float?)amount;
            comparedItem.Merch = (Merchendise.Merch)comparedMerch;
     

            comparedItem.NameLT = _algorithmService.refactorItemName(comparedItem.NameLT).ToLower();
            cheapestItem = _algorithmService.GetCheapestItemAlgorithm(comparedItem, sortedList);
            return cheapestItem;
        }

        private bool IsInvalidItemFilter(Item item)
        {
            if(item.Price == 0 || item.NameLT == null || item.NameLT.Length < 4)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<String>> GetItemsCat(int index, int count)
        {
            var resWithCat = await _context.Items.Include(e => e.Category).ToListAsync();
            var res = resWithCat.GetRange(index, count);
            List<String> categories = new List<String>();
            foreach(var item in res)
            {
                if(item.Category.NameLT != null)
                    categories.Add(item.Category.NameLT);
            }
           
            return categories;
        }

        public bool IsItemActiveAsync(Guid id)
        {
            var res = _context.Items.Any(i => i.Id == id);
            return res;
        }


    }
}
