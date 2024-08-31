using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public interface IGoldPriceCalculatorFactory
    {
        IGoldPriceCalculator GetGoldPriceCalculator();
    }
}