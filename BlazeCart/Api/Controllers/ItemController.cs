using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Scraper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/items")]
    public class ItemController : Controller
    {
        // GET: api/values
        [HttpGet(Name = "api/items")]
        public IEnumerable<Item> Get()
        {
            // TODO: Query the database once DB is implemented
            return (IEnumerable<Item>) (new IKIScraper()).Items;
            //return new List<string>();
        }
    }
}

