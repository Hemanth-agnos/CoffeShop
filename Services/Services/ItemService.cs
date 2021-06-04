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
    public class ItemService : IItemService
    {
        public Database DB { get; set; }

        private readonly AutoMapper.IMapper Mapper;

        public ItemService(Database db, AutoMapper.IMapper mapper)
        {
            this.DB = db;
            this.Mapper = mapper;
        }

        public bool AddItem(Item item)
        {
            var ItemModel = this.Mapper.Map<Item, DataModel.Item>(item);
            return Convert.ToInt32(this.DB.Insert(ItemModel)) > 0;
        }

        public List<Item> GetAvailableItems()
        {
            var items = this.DB.Fetch<DataModel.Item>("Where IsAvailable = @0", true);
            return this.Mapper.Map<List<DataModel.Item>, List<Item>>(items);
        }


        public Item GetItem(int itemId)
        {
            var item = this.DB.SingleOrDefault<DataModel.Item>("Where Id = @0", itemId);
            return this.Mapper.Map<DataModel.Item, Item>(item);
        }

        public List<Item> GetAllItems()
        {
            var items = this.DB.Fetch<DataModel.Item>(string.Empty);
            return this.Mapper.Map<List<DataModel.Item>, List<Item>>(items);
        }

        public bool UpdateItem(Item item)
        {
            var itemModel = this.Mapper.Map<Item, DataModel.Item>(item);
            return this.DB.Update(itemModel,new string[] { "Name", "IsAvailable", "Price", "TaxRate" }) > 0;
        }
        public bool DeleteItem(int itemId)
        {
            return this.DB.Delete<DataModel.Item>(itemId) > 0;
        }

        public List<Item> GetItemsList(List<int> ids)
        {
            var Ids = string.Join(",", ids);
            var items = this.DB.Fetch<DataModel.Item>($"Where Id IN ({Ids})");
            return this.Mapper.Map<List<DataModel.Item>, List<Item>>(items);
        }

        public List<Discount> GetAllDiscounts()
        {
            var discounts = this.DB.Fetch<DataModel.Discount>(string.Empty);
            return this.Mapper.Map<List<DataModel.Discount>, List<Discount>>(discounts);
        }

        public List<Discount> GetAllItemDiscounts(int ItemId)
        {
            var discounts = this.DB.Fetch<DataModel.Discount>("Where ItemId = @0", ItemId);
            return this.Mapper.Map<List<DataModel.Discount>, List<Discount>>(discounts);
        }
        public List<OrderItem> GetAllComboDiscountItems(int ItemId)
        {
            var discounts = this.Mapper.Map<List<DataModel.Discount>, List<Discount>>(this.DB.Fetch<DataModel.Discount>("Where ComboItemsId Like '%,@0,%'", ItemId));
            List<OrderItem> Items = new List<OrderItem>();
            foreach(var discount in discounts)
            {
                var item = this.GetItem(discount.ItemId);
                var orderItem = this.Mapper.Map<Item, OrderItem>(item);
                orderItem.DiscountDetails = new List<Discount>();
                orderItem.DiscountDetails.Add(discount);
                Items.Add(orderItem);
            }

            return Items;
        }

        public bool AddDiscount(Discount discount)
        {
            var model = this.Mapper.Map<Discount, DataModel.Discount>(discount);
            return Convert.ToInt32(this.DB.Insert(model)) > 0;
        }
        public bool DeleteDiscount(int discountId)
        {
            return this.DB.Delete<DataModel.Discount>(discountId) > 0;
        }
    }
}
