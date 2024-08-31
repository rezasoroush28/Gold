using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldBelongingTypeService
    {
        List<GoldBelongingType> GetAllGoldBelongingTypes();

        void InsertBelongingType(GoldBelongingType goldBelongingType);

        void DeleteBelongingType(GoldBelongingType goldBelongingType);

        void UpdateBelongingType(GoldBelongingType goldBelongingType);

        bool CheckName(GoldBelongingType goldBelongingType);

        GoldBelongingType GetBelongingTypeById(int id);
    }
}