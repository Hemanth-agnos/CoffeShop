using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IItemService
    {
        bool AddItem(Item item);

        List<Item> GetAvailableItems();

        List<Item> GetAllItems();

        Item GetItem(int itemId);

        bool UpdateItem(Item item);

        bool DeleteItem(int itemId);

        List<Discount> GetAllDiscounts();

        List<Discount> GetAllItemDiscounts(int ItemId);

        List<OrderItem> GetAllComboDiscountItems(int ItemId);

        bool AddDiscount(Discount discount);

        bool DeleteDiscount(int discountId);
    }
}
