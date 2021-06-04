using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace CoffeShop.App_Start
{
    /// <summary>
    /// Class SimpleInjectorInitializer.
    /// </summary>
    public class SimpleInjectorInitializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            SimpleInjectorBootstrap.InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify(VerificationOption.VerifyAndDiagnose);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            // Register the dependency resolver for Web API.
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }

    /// <summary>
    /// Properties with this attribute will be injected.
    /// </summary>
    public class ImportAttribute : Attribute
    {
    }

    /// <summary>
    /// Property selector used to do property injection with simple injector. <See url="https://simpleinjector.readthedocs.org/en/latest/advanced.html#property-injection" />
    /// </summary>
    internal class ImportPropertySelectionBehavior : IPropertySelectionBehavior
    {
        /// <summary>
        /// Selects the property that should be injected.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="prop">The property.</param>
        /// <returns>True if the property needs to be injected.</returns>
        public bool SelectProperty(Type type, PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(ImportAttribute)).Any();
        }
    }
}
