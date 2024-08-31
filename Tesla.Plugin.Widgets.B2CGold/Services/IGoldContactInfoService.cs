using Nop.Core;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Services
{
    public interface IGoldContactInfoService
    {
        IPagedList<GoldContactInfo> GetAllGoldContactInfo(int pageIndex = 0, int pageSize = int.MaxValue);
        GoldContactInfo GetGoldContactInfoById(int id);

        void InsertGoldContactInfo(GoldContactInfo goldContactInfo);
        void UpdateGoldContactInfo(GoldContactInfo goldContactInfo);
        void DeleteGoldContactInfo(GoldContactInfo goldContactInfo);
    }
}