using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public float TotalPrice { get; set; }

        public void CalculateTotalPrice()
        {
            this.TotalPrice = 0;
            foreach(var item in Items)
            {
                this.TotalPrice += item.TotalItemprice;
            }
        }
    }

    public enum OrderStatus
    {
        Placed,
        InProgress,
        Completed,
        Deleted
    }
}
