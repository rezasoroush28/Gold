using Microsoft.AspNetCore.Mvc.Rendering;

using Nop;
using Nop.Web.Framework.Models;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class GoldIngredientModel : BaseNopEntityModel
    {
        #region Ctor

        public GoldIngredientModel()
        {
            SpecificationList = new List<ProductGoldIngredientSpecificationModel>();
        }

        #endregion

        #region Properties

        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public string IngredientName { get; set; }
        public decimal IngredientWeight { get; set; }
        public string IngredientWeightFormmater => IngredientWeight.GetDecimalRoundFormated();
        public decimal IngredientLength { get; set; }
        public string IngredientLengthFormmater => IngredientLength.GetDecimalRoundFormated();
        public decimal IngredientWidth { get; set; }
        public string IngredientWidthFormmater => IngredientWidth.GetDecimalRoundFormated();
        public decimal IngredientHeight { get; set; }
        public string IngredientHeightFormmater => IngredientHeight.GetDecimalRoundFormated();

        public string IngredientMeasureWeightName { get; set; }
        public IList<ProductGoldIngredientSpecificationModel> SpecificationList { get; set; }

        #endregion
    }
}
