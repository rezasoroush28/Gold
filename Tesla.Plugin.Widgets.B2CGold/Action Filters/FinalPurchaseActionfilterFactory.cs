using Microsoft.AspNetCore.Mvc.Filters;

using Nop.Core.Infrastructure;

using NopLine.Nop.Framework.ActionFilters;

namespace Tesla.Plugin.Widgets.B2CGold.ActionFilters
{
    public class FinalPurchaseActionfilterFactory : IControllerActionFilterFactory
    {
        public string ControllerName => "Checkout";

        public string ActionName => "ProcessPaymentMethod";

        public ActionFilterAttribute GetActionFilterAttribute()
        {
            return (FinalPurchaseActionfilter)EngineContext.Current.ResolveUnregistered(typeof(FinalPurchaseActionfilter));
        }
    }

}