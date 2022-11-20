﻿using Api.Repositories;
using DB;
using Microsoft.AspNetCore.Mvc;
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
      

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Item>))]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemRepository.GetAllItemsAsync();
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



    }
}

