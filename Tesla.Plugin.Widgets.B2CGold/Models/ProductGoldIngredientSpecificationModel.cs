using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class ProductGoldIngredientSpecificationModel : BaseNopEntityModel
    {
        public int GoldIngredientId { get; set; }

        [NopResourceDisplayName("Admin.Product.ProductGoldSpecification.Fields.SpecKey")]
        public string SpecKey { get; set; }

        [NopResourceDisplayName("Admin.Product.ProductGoldSpecification.Fields.SpecValue")]
        public string Value { get; set; }
    }
}



