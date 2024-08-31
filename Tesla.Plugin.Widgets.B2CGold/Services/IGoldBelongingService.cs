using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldBelongingService
    {
        void InsertGoldBelonging(GoldBelonging goldBelonging);
        void DeleteGoldBelonging(GoldBelonging goldBelonging);
        void UpdateGoldBelonging(GoldBelonging goldBelonging);
        GoldBelonging GetGoldBelongingById(int id);
        List<GoldBelonging> GetAllGoldBelongings();
        bool CheckName(GoldBelonging goldBelonging);
        decimal GetGoldProductBelongingPrice(decimal goldProductWeight, decimal totalCalulatedWeight, GoldProductBelongingCalculation goldProductBelonging, decimal goldRealtimePrice);
    }
}