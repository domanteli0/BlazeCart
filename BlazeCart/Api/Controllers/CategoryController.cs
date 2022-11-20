using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllItems()
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
        [ProducesResponseType(200, Type = typeof(List<Item>))]
        [ProducesResponseType(400)]
        public IActionResult GetItemsByCategoryIdAsync(Guid id)
        {
            if (!_categoryRepository.IsCategoryActive(id))
                return NotFound();
            var items =  _categoryRepository.GetItemsByCategoryIdAsync(id);
           if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(items);
        }

    }
}
