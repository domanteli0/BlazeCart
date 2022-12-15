using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Api.Repositories;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Models;
using Scraper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly  IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
      

        [HttpGet("{index}/{count}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        public async Task<IActionResult> GetRangeOfItems(int index, int count)
        {
            var items = await _itemRepository.GetRangeOfItemsAsync(index, count);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(items);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetItemByIdAsync(Guid id)
        {
            if (!_itemRepository.IsItemActiveAsync(id))
                return NotFound();
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(item);
        }

        [HttpGet("{index}/{count}/cats")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<String>))]
        public async Task<IActionResult> GetItemsCat(int index, int count)
        {
            var cats = await _itemRepository.GetItemsCat(index, count);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cats);

        }

        [HttpGet("{name}/{category}/{price}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public async Task<IActionResult> GetCheapestItem(string name, string category, double price)
        {
            var item = await _itemRepository.GetCheapestItem(name, category, price);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int pr = item.Price / 0;
            return Ok(item);
        }
       


    }
}

