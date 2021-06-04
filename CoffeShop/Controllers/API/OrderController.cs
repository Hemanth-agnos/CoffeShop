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
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        public IOrderService OrderService;
        public OrderController(IOrderService orderService)
        {
            this.OrderService = orderService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Order> GetAllOrders()
        {
            return this.OrderService.GetAllOrders();
        }

        [HttpGet]
        [Route("{id}")]
        public Order GetOrderById(int id)
        {
            return this.OrderService.GetOrderDetails(id);
        }

        [HttpGet]
        [Route("{id}/status")]
        public OrderStatus GetOrderStatus(int id)
        {
            return this.OrderService.GetOrderStatus(id);
        }

        [HttpPost]
        [Route("add/{cartId}")]
        public bool Post(int cartId)
        {
            return this.OrderService.PlaceOrder(cartId);
        }

        [HttpPut]
        [Route("{id}/updatestatus")]
        public bool UpdateOrderStatus(int id, OrderStatus status)
        {
            return this.OrderService.UpdateOrderStatus(id, status);
        }

        [HttpDelete]
        [Route("{id}/delete")]
        public bool DeleteOrder(int id)
        {
            return this.OrderService.DeleteOrder(id);
        }
    }
}