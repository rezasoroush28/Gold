using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopLine.Nop.Framework.Components;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Nop.Web.Models.Catalog;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "ProductGoldIngredient")]
    public class B2CProductGoldIngredientComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IProductGoldBelongingMappingService _productGoldBelongingMappingService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;

        #endregion

        #region Ctor

        public B2CProductGoldIngredientComponent(IGoldBelongingService goldBelongingService,
            IProductGoldBelongingMappingService productGoldBelongingMappingService,
            IB2CGoldModelFactory b2CGoldModelFactory)
        {
            _goldBelongingService = goldBelongingService;
            _productGoldBelongingMappingService = productGoldBelongingMappingService;
            _b2CGoldModelFactory = b2CGoldModelFactory;

        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var productModel = additionalData as ProductDetailsModel;
            if (productModel != null)
            {
                var listModel = new ProductGoldIngredientModel
                {
                    ProductGoldIngredientList = _b2CGoldModelFactory.PrepareB2CGoldIngredientViewModelsByProductId(productModel.Id)
                };

                return View(ViewPathKeys.GoldProductGoldIngredientViewPath, listModel);
            }

            return View();
        }

        #endregion
    }
}