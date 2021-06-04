using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem : Item
    {
        public int Quantity;
        public List<Discount> DiscountDetails  { get; set; }

        public float DiscountAmount {
            get
            {
                float maxDiscount = 0;
                foreach(var discount in DiscountDetails)
                {
                    float discountAmt = 0;
                    if (discount.Type == DiscountType.Percentage)
                    {
                        discountAmt = (this.Price * discount.Value);
                    }
                    else if(discount.Type == DiscountType.FlatPrice)
                    {
                        discountAmt = (this.Price - discount.Value);
                    }
                    if(maxDiscount < discountAmt)
                    {
                        maxDiscount = discountAmt;
                    }
                }
                return maxDiscount;
            }
        }

        public float TotalItemprice
        {
            get
            {
                return (((this.Price) + (this.Price * this.TaxRate) - this.DiscountAmount)*this.Quantity);
            }
        }
    }
}
