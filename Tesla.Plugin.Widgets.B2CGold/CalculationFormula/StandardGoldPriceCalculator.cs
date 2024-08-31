using Nop.Core;
using Nop.Core.Configuration;

using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Helper;
namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    public class StandardGoldPriceCalculator : IGoldPriceCalculator
    {
        #region Field

        private readonly IGoldBelongingService _goldBelongingService;
        private readonly B2CGoldSettings _b2CGoldSettings;

        #endregion

        #region Ctor
        public StandardGoldPriceCalculator(B2CGoldSettings b2CGoldSettings, IGoldBelongingService goldBelongingService)
        {
            _b2CGoldSettings = b2CGoldSettings;
            _goldBelongingService = goldBelongingService;
        }

        #endregion

        #region Properties

        public string CalculationFormula
        {
            get { return _b2CGoldSettings.PriceCalculationMethodName; }
            set
            {
                value = _b2CGoldSettings.PriceCalculationMethodName;
            }
        }

        #endregion

        #region Methods
    
        public GoldCalulatedInfoModel GetPriceTable(decimal goldProductWeight, GoldPriceInfo goldPriceInfo
              ,GoldProductBelongingCalculation goldProductBelongingCalculation, decimal goldRealTimePrice,bool availablePreOrder = false)
        {
            if (goldPriceInfo == null)
            {
                goldPriceInfo = new GoldPriceInfo { };
            }

            var manufacturerGoldGram = goldProductWeight * goldPriceInfo.ManufacturerCommissionPercentage / 100;
            var vendorGoldGram = goldProductWeight * goldPriceInfo.VendorCommissionPercentage / 100;
            var taxRate = ((decimal)_b2CGoldSettings.GoldTaxRate) / 100;
            var taxGram = (manufacturerGoldGram + vendorGoldGram) * taxRate;
            var totalCalulatedWeight = manufacturerGoldGram + vendorGoldGram + taxGram;
            var belongingCalculatedPrice = _goldBelongingService.GetGoldProductBelongingPrice(goldProductWeight, totalCalulatedWeight, goldProductBelongingCalculation, goldRealTimePrice);
            var goldCalculatedPrice = (goldProductWeight + totalCalulatedWeight) * goldRealTimePrice;
            var finalPrice = goldCalculatedPrice + belongingCalculatedPrice + goldPriceInfo.AddedPriceAmount;

            var result = new GoldCalulatedInfoModel
            {
                GoldCurrentPrice = goldRealTimePrice,
                TotalWeight = goldProductWeight,
                ProductGoldPrice = Helper.Helper.RoundToThousend(finalPrice),
                GoldPrice = Helper.Helper.RoundToThousend(goldProductWeight * goldRealTimePrice),
                BelongingPrice = Helper.Helper.RoundToThousend(belongingCalculatedPrice),
                Tax = Helper.Helper.RoundToThousend(taxGram * goldRealTimePrice),
                TotalPrice = Helper.Helper.RoundToThousend(finalPrice),
                ManufacturerProfit = manufacturerGoldGram * goldRealTimePrice,
                MiscPrice = goldPriceInfo.AddedPriceAmount,
                PreOrderPrice = 0,
                VendorProfit = vendorGoldGram * goldRealTimePrice,
                ManuFacturerProfitPrecentage = goldPriceInfo.ManufacturerCommissionPercentage,
                CommisionTexPrecentage = _b2CGoldSettings.GoldTaxRate,
                ProfitPrecentage = goldPriceInfo.VendorCommissionPercentage
            };

            if (availablePreOrder)
            {
                result.PreOrderPrice = Helper.Helper.RoundToThousend((decimal)(result.TotalPrice * (decimal)(_b2CGoldSettings.GoldPreOrderPercentage) / 100));
            }

            return result;
        }

        #endregion
    }
}