using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class GoldBelongingPayingModel : BaseNopEntityModel
    {
        #region Ctor

        public GoldBelongingPayingModel()
        {
            AvailableBelongingPaying = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.Value")]
        public decimal Value { get; set; }
        public int GoldBelongingCalculationTypeId { get; set; }
        public int ProductId { get; set; }
        public IList<SelectListItem> AvailableBelongingPaying { get; set; }
        public GoldBelongingCalculationType PayingType
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

        public string PayingTypeText
        {
            get
            {
                if (GoldBelongingCalculationTypeId == 1) { return "هزینه مستقیم"; }
                else if (GoldBelongingCalculationTypeId == 2) { return "هزینه متعلقات بر اساس درصد طلا در محصول"; }
                else if (GoldBelongingCalculationTypeId == 3) { return "هزینه و اجرت ساخت متعلقات بر اساس درصد طلا در محصول"; }
                return "";
            }
        }

        public string PayingUnit
        {
            get
            {
                if (GoldBelongingCalculationTypeId == 1) { return " تومان"; }
                return "درصد";
            }
        }

        #endregion
    }
}




