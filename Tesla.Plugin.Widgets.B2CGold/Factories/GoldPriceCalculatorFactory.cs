using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Configuration;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Services.Localization;
using System.Linq;
using Nop.Services.Directory;
using Nop.Services.Catalog;
using Nop.Services.Shipping;
using Nop.Services.Logging;
using Nop.Core.Caching;
using static Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.B2CGoldSettingsModel;
using Nop.Core.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using System;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public partial class GoldPriceCalculatorFactory : IGoldPriceCalculatorFactory
    {
        private readonly B2CGoldSettings _b2CGoldSettings;

        public GoldPriceCalculatorFactory(B2CGoldSettings b2CGoldSettings)
        {
            _b2CGoldSettings = b2CGoldSettings;
        }

        public IGoldPriceCalculator GetGoldPriceCalculator()
        {
            var priceCalculationName = _b2CGoldSettings.PriceCalculationMethodName;
            //Create the proper instance based on the settings
            var types = ReflectionHelper.FindImplementations<IGoldPriceCalculator>();
            var chooseType = types.Where(x => x.Name == priceCalculationName).Single();
            return (IGoldPriceCalculator)EngineContext.Current.ResolveUnregistered(chooseType);
        }
    }
}

