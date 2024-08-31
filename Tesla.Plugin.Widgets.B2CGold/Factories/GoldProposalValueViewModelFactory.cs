using System.Collections.Generic;
using System.Linq;

using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory implementation
    /// </summary>
    public partial class GoldProposalValueViewModelFactory : IGoldProposalValueViewModelFactory
    {
        #region Fields

        private readonly IGoldProposalValueService _goldProposalValueService;

        #endregion

        #region Ctor

        public GoldProposalValueViewModelFactory(IGoldProposalValueService goldProposalValueService)
        {
            _goldProposalValueService = goldProposalValueService;
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
        public virtual GoldProposalValueViewModel PrepareGoldProposalValueViewModel()
        {
            var model = new GoldProposalValueViewModel { GoldProposalValues = new List<GoldProposalValueModel>() };
            var goldProposalValues = _goldProposalValueService.GetAllGoldProposalValue();
            foreach (var goldProposalValue in goldProposalValues)
            {
                var goldProposalValueModel = goldProposalValue.ToModel<GoldProposalValueModel>();
                model.GoldProposalValues.Add(goldProposalValueModel);
            }

            return model;
        }

        #endregion
    }
}