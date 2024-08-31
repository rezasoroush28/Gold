using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopLine.Nop.Framework.Components;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.Gold.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Components
{
    [ViewComponent(Name = "B2CGoldGoldBelonging")]
    public class B2CGoldBelongingViewComponent : BaseNopLineComponent
    {
        #region Field

        private readonly IGoldBelongingService _goldBelongingService;

        #endregion

        #region Ctor
        public B2CGoldBelongingViewComponent(IGoldBelongingService goldBelongingService)
        {
            _goldBelongingService = goldBelongingService;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var productModel = additionalData as ProductModel;
            var productId = productModel.Id;
            var listModel = new ProductGoldBelongingMappingModel();
            listModel.ProductId = productId;
            var availableGoldBelongings = _goldBelongingService.GetAllGoldBelongings();
            foreach (var item in availableGoldBelongings)
            {
                listModel.AvailableGoldBelongings.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = false
                });
            }
            return View(AdminViewPathKeys.GoldBelongingViewPath, listModel);
        }

        #endregion
    }
}