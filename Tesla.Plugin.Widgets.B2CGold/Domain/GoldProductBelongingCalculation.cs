using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldProductBelongingCalculation : BaseEntity
    {
        public int ProductId { get; set; }
        public int GoldBelongingCalculationTypeId { get; set; }
        public GoldBelongingCalculationType GoldBelongingCalculationType
        {
            get
            {
                return (GoldBelongingCalculationType)GoldBelongingCalculationTypeId;
            }
            set
            {
                GoldBelongingCalculationTypeId = (int)value;
            }
        }

        public decimal Value { get; set; }
    }
}
