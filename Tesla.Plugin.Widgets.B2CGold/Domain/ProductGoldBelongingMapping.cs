using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class ProductGoldBelongingMapping : BaseEntity
    {
        public int ProductId { get; set; }
        public int GoldBelongingId { get; set; }
        public int Count { get; set; }
        public decimal Weight { get; set; }
    }
}
