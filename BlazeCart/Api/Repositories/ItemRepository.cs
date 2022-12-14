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
        public async Task<Item> GetCheapestItem(string name, string category, double price, double amount)
        {
            Item cheapestItem = new();   
            //Get items from the specific category:
           var records = await _context.Items.Where(i => i.Category.NameLT == category).ToListAsync();
            //Applying filter:
            for (int i = 0; i < records.Count; i++)
            {
                if (IsInvalidItemFilter(records[i]))
                {
                    records.Remove(records[i]);
                    i--;
                }
            }
            //Sort to find unique categories first:
            List<Item> _sortedList = records.OrderBy(x => x.NameLT).ToList<Item>();
            Dictionary<Item, string> refactoredD = _algorithmService.GetItemDictionary(_sortedList);
            HashSet<String> hs = _algorithmService.GetSetOfUnique(refactoredD);
            refactoredD = _algorithmService.RefactorDictionaryToUnique(refactoredD, hs);
            Item comparedItem = new();
            Category category1 = new();
            comparedItem.Category = category1;
     
            comparedItem.NameLT = name;
            comparedItem.Category.NameLT = category;
            comparedItem.Price = (int)(price * 100);
            comparedItem.Ammount = (float?)amount;
            KeyValuePair<Item, string> comparedPair = new KeyValuePair<Item, string>(comparedItem, _algorithmService.refactorItemName(comparedItem.NameLT).ToLower());
            cheapestItem = _algorithmService.GetCheapestItemAlgorithm(comparedPair, refactoredD);
            return cheapestItem;
        }

        private Boolean IsInvalidItemFilter(Item item)
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
