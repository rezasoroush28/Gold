using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class GoldIngredientSpecificationAdminModel : BaseNopEntityModel
    {
        public int GoldIngredientId { get; set; }
        
        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.Name")]
        public string SpecKey { get; set; }
        public string Value { get; set; }
    }
}



