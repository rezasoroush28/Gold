using Microsoft.AspNetCore.Mvc;

using Nop.Core.Domain.Orders;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Models.Order;

using NopLine.Nop.Framework.Components;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Components
{
    [ViewComponent(Name = "B2CGoldOrderDetailsViewComponent")]
    public class B2CGoldOrderDetailsViewComponent : BaseNopLineComponent
    {
        #region Fields

        private readonly IGoldOrderDetailsModelFactory _goldOrderDetailsModelFactory;

        #endregion

        #region Ctor

        public B2CGoldOrderDetailsViewComponent(IGoldOrderDetailsModelFactory goldOrderDetailsModelFactory)
        {
            _goldOrderDetailsModelFactory = goldOrderDetailsModelFactory;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var orderDetails = additionalData as OrderDetailsModel;
            var orderDetailsGoldModel = new OrderDetailsGoldModel();
            var orderDomian = new Order();
            orderDetailsGoldModel = _goldOrderDetailsModelFactory.PrepareOrderDetailsGoldModel(orderDetails, orderDetailsGoldModel, orderDomian);
            orderDetailsGoldModel.OrderId = orderDetails.Id;
            orderDetailsGoldModel.IsPaid = orderDetails.PaymentStatusEnum == Nop.Core.Domain.Payments.PaymentStatus.Paid;

            return View(ViewPathKeys.GoldOrderDetailsViewPath, orderDetailsGoldModel);
        }

        #endregion
    }
}