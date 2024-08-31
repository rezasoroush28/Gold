using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory
    /// </summary>
    public partial interface IGoldProposalValueModelFactory
    {
        /// <summary>
        /// Prepare GoldProposalValue search model
        /// </summary>
        /// <param name="searchModel">GoldProposalValue search model</param>
        /// <returns>GoldProposalValue search model</returns>
        GoldProposalValueSearchModel PrepareGoldProposalValueSearchModel(GoldProposalValueSearchModel searchModel);

        /// <summary>
        /// Prepare paged GoldProposalValue list model
        /// </summary>
        /// <param name="searchModel">GoldProposalValue search model</param>
        /// <returns>GoldProposalValue list model</returns>
        GoldProposalValueListModel PrepareGoldProposalValueListModel(GoldProposalValueSearchModel searchModel);

        /// <summary>
        /// Prepare GoldProposalValue model
        /// </summary>
        /// <param name="model">GoldProposalValue model</param>
        /// <param name="GoldProposalValue">GoldProposalValue</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldProposalValue model</returns>
        GoldProposalValueModel PrepareGoldProposalValueModel(GoldProposalValueModel model, GoldProposalValue GoldProposalValue, bool excludeProperties = false);
    }
}