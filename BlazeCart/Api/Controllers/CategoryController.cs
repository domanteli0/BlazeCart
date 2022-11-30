using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categories);

        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
        {
            if (!_categoryRepository.IsCategoryActive(id))
                return NotFound();
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpGet("{id}/items")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        [ProducesResponseType(400)]
        public async Task <IActionResult> GetItemsByCategoryIdAsync(Guid id)
        {
            if (!_categoryRepository.IsCategoryActive(id))
                return NotFound();
            var items =  _categoryRepository.GetItemsByCategoryIdAsync(id);
           if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(items);
        }

        [HttpGet("{name}/categories")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoriesByNameAsync(string name)
        {
            var categories =  await _categoryRepository.GetCategoriesByNameAsync(name);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categories);
        }

        [HttpGet("{index}/{count}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRangeOfCategories(int index, int count)
        {
            var items = await _categoryRepository.GetRangeOfCategoriesAsync(index, count);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(items);

        }

    }
}
