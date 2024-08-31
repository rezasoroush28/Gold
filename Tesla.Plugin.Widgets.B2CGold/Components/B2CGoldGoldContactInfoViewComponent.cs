using Microsoft.AspNetCore.Mvc;

using Nop.Core.Domain.Catalog;

using NopLine.Nop.Framework.Components;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Factories;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "B2CGoldGoldContactInfo")]
    public class B2CGoldGoldContactInfoViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldContactInfoViewModelFactory _goldContactInfoViewModelFactory;

        #endregion

        #region Ctor

        public B2CGoldGoldContactInfoViewComponent(IGoldContactInfoViewModelFactory goldContactInfoViewModelFactory)
        {
            _goldContactInfoViewModelFactory = goldContactInfoViewModelFactory;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var model = _goldContactInfoViewModelFactory.PrepareGoldContactInfoViewModel();
            return View(ViewPathKeys.GoldContactInfoViewPath, model);
        }
        
        #endregion
    }
}