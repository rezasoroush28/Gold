using Microsoft.AspNetCore.Mvc.Rendering;

using Nop;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class GoldBelongingMappingModel : BaseNopEntityModel
    {
        public int GoldBelongingId { get; set; }
        public int ProductId { get; set; }
        public string BelongingName { get; set; }
        public int BelongingCount { get; set; }
        public decimal BelongingWeight { get; set; }
        public string BelongingWeightFormmater => BelongingWeight.GetDecimalRoundFormated();
        public string BelongingType { get; set; }
        public string BelongingMeasureWeight { get; set; }
    }
}
