using Nop.Core;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;

namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    public interface IGoldPriceCalculator
    {
        string CalculationFormula { get; set; }
        GoldCalulatedInfoModel GetPriceTable(decimal goldProductWeight, GoldPriceInfo goldPriceInfo, GoldProductBelongingCalculation goldProductBelongingCalculation, decimal goldRealTimePrice, bool availablePreOrder = false);
    }
}