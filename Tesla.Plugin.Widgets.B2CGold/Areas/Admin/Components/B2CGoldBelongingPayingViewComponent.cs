using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using NopLine.Nop.Framework.Components;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.Gold.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Core.Data;
using System;
using System.Linq;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Nop.Services.Localization;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Components
{
    [ViewComponent(Name = "B2CGoldBelongingPaying")]
    public class B2CGoldBelongingPayingViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IRepository<GoldProductBelongingCalculation> _goldProductBelongingRepository;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public B2CGoldBelongingPayingViewComponent(IGoldBelongingService goldBelongingService,
            IRepository<GoldProductBelongingCalculation> goldBelongingPayingRepository,
            IGoldProductBelongingCalculationService goldBelongingPayingService,
            ILocalizationService localizationService = null)
        {
            _goldBelongingService = goldBelongingService;
            _goldProductBelongingRepository = goldBelongingPayingRepository;
            _goldBelongingPayingService = goldBelongingPayingService;
            _localizationService = localizationService;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(int productId)
        {
            var paying = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(productId);
            var model = new GoldBelongingPayingAdminModel();

            if(paying != null)
            {
               model =  paying.ToModel<GoldBelongingPayingAdminModel>();
            }
            model.ProductId = productId;
            model.AvailableBelongingPaying = Enum.GetValues(typeof(GoldBelongingCalculationType))
                                               .Cast<GoldBelongingCalculationType>()
                                               .Select(e => new SelectListItem
                                               {
                                                   Text = GetGoldBelongingCalculationTypeName(e),
                                                   Value = ((int)e).ToString(),
                                                   Selected = false
                                               }).ToList();

            return View(AdminViewPathKeys.GoldBelongingPayingTypeViewPath, model);
        }

        #endregion

        #region Tools

        private string GetGoldBelongingCalculationTypeName(GoldBelongingCalculationType type)
        {
            switch (type)
            {
                case GoldBelongingCalculationType.GoldFinalPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.GoldFinalPriceGoldFinalPrice");
                case GoldBelongingCalculationType.SolidPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.SolidPrice");
                case GoldBelongingCalculationType.BaseProductGoldPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.BaseProductGoldPrice");
            }
            return type.ToString();
        }

        #endregion
    }
}