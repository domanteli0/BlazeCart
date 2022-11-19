using Api.Repositories;
using DB;
using Microsoft.AspNetCore.Mvc;
using Models;
using Scraper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/items")]
    public class ItemController : Controller
    {
        private readonly  IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        // GET: api/values
        // public IEnumerable<Item> Get()
        //{
        // TODO: Query the database once DB is implemented
        //    return (IEnumerable<Item>) (new IKIScraper()).Items;
        //  return new List<string>();
        // }

        [HttpGet(Name = "api/items")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemRepository.GetAllItemsAsync();
            return Ok(items);

        }
    }
}

