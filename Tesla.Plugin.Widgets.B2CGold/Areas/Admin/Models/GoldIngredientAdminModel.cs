using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models

{
    public class GoldIngredientAdminModel : BaseNopEntityModel
{
        #region Fields

        public GoldIngredientAdminModel()
        {
            AvailableMeasurments = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        public int ProductId { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.IngredientName")]
        public string IngredientName { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.IngredientWidth")]
        public decimal IngredientWidth { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.IngredientLength")]
        public decimal IngredientLength { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.IngredientHeight")]
        public decimal IngredientHeight { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.IngredientWeight")]
        public decimal IngredientWeight { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.AvailableMeasurments")]
        public IList<SelectListItem> AvailableMeasurments { get; set; }

        [NopResourceDisplayName("Admin.Product.B2CGold.Fields.MeasureWeightId")]
        public int MeasureWeightId { get; set; }
        public string measureWeightName { get; set; }
    } 

    #endregion
}




