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
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        public ICartService CartService;
        public CartController(ICartService cartService)
        {
            this.CartService = cartService;
        }

        [HttpGet]
        [Route("cartId/{customerId}")]
        public int GetCartId(int customerId)
        {
            return this.CartService.GetCartId(customerId);
        }

        [HttpGet]
        [Route("{id}")]
        public Cart GetCart(int id)
        {
            return this.CartService.GetCartDetails(id);
        }

        [HttpPost]
        [Route("{cartId}/addItem/{itemId}")]
        public bool AddItemToCart(int cartId, int ItemId)
        {
            return this.CartService.AddItem(ItemId, cartId);
        }

        [HttpPut]
        [Route("{cartId}/updateItem/{itemId}")]
        public bool UpdateCartItem(int cartId, int itemId, int quantity)
        {
            return this.CartService.UpdateItemQuantity(cartId, itemId, quantity);
        }

        [HttpPut]
        [Route("{cartId}/clearCart")]
        public void ClearCart(int cartId)
        {
            this.CartService.ClearCart(cartId);
        }

        [HttpDelete]
        [Route("{cartId}/deleteItem/{itemId}")]
        public bool DeleteCartItem(int cartId, int ItemId)
        {
            return this.CartService.DeleteCartItem(cartId, ItemId);
        }
    }
}