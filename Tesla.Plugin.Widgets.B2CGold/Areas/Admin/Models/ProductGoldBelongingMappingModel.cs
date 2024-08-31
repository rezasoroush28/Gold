using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class ProductGoldBelongingMappingModel : BaseNopEntityModel
    {
        #region Ctor

        public ProductGoldBelongingMappingModel()
        {
            AvailableGoldBelongings = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        public int GoldBelongingId { get; set; }
        public int ProductId { get; set; }
        [NopResourceDisplayName("Admin.product.B2CGold.Belonging.Name")]
        public string GoldBelongingName { get; set; }
        [NopResourceDisplayName("Admin.product.B2CGold.Belonging.Count")]
        public int BelongingCount { get; set; }
        [NopResourceDisplayName("Admin.product.B2CGold.Belonging.Weight")]
        public decimal BelongingWeight { get; set; }
        [NopResourceDisplayName("Admin.product.B2CGold.Belonging.Type")]
        public IList<SelectListItem> AvailableGoldBelongings { get; set; }

        #endregion
    }
}



