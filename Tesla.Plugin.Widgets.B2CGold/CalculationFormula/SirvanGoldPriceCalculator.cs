using Nop.Core;
using Nop.Core.Configuration;

using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Helper;
namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    public class SirvanGoldPriceCalculator : StandardGoldPriceCalculator
    {
        public SirvanGoldPriceCalculator(B2CGoldSettings b2CGoldSettings, 
            IGoldBelongingService goldBelongingService) 
            : base(b2CGoldSettings, goldBelongingService)
        {
        }
    }
}