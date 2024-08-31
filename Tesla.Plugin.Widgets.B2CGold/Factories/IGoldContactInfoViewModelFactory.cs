using Tesla.Plugin.Widgets.B2CGold.Models.GoldContactInfo;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory
    /// </summary>
    public partial interface IGoldContactInfoViewModelFactory
    {
        /// <summary>
        /// Prepare GoldProposalValue model
        /// </summary>
        /// <param name="model">GoldProposalValue model</param>
        /// <param name="GoldProposalValue">GoldProposalValue</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldProposalValue model</returns>
        GoldContactInfoViewModel PrepareGoldContactInfoViewModel();
    }
}
