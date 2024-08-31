using Microsoft.AspNetCore.Mvc;

using Nop.Core.Domain.Catalog;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Models.Catalog;

using NopLine.Nop.Framework.Components;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Factories;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "ProductMobilePreOrder")]
    public class B2CGoldProductMobilePreOrderViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldContactInfoViewModelFactory _goldContactInfoViewModelFactory;

        #endregion

        #region Ctor

        public B2CGoldProductMobilePreOrderViewComponent(IGoldContactInfoViewModelFactory goldContactInfoViewModelFactory)
        {
            _goldContactInfoViewModelFactory = goldContactInfoViewModelFactory;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var model = additionalData as ProductDetailsModel;
            return View(ViewPathKeys.GoldProductMobilePreOrderViewPath, model.Sku);
        }
        
        #endregion
    }
}