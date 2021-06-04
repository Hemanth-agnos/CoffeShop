using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public float Value { get; set; }
        public DiscountType Type { get; set; }
        public bool IsComboOffer { get; set; }
        public List<int> ComboItemsId { get; set; }
    }

    public enum DiscountType
    {
        Percentage,
        FlatPrice
    }
}
