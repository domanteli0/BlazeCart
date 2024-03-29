﻿using DB;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ScraperDbContext _context;

        public CategoryRepository(ScraperDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var records = await _context.Categories.ToListAsync();
            return records;
        }

        public bool IsCategoryActive(Guid id)
        {
            var res = _context.Categories.Any(c => c.Id == id);
            return res;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }
        public async Task<List<Item>> GetItemsByCategoryIdAsync(Guid id)
        {
            var items = await _context.Items.Where(i => i.Category.Id == id).ToListAsync();
            return items;
        }

        public async Task<List<Item>> GetRangeOfItemsByCategoryIdAsync(Guid id, int index, int count)
        {
            var items = await _context.Items.Where(i => i.Category.Id == id).ToListAsync();
            var range = items.GetRange(index, count);
            return range;
        }

        public async Task<List<Category>> GetCategoriesByNameAsync(string name)
        {
            var category = await _context.Categories.Where(c => c.NameLT.Contains(name)).ToListAsync();
            return category;
        }
        public async Task<IEnumerable<Category>> GetRangeOfCategoriesAsync(int index, int count)
        {
            var res = await _context.Categories.ToListAsync();
            var records = res.GetRange(index, count);
            return records;
        }
    }
}