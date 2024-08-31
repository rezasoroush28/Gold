using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory implementation
    /// </summary>
    public partial class GoldContactInfoViewModelFactory : IGoldContactInfoViewModelFactory
    {
        #region Fields

        private readonly IGoldContactInfoService _goldContactInfoService;

        #endregion

        #region Ctor

        public GoldContactInfoViewModelFactory(IGoldContactInfoService goldContactInfoService)
        {
            _goldContactInfoService = goldContactInfoService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare GoldProposalValue model
        /// </summary>
        /// <param name="model">GoldProposalValue model</param>
        /// <param name="goldProposalValue">GoldProposalValue</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldProposalValue model</returns>
        public virtual GoldContactInfoViewModel PrepareGoldContactInfoViewModel()
        {
            var model = new GoldContactInfoViewModel();
            var goldContactInfos = _goldContactInfoService.GetAllGoldContactInfo();
            foreach (var goldContactInfo in goldContactInfos)
            {
                var goldContactInfoModel = goldContactInfo.ToModel<GoldContactInfoModel>();
                model.GoldContactInfos.Add(goldContactInfoModel);
            }

            return model;
        }

        #endregion
    }
}