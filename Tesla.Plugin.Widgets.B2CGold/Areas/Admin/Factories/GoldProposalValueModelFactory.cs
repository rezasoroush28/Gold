using System;
using System.Linq;

using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory implementation
    /// </summary>
    public partial class GoldProposalValueModelFactory : IGoldProposalValueModelFactory
    {
        #region Fields

        private readonly IGoldProposalValueService _goldProposalValueService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public GoldProposalValueModelFactory(IGoldProposalValueService GoldProposalValueService,
            ILocalizationService localizationService)
        {
            _goldProposalValueService = GoldProposalValueService;
            _localizationService = localizationService;
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
        public virtual GoldProposalValueModel PrepareGoldProposalValueModel(GoldProposalValueModel model, GoldProposalValue goldProposalValue, bool excludeProperties = false)
        {
            //Manage 4 state for model preparation

            model = model ?? goldProposalValue?.ToModel<GoldProposalValueModel>() ?? new GoldProposalValueModel();

            return model;
        }

        /// <summary>
        /// Prepare GoldProposalValue search model
        /// </summary>
        /// <param name="searchModel">GoldProposalValue search model</param>
        /// <returns>GoldProposalValue search model</returns>
        public virtual GoldProposalValueSearchModel PrepareGoldProposalValueSearchModel(GoldProposalValueSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged GoldProposalValue list model
        /// </summary>
        /// <param name="searchModel">GoldProposalValue search model</param>
        /// <returns>GoldProposalValue list model</returns>
        public virtual GoldProposalValueListModel PrepareGoldProposalValueListModel(GoldProposalValueSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get categories
            var goldProposalValues = _goldProposalValueService.GetAllGoldProposalValue(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new GoldProposalValueListModel().PrepareToGrid(searchModel, goldProposalValues, () =>
            {
                return goldProposalValues.Select(goldProposalValue =>
                {
                    //fill in model values from the entity
                    var goldProposalValueModel = goldProposalValue.ToModel<GoldProposalValueModel>();

                    return goldProposalValueModel;
                });
            });

            return model;
        }

        #endregion
    }
}