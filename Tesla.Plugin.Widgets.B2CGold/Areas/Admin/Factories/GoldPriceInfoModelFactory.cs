using System;
using System.Linq;

using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldPriceInfo;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldProposalValue model factory implementation
    /// </summary>
    public partial class GoldPriceInfoModelFactory : IGoldPriceInfoModelFactory
    {
        #region Fields

        private readonly IGoldProposalValueService _goldProposalValueService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public GoldPriceInfoModelFactory(IGoldProposalValueService GoldProposalValueService,
            ILocalizationService localizationService)
        {
            _goldProposalValueService = GoldProposalValueService;
            _localizationService = localizationService;
        }

        #endregion

        #region Methods

        public GoldPriceInfoModel PrepareGoldPriceInfoModel(GoldPriceInfoModel model, GoldPriceInfo productGoldInfo)
        {
            model = model ?? productGoldInfo?.ToModel<GoldPriceInfoModel>() ?? new GoldPriceInfoModel();

            return model;
        }

        #endregion
    }
}