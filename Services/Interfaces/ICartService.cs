using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartService
    {
        bool AddItem(int itemId, int cartId);

        Cart GetCartDetails(int cartId);

        bool UpdateItemQuantity(int cartId, int itemId, int quantity);

        bool DeleteCartItem(int cartId, int itemId);

        void ClearCart(int cartId);

        int GetCartId(int customerId);
    }
}
