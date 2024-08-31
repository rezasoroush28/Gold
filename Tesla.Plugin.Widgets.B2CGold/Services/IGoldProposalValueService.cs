using Nop.Core;
using Nop.Core.ResponseModel;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Services
{
    public interface IGoldProposalValueService
    {
        IPagedList<GoldProposalValue> GetAllGoldProposalValue(int pageIndex = 0, int pageSize = int.MaxValue);
        GoldProposalValue GetGoldProposalValueById(int id);

        GeneralResponseModel InsertGoldProposalValue(GoldProposalValue goldProposalValue);
        GeneralResponseModel UpdateGoldProposalValue(GoldProposalValue goldProposalValue);
        void DeleteGoldProposalValue(GoldProposalValue goldProposalValue);
    }
}