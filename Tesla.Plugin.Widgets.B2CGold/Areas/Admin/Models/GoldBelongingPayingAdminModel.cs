using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models

{
    public class GoldBelongingPayingAdminModel : BaseNopEntityModel
    {
        #region Ctor

        public GoldBelongingPayingAdminModel()
        {
            AvailableBelongingPaying = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Product.B2CGold.BelongingPaying.Fields.Value")]
        public decimal Value { get; set; }
        [NopResourceDisplayName("Admin.Product.B2CGold.BelongingPaying.Fields.GoldBelongingCalculationTypeId")]
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

        public string GoldBelongingCalculationTypeName { get; set; }

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




