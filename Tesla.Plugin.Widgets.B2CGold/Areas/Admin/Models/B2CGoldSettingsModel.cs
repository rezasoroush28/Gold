using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

using System;
using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class B2CGoldSettingsModel : BaseNopModel, ISettingsModel
    {
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.Enabled")]
        public bool Enabled { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldCurrentPriceOnline")]
        public decimal GoldCurrentPriceOnline { get; set; }
        public bool Enabled_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.SellerProfitPercentage")]
        public int SellerProfitPercentage { get; set; }
        public bool SellerProfitPercentage_OverrideForStore { get; set; }
        public bool GoldPrepurchasePercentage_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldCurrentPriceInput")]
        public decimal GoldCurrentPriceInput { get; set; }
        public bool GoldCurrentPriceInput_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.UseDefaultGoldCurrentPrice")]
        public bool UseDefaultGoldCurrentPrice { get; set; }
        public int ActiveStoreScopeConfiguration { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldPreOrderPercentage")]
        public int GoldPreOrderPercentage { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldTaxRate")]
        public int GoldTaxRate { get; set; }
        public bool GoldTaxRate_OverrideForStore { get; set; }
        public bool IsActiveIngredients { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldCurrentPriceIsActive")]
        public bool GoldCurrentPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.TotalWeightIsActive")]
        public bool TotalWeightIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.ProductGoldPriceIsActive")]
        public bool ProductGoldPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.BelongingPriceIsActive")]
        public bool BelongingPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.TaxIsActive")]
        public bool TaxIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.TotalPriceIsActive")]
        public bool TotalPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.ManufacturerProfitIsActive")]
        public bool ManufacturerProfitIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.MiscPriceIsActive")]
        public bool MiscPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.PreOrderPriceIsActive")]
        public bool PreOrderPriceIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.VendorProfitIsActive")]
        public bool VendorProfitIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.PriceGoldByTheMinute")]
        public int PriceGoldByTheMinute { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.PriceGrabberConnectionString")]
        public string PriceGrabberConnectionString { get; set; }
        public string PriceCalculationMethodName { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.AvailableCalculationMethods")]
        public List<SelectListItem> AvailableCalculationMethods { get; set; }
        public bool OrderValidateTimeInSeconds_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.OrderValidateTimeInSeconds")]
        public int OrderValidateTimeInSeconds { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.ApiPriceGoldActive")]
        public bool ApiPriceGoldActive { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.LastUpdatePriceGold")]
        public string LastUpdatePriceGold { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.ApiGoldPriceUrl")]
        public string ApiGoldPriceUrl { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.IfShowGoldPrePurchaceFactor")]
        public bool IfShowGoldPrePurchaceFactor { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.IsShowGoldFactorDetails")]
        public bool IsShowGoldFactorDetails { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.GoldPriceUsedIsActive")]
        public bool GoldPriceUsedIsActive { get; set; }
        public bool GoldPriceUsedIsActive_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2cgold.Configuration.IsActiveOnlineChat")]
        public bool IsActiveOnlineChat { get; set; }
        public bool IsActiveOnlineChat_OverrideForStore { get; set; }
    }
}
