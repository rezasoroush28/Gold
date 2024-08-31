using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using Nop.Core.Domain;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routing;

namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure
{
    /// <summary>
    /// Represents provider that provided basic routes
    /// </summary>
    public partial class RouteProvider : IRouteProvider
    {
        #region Methods

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routeBuilder">Route builder</param>
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //reorder routes so the most used ones are on top. It can improve performance

            //page not found
            routeBuilder.MapLocalizedRoute("AddToCart", "addproducttocart/details/{productId}/{shoppingcarttypeid}",
                new { controller = "B2CShoppingCartGold", action = "AddProductToCart_Details" });

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority
        {
            get { return 1000; }
        }

        #endregion
    }
}
