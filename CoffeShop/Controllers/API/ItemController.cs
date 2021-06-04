using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeShop.Controllers.API
{
    [RoutePrefix("api/items")]
    public class ItemController : ApiController
    {
        public IItemService ItemService;
        public ItemController(IItemService itemService)
        {
            this.ItemService = itemService;
        }

        [HttpGet]
        [Route("available")]
        public IEnumerable<Item> GetAvailableItems()
        {
            return this.ItemService.GetAvailableItems();
        }

        [HttpGet]
        [Route("{id}")]
        // GET api/<controller>/5
        public Item GetItemById(int id)
        {
            return this.ItemService.GetItem(id);
        }

        [HttpPost]
        [Route("add")]
        // POST api/<controller>
        public bool Post([FromBody] Item item)
        {
            return this.ItemService.AddItem(item);
        }

        [HttpPut]
        [Route("update/{id}")]
        public bool Put(int id, [FromBody] Item item)
        {
            item.Id = id;
            return this.ItemService.UpdateItem(item);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public bool Delete(int id)
        {
            return this.ItemService.DeleteItem(id);
        }

        [HttpGet]
        [Route("discounts/{id}")]
        public IEnumerable<Discount> GetItemDiscounts(int id)
        {
            return this.ItemService.GetAllItemDiscounts(id);
        }

        [HttpGet]
        [Route("combodiscountitems/{id}")]
        public IEnumerable<OrderItem> GetDiscountComboItem(int id)
        {
            return this.ItemService.GetAllComboDiscountItems(id);
        }

        [HttpGet]
        [Route("discounts")]
        public IEnumerable<Discount> GetDiscounts()
        {
            return this.ItemService.GetAllDiscounts();
        }

        [HttpPost]
        [Route("adddiscount")]
        public bool AddDiscount([FromBody] Discount discount)
        {
            return this.ItemService.AddDiscount(discount);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("deletediscount{id}")]
        public bool DeleteDiscount(int id)
        {
            return this.ItemService.DeleteItem(id);
        }
    }
}