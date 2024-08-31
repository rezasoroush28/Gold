using Nop.Core.Configuration;

using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

using System;

namespace Tesla.Plugin.Widgets.B2CGold
{
    public class B2CGoldSettings : ISettings
    {
        public bool Enabled { get; set; }
        public int GoldTaxRate { get; set; }
        public bool GoldTaxRate_OverrideForStore { get; set; }
        public int SellerProfitPercentage { get; set; }
        public bool SellerProfitPercentage_OverrideForStore { get; set; }
        public int GoldPreOrderPercentage { get; set; }
        public bool GoldPrepurchasePercentage_OverrideForStore { get; set; }
        public decimal GoldCurrentPriceInput { get; set; }
        public bool GoldCurrentPriceInput_OverrideForStore { get; set; }
        public decimal GoldCurrentPriceOnline { get; set; }
        public bool UseDefaultGoldCurrentPrice { get; set; }
        public bool GoldCurrentPriceIsActive { get; set; }
        public bool TotalWeightIsActive { get; set; }
        public bool ProductGoldPriceIsActive { get; set; }
        public bool GoldPriceUsedIsActive { get; set; }
        public bool BelongingPriceIsActive { get; set; }
        public bool TaxIsActive { get; set; }
        public bool TotalPriceIsActive { get; set; }
        public bool ManufacturerProfitIsActive { get; set; }
        public bool MiscPriceIsActive { get; set; }
        public bool PreOrderPriceIsActive { get; set; }
        public bool VendorProfitIsActive { get; set; }
        public int ActiveStoreScopeConfiguration { get; set; }
        public int PriceGoldByTheMinute { get; set; }
        public string PriceGrabberConnectionString { get; set; }
        public string ApiUrl { get; set; }
        public string PriceElement { get; set; }
        public string PriceCalculationMethodName { get; set; }
        public int OrderValidateTimeInSeconds { get; set; }
        public bool ApiPriceGoldActive { get; set; }
        public string LastUpdatePriceGold { get; set; }
        public string ApiGoldPriceUrl { get; set; }
        public int ProductAtrributeWeightId { get; set; }
        public bool IfShowGoldPrePurchaceFactor { get; set; }
        public bool IsShowGoldFactorDetails { get; set; }
        public bool IsActiveOnlineChat { get; set; }
    }
}
