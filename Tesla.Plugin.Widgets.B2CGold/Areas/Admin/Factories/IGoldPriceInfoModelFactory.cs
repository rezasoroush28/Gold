using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldPriceInfo;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory
    /// </summary>
    public partial interface IGoldPriceInfoModelFactory
    {
        GoldPriceInfoModel PrepareGoldPriceInfoModel(GoldPriceInfoModel model, GoldPriceInfo productGoldInfo);
    }
}