using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class ProductGoldIngredientModel : BaseNopEntityModel
    {
        public ProductGoldIngredientModel()
        {
            ProductGoldIngredientList = new List<GoldIngredientModel>();
        }
        public IList<GoldIngredientModel> ProductGoldIngredientList { get; set; }
    }
}
