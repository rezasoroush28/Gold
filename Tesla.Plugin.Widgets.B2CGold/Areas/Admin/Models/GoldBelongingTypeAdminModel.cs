using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models
{
    public class GoldBelongingTypeAdminModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.B2CGold.GoldBelongingType.Fields.Name")]
        public string Name { get; set; }
    }
}



