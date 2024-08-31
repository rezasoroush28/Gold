using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Core.Domain.Directory;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class OrderDetailsGoldModel : BaseNopEntityModel
    {
        public int OrderId { get; set; }
        public decimal TotalGoldPrice { get; set; }
        public decimal TotalSpecialServices { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal InstallmentPurchaseCommission { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal TotalWeightCorrection { get; set; }
        public decimal TotalPriceCorrection { get; set; }
        public decimal ShippingCost { get; set; }
        public string TotalOrderPriceInLetter { get; set; }
        public decimal  PreOrderPrices { get; set; }
        public decimal LatestCurrentGoldPrice { get; set; }
        public bool IsPaid { get; set; }

    }
}
