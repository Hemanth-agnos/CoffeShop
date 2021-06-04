using Models;
using PetaPoco;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        public Database DB { get; set; }

        private readonly AutoMapper.IMapper Mapper;

        public ICartService CartService;

        public OrderService(Database db, AutoMapper.IMapper mapper, ICartService cartService)
        {
            this.DB = db;
            this.Mapper = mapper;
            this.CartService = cartService;
        }

        public bool PlaceOrder(int cartId)
        {
            var cart = this.CartService.GetCartDetails(cartId);
            var newOrder = new Order()
            {
                CustomerId = cart.CustomerId,
                Items = cart.Items,
                Status = OrderStatus.Placed
            };

            newOrder.CalculateTotalPrice();

            var orderModel = this.Mapper.Map<Order, DataModel.Order>(newOrder);

            return Convert.ToInt32(this.DB.Insert(orderModel)) > 0;
        }

        public List<Order> GetAllOrders()
        {
            return this.Mapper.Map<List<DataModel.Order>, List<Order>>(this.DB.Fetch<DataModel.Order>(string.Empty));
        }

        public OrderStatus GetOrderStatus(int orderId)
        {
            var order = this.GetOrderDetails(orderId);
            return order.Status;
        }

        public Order GetOrderDetails(int orderId)
        {
            return this.Mapper.Map<DataModel.Order, Order>(this.DB.SingleOrDefault<DataModel.Order>("Where Id = @0", orderId));
        }

        public bool UpdateOrder(Order order)
        {
            var orderModel = this.Mapper.Map<Order, DataModel.Order>(order);
            return this.DB.Update(orderModel) > 0;
        }

        public bool UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = this.GetOrderDetails(orderId);
            order.Status = status;
            var orderModel = this.Mapper.Map<Order, DataModel.Order>(order);
            return this.DB.Update(orderModel, new string[] { "Status" }) > 0;
        }

        public bool DeleteOrder(int orderId)
        {
            return this.DB.Delete<DataModel.Order>(orderId) > 0;
        }
    }
}
