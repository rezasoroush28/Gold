using Microsoft.AspNetCore.Mvc.Rendering;

using Nop;
using Nop.Web.Framework.Models;

using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Models
{
    public class GoldInvoiceModel : BaseNopEntityModel
    {
        #region Properties

        public decimal BelongingPrice { get; set; }

        #endregion
    }
}
