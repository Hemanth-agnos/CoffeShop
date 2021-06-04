using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        bool PlaceOrder(int cartId);

        List<Order> GetAllOrders();

        OrderStatus GetOrderStatus(int orderId);

        Order GetOrderDetails(int orderId);

        bool UpdateOrder(Order order);

        bool UpdateOrderStatus(int orderId, OrderStatus status);

        bool DeleteOrder(int orderId);
    }
}
