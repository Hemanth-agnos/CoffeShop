using AutoMapper;
using AutoMapper.Configuration;
using DataModel;
using PetaPoco;
using Services;
using Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop
{
    public sealed class SimpleInjectorBootstrap
    {
        public static void InitializeContainer(Container container)
        {
            container.Register<IItemService, ItemService>(Lifestyle.Scoped);
            container.Register<IOrderService, OrderService>(Lifestyle.Scoped);
            container.Register<ICartService, CartService>(Lifestyle.Scoped);
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);
            container.Register<Database>(() => new CoffeShopDB("CoffeShop"),Lifestyle.Scoped);
            container.RegisterSingleton(() => GetMapper(container));
        }
        private static AutoMapper.IMapper GetMapper(Container container)
        {
            var mp = new MapperProvider(container);
            return mp.GetMapper();
        }
    }

    public class MapperProvider
    {
        private readonly Container _container;

        public MapperProvider(Container container)
        {
            _container = container;
        }

        public AutoMapper.IMapper GetMapper()
        {
            var mce = new MapperConfigurationExpression();
            mce.ConstructServicesUsing(_container.GetInstance);

            mce.AddMaps(typeof(AutoMapperProfile).Assembly);

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();

            AutoMapper.IMapper m = new Mapper(mc, t => _container.GetInstance(t));

            return m;
        }
    }

}