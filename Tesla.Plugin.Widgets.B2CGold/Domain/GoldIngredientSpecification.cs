using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldIngredientSpecification : BaseEntity
    {
        public string SpecKey { get; set; }
        public string Value { get; set; }
        public int GoldIngredientId { get; set; }
    }
}
