using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory
    /// </summary>
    public partial interface IGoldProposalValueViewModelFactory
    {
        /// <summary>
        /// Prepare GoldProposalValue model
        /// </summary>
        /// <param name="model">GoldProposalValue model</param>
        /// <param name="GoldProposalValue">GoldProposalValue</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldProposalValue model</returns>
        GoldProposalValueViewModel PrepareGoldProposalValueViewModel();
    }
}
