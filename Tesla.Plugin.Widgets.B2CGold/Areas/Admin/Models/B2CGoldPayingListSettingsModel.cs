using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class B2CGoldPayingListSettingsModel : BaseNopModel, ISettingsModel
    {
        public bool GoldCurrentPriceIsActive { get; set; }
        public bool TotalWeightIsActive { get; set; }
        public bool ProductGoldPriceIsActive { get; set; }
        public bool GoldPriceIsActive { get; set; }
        public bool BelongingPriceIsActive { get; set; }
        public bool TaxIsActive { get; set; }
        public bool TotalPriceIsActive { get; set; }
        public bool ManufacturerProfitIsActive { get; set; }
        public bool MiscPriceIsActive { get; set; }
        public bool PreOrderPriceIsActive { get; set; }
        public bool VendorProfitIsActive { get; set; }
        public int ActiveStoreScopeConfiguration { get; set; }
    }
}
