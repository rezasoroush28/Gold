using Nop.Web.Framework.Models;

using System.Collections.Generic;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class ProductGoldBelongingsModel : BaseNopEntityModel
    {
        #region Ctor

        public ProductGoldBelongingsModel()
        {
            ProductGoldBelongingsList = new List<GoldBelongingMappingModel>();
        }

        #endregion

        #region Properties

        public IList<GoldBelongingMappingModel> ProductGoldBelongingsList { get; set; }
        public decimal BelongingsPrice { get; set; }

        #endregion
    }
}
