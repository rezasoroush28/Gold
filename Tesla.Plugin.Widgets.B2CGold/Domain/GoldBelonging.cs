using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldBelonging : BaseEntity
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int MeasureWeightId { get; set; }
    }
}
