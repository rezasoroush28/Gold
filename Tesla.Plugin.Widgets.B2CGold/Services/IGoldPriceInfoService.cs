using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldPriceInfoService
    {
        List<GoldPriceInfo> GetAllGoldPriceInfo();

        void InsertGoldPriceInfo(GoldPriceInfo productGoldInfo);

        void DeleteGoldPriceInfo(GoldPriceInfo productGoldInfo);

        void UpdateGoldPriceInfo(GoldPriceInfo productGoldInfo);

        GoldPriceInfo GetGoldPriceInfoById(int id);

        GoldPriceInfo GetGoldPriceInfoByProductId(int id);

    }
}