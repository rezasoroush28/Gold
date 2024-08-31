using Microsoft.AspNetCore.Mvc;
using NopLine.Nop.Framework.Components;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Web.Areas.Admin.Models.Catalog;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldPriceInfo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Components
{
    [ViewComponent(Name = "B2CGoldGoldPriceInfo")]
    public class B2CGoldGoldInfoViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldPriceInfoModelFactory _goldPriceInfoModelFactory;
        private readonly IGoldPriceInfoService _goldPriceInfoService;

        #endregion

        #region Ctor 

        public B2CGoldGoldInfoViewComponent(IGoldPriceInfoModelFactory goldPriceInfoModelFactory,
            IGoldPriceInfoService goldPriceInfoService)
        {
            _goldPriceInfoModelFactory = goldPriceInfoModelFactory;
            _goldPriceInfoService = goldPriceInfoService;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var productModel = additionalData as ProductModel;
            var model = new GoldPriceInfoModel();
            var goldPriceInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(productModel.Id);
            if (goldPriceInfo != null)
            {
                model = goldPriceInfo?.ToModel<GoldPriceInfoModel>();
            }

            return View(AdminViewPathKeys.B2CGoldGoldPriceInfoViewPath, model);
        } 

        #endregion
    }
}