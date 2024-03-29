﻿using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

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

        [HttpGet("{name}/{category}/{price}/{amount}/{merch}/{comparedMerch}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public async Task<IActionResult> GetCheapestItem(string name, string category, double price, double amount, int merch, int comparedMerch)
        {
            var item = await _itemRepository.GetCheapestItem(name, category, price, amount, merch, comparedMerch);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(item);
        }
       


    }
}

