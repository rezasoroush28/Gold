using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models

{
    public class GoldBelongingAdminModel : BaseNopEntityModel
    {
        #region Ctor
        
        public GoldBelongingAdminModel()
        {
            AvailableMeasurments = new List<SelectListItem>();
            AvailableTypes = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.AvailableTypes")]
        public IList<SelectListItem> AvailableTypes { get; set; }

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.TypeId")]
        public int TypeId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.AvailableMeasurments")]
        public IList<SelectListItem> AvailableMeasurments { get; set; }

        [NopResourceDisplayName("Admin.Configuration.B2CGold.Fields.MeasureWeightId")]
        public int MeasureWeightId { get; set; }
    } 

    #endregion
}



