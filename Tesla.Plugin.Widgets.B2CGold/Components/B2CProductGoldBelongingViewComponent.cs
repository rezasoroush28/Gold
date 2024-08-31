using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopLine.Nop.Framework.Components;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Nop.Web.Models.Catalog;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "ProductGoldBelonging")]
    public class B2CProductGoldBelongingComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IProductGoldBelongingMappingService _productGoldBelongingMappingService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;

        #endregion

        #region Ctor

        public B2CProductGoldBelongingComponent(IGoldBelongingService goldBelongingService,
            IProductGoldBelongingMappingService productGoldBelongingMappingService,
            IB2CGoldModelFactory b2CGoldModelFactory,
            IGoldProductBelongingCalculationService goldBelongingPayingService)
        {
            _goldBelongingService = goldBelongingService;
            _productGoldBelongingMappingService = productGoldBelongingMappingService;
            _b2CGoldModelFactory = b2CGoldModelFactory;
            _goldBelongingPayingService = goldBelongingPayingService;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {

            ProductDetailsModel productModel = additionalData as ProductDetailsModel;

            if (productModel != null)
            {
                var belongingsPaying = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(productModel.Id);
                var belongingMotherModel = _b2CGoldModelFactory.PrepareB2CProductGoldBelongingModelsByProductId(productModel.Id);
                return View(ViewPathKeys.GoldProductGoldBelongingViewPath, belongingMotherModel);
            }
            return View();
        }
    }
    #endregion

}