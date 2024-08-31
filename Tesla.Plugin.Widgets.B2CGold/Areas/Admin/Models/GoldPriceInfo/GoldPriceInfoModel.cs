using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldPriceInfo
{
    public class GoldPriceInfoModel : BaseNopEntityModel
    {

        public int ProductId { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2CGold.ProductGoldInfo.ManufacturerCommissionPercentage")]
        public decimal ManufacturerCommissionPercentage { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2CGold.ProductGoldInfo.VendorCommissionPercentage")]
        public decimal VendorCommissionPercentage { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.B2CGold.ProductGoldInfo.BonakdarCommissionPercentage")]
        public decimal BonakdarCommissionPercentage { get; set; }
    }
}
