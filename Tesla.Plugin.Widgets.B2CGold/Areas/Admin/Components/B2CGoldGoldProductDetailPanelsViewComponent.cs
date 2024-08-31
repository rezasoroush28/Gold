using Microsoft.AspNetCore.Mvc;
using NopLine.Nop.Framework.Components;
using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Components
{
    [ViewComponent(Name = "B2CGoldGoldProductDetailsPanels")]
    public class B2CGoldGoldProductDetailPanelsViewComponent : BaseNopLineComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "B2CGoldGoldPriceInfo", additionalData},
                { "B2CGoldGoldBelonging", additionalData },
                { "B2CGoldGoldIngredient", additionalData },
            };

            return View("/Plugins/Widgets.B2CGold/Areas/Admin/Views/Shared/Components/GoldProductDetailsPanels/Default.cshtml", dictionary);
        }
    }
}