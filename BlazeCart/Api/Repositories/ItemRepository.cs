using DB;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query;
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
        public async Task<Item> GetCheapestItem(string name, string category, double price)
        {
            double min = price;
            Item cheapestItem = new();
            
           var records = await _context.Items.Where(i => i.Category.NameLT == category).ToListAsync();
            foreach(var item in records)
            {
                double priceValue = item.Price;
                if (priceValue / 100 <= min && item.Price != 0)
                {
                    cheapestItem.Price = item.Price;
                    cheapestItem.PricePerUnitOfMeasure = item.PricePerUnitOfMeasure;
                    cheapestItem.MeasureUnit = item.MeasureUnit;
                    cheapestItem.Image = item.Image;
                    cheapestItem.Merch = item.Merch;
                    cheapestItem.NameLT = item.NameLT;
                    cheapestItem.Description = item.Description;
                    cheapestItem.Ammount = item.Ammount;
                   
                    min = item.Price;
                }
                
            }
            return cheapestItem;


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
