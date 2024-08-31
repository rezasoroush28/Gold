using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldProductBelongingCalculationService
    {
        void InsertGoldProductBelongingCalculation(GoldProductBelongingCalculation goldProductBelonging);
        void UpdateGoldProductBelongingCalculation(GoldProductBelongingCalculation goldProductBelonging);
        GoldProductBelongingCalculation GetGoldProductBelongingCalculationByProductId(int id);
    }
}