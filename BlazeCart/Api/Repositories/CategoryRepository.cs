using DB;
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

        public bool IsCategoryActiveAsync(Guid id)
        {
            var res = _context.Categories.Any(c => c.Id == id);
            return res;
        }
    }
}