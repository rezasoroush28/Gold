using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldPriceInfo : BaseEntity
    {
        public int ProductId { get; set; }
        public decimal ManufacturerCommissionPercentage { get; set; }
        public decimal VendorCommissionPercentage { get; set; }
        public decimal BonakdarCommissionPercentage { get; set; }
        public decimal AddedPriceAmount { get; set; }
    }
}
