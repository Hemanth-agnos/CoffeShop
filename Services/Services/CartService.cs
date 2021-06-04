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
    public class CartService : ICartService
    {
        public Database DB { get; set; }

        private readonly AutoMapper.IMapper Mapper;

        public IItemService ItemService;

        public CartService(Database db, AutoMapper.IMapper mapper, IItemService itemService)
        {
            this.DB = db;
            this.Mapper = mapper;
            this.ItemService = itemService;
        }

        public bool AddItem(int itemId, int cartId)
        {
            var cart = this.Mapper.Map<DataModel.CART,Cart>(this.DB.SingleOrDefault<DataModel.CART>("Where Id = @0", cartId));
            var currentItem = cart.Items.Find(it => it.Id == itemId);
            if (currentItem != null)
            {
                currentItem.Quantity += 1;
            }
            else
            {
                var item = this.Mapper.Map<Item, OrderItem>(this.ItemService.GetItem(itemId));
                item.Quantity = 1;
                cart.Items.Add(item);
            }
            var cartModel = this.Mapper.Map<Cart, DataModel.CART>(cart);
            var result = currentItem != null ? this.DB.Update(cartModel, new string[] { "Quantity" }) > 0 : Convert.ToInt32(this.DB.Insert(cartModel)) > 0;
            return result;
        }

        public int GetCartId(int customerId)
        {
            var cart = this.DB.SingleOrDefault<DataModel.CART>("Where CustomerId = @0", customerId);
            if(cart == null)
            {
                return this.InsertCart(customerId);
            }
            else
            {
                return cart.Id;
            }
        }

        public Cart GetCart(int cartId)
        {
            return this.Mapper.Map<DataModel.CART, Cart>(this.DB.SingleOrDefault<DataModel.CART>("Where Id = @0", cartId));
        }

        public Cart GetCartDetails(int cartId)
        {
            var cart = this.Mapper.Map<DataModel.CART, Cart>(this.DB.SingleOrDefault<DataModel.CART>("Where Id = @0", cartId));
            cart = this.UpdateDiscountForCartItems(cart);
            this.UpdateCart(cart);
            return cart;
        }

        public bool UpdateItemQuantity(int cartId, int itemId, int quantity)
        {
            var cart = this.GetCart(cartId);
            var currentItem = cart.Items.Find(it => it.Id == itemId);
            if (currentItem != null)
            {
                if (quantity == 0)
                {
                    cart.Items.Remove(currentItem);
                }
                else
                {
                    currentItem.Quantity = quantity;
                }
            }
            var cartModel = this.Mapper.Map<Cart, DataModel.CART>(cart);
            return this.DB.Update(cartModel, new string[] { "Quantity", "Items" }) > 0;
        }
        public bool DeleteCartItem(int cartId, int itemId)
        {
            return this.UpdateItemQuantity(cartId, itemId, 0);
        }

        public void ClearCart(int cartId)
        {
            var cart = this.GetCart(cartId);
            cart.Items.Clear();
            var cartModel = this.Mapper.Map<Cart, DataModel.CART>(cart);
            this.DB.Update(cartModel, new string[] { "Quantity", "Items" });
        }

        private void UpdateCart(Cart cart)
        {
            var cartModel = this.Mapper.Map<Cart, DataModel.CART>(cart);
            this.DB.Update(cartModel, new string[] { "Quantity", "Items" });
        }

        private int InsertCart(int customerId)
        {
            var cart = new Cart()
            {
                Items = new List<OrderItem>(),
                CustomerId = customerId
            };
            var cartModel = this.Mapper.Map<Cart, DataModel.CART>(cart);
            return Convert.ToInt32(this.DB.Insert(cartModel));
        }

        private Cart UpdateDiscountForCartItems(Cart cart)
        {
            foreach (var item in cart.Items)
            {
                var discounts = this.ItemService.GetAllItemDiscounts(item.Id);
                foreach (var discount in discounts)
                {
                    if (discount.IsComboOffer)
                    {
                        var isOfferEligible = cart.Items.Where(it => discount.ComboItemsId.Contains(it.Id)).Count() > 0;
                        if (isOfferEligible)
                        {
                            item.DiscountDetails.Add(discount);
                        }
                    }
                    else
                    {
                        item.DiscountDetails.Add(discount);
                    }
                }
            }
            return cart;
        }
    }
}
