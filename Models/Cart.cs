using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }

        public float TotalPrice { 
            get
            {
                float sum = 0;
                foreach (var item in Items)
                {
                    sum += item.TotalItemprice;
                }
                return sum;
            } 
        }
    }
}
