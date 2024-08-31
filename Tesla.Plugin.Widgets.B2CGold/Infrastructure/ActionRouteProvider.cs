using Microsoft.AspNetCore.Mvc.Filters;

using NopLine.Nop.Framework.ActionFilters;
using NopLine.Nop.Framework.Routing;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.ActionFilters;


namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure
{
    /// <summary>
    /// Represents plugin route provider
    /// </summary>
    public class ActionRouteProvider : BaseRouteProvider
    {
        protected override void RegisterPluginActionFilters(IList<IFilterProvider> providers)
        {
            var generalActionFilterProvider = new GeneralActionFilterProvider();
            generalActionFilterProvider.Add(new FinalPurchaseActionfilterFactory());
            generalActionFilterProvider.Add(new SetExtraGoldInvoiceActionFilterAttributeFactory());

            providers.Add(generalActionFilterProvider);
        }

        protected override string PluginSystemName => "Tesla.Plugin.Widgets.B2CGold";
    }
}