using Microsoft.AspNetCore.Mvc;

using NopLine.Nop.Framework.Components;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Factories;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "B2CGoldGoldProposalValue")]
    public class B2CGoldGoldProposalValueViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldProposalValueViewModelFactory _goldProposalValueModelFactory;

        #endregion

        #region Ctor

        public B2CGoldGoldProposalValueViewComponent(IGoldProposalValueViewModelFactory goldProposalValueModelFactory)
        {
            _goldProposalValueModelFactory = goldProposalValueModelFactory;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var model = _goldProposalValueModelFactory.PrepareGoldProposalValueViewModel();
            return View(ViewPathKeys.GoldProposalValueViewPath, model);
        }

        #endregion
    }
}