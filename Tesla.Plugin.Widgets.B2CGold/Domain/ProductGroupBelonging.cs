using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class ProductGroupBelonging : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int MeasureWeightId { get; set; }
    }
}
