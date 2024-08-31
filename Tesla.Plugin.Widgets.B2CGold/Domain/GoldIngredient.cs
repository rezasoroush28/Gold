using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldIngredient : BaseEntity
    {
        public int ProductId { get; set; }
        public string IngredientName { get; set; }
        public decimal IngredientWidth { get; set; }
        public decimal IngredientLength { get; set; }
        public decimal IngredientHeight { get; set; }
        public decimal IngredientWeight { get; set; }
        public int  MeasureWeightId { get; set; }
    }
}
