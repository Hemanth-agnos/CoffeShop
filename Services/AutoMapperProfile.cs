using AutoMapper;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Item, DataModel.Item>()
                .ReverseMap();

            CreateMap<Item, OrderItem>()
                .ForMember(dest => dest.DiscountDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Quantity, opt => opt.Ignore());

            CreateMap<Cart, DataModel.CART>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToJson()))
                .ReverseMap()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.FromJson<OrderItem>()));

            CreateMap<Order, DataModel.Order>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToJson()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (OrderStatus)src.Status))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.FromJson<OrderItem>()));

            CreateMap<Discount, DataModel.Discount>()
               .ForMember(dest => dest.ComboItemsId, opt => opt.MapFrom(src => src.ComboItemsId.ToJson()))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (DiscountType)src.Type))
               .ForMember(dest => dest.ComboItemsId, opt => opt.MapFrom(src => src.ComboItemsId.FromJson<List<int>>()));

            CreateMap<Customer, DataModel.Customer>()
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.ToJson()))
              .ReverseMap()
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.FromJson<Item>()));
        }
    }
}
