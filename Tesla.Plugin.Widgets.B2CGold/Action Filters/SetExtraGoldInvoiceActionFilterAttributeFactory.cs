using Microsoft.AspNetCore.Mvc.Filters;

using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Media;
using Nop.Services.Orders;

using NopLine.Nop.Framework.ActionFilters;

using RazorEngine;

namespace Tesla.Plugin.Widgets.B2CGold.ActionFilters
{
    public class SetExtraGoldInvoiceActionFilterAttributeFactory : IControllerActionFilterFactory
    {
        #region Properties

        public string ControllerName => "Order";

        public string ActionName => "DetailsForPrint";

        #endregion

        #region Methods

        public ActionFilterAttribute GetActionFilterAttribute()
        {
            return new SetExtraGoldInvoiceActionFilterAttribute(EngineContext.Current.Resolve<IGenericAttributeService>(),EngineContext.Current.Resolve<IOrderService>());
        }

        #endregion
    }
}