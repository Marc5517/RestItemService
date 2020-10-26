using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModelLib.Model;
using FilterItem = ModelLib.Model.FilterItem;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestItemService.Controllers
{
    [Route("api/localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>()
        {
            new Item(1,"Bread","Low",33),
            new Item(2,"Bread","Middle",21),
            new Item(3,"Beer","low",70.5),
            new Item(4,"Soda","High",21.4),
            new Item(5,"Milk","Low",55.8)
        };

        [HttpGet] // Http request Method
        [Route("{id}")] // Http request URI
        [ProducesResponseType(StatusCodes.Status200OK)] // http response statuscode
        [ProducesResponseType(StatusCodes.Status404NotFound)] // http response statuscode
        [EnableCors("AllowAnyOriginGetPost")]
        public IActionResult Get(int id)
        {
            if (items.Exists(i => i.Id == id))
            {
                return Ok(items.Find(i => i.Id == id));
            }
            return NotFound($"item id {id} not found");
        }

        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(String substring)
        {
            List<Item> lItem = items.FindAll(i => i.Name.Contains(substring));
            return lItem;
        }

        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetFromAnotherSubstring(String substring)
        {
            List<Item> lItem = items.FindAll(i => i.Quality.Contains(substring));
            return lItem;
        }

        [HttpGet]
        [Route("Search")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {
            List<Item> gwfList = null;
            if (filter.LowQuantity != 0.0)
            {
                gwfList = items.FindAll(i => i.Quantity.Equals(filter.LowQuantity) && i.Quantity.Equals(filter.HighQuantity));
            }
            else
            {
                gwfList = items;
            }
            return gwfList;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }


        // GET api/<ItemController>/5
        //[HttpGet]
        //[Route("{id}")]
        private Item GetItem(int id)
        {
            return items.Find(i => i.Id == id);
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            items.Add(value);
        }

        // PUT api/<ItemController>/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = GetItem(id);
            if (item != null)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = GetItem(id);
            items.Remove(item);
        }
    }
}
