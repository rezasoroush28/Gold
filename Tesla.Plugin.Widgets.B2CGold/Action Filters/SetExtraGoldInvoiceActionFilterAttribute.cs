using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

using Nop.Core.Domain.Orders;
using Nop.Services.Common;
using Nop.Services.Orders;
using Nop.Web.Models.Order;

using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Models;

namespace Tesla.Plugin.Widgets.B2CGold.ActionFilters
{
    public class SetExtraGoldInvoiceActionFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IOrderService _orderService;

        #endregion

        #region Ctor

        public SetExtraGoldInvoiceActionFilterAttribute(IGenericAttributeService genericAttributeService,
            IOrderService orderService)
        {
            _genericAttributeService = genericAttributeService;
            _orderService = orderService;
        }

        #endregion

        #region Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (!(filterContext.Result == null || !(filterContext.Result is ViewResult)
                || (filterContext.Result as ViewResult).Model == null || !((filterContext.Result as ViewResult).Model is OrderDetailsModel)))
            {
                var orderModel = (filterContext.Result as ViewResult).Model as OrderDetailsModel;
                //foreach (var orderItemModel in orderModel.Items)
                //{
                //    var genricAttr = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(orderItemModel.Id, nameof(OrderItem), GenericAttributeKeys.OrderItemCalculationsKey);
                //    var belongingPrice = JsonConvert.DeserializeObject<GoldCalulatedInfoModel>(genricAttr.Value).BelongingPrice;
                //    if (genricAttr != null && belongingPrice != 0)
                //    {
                //        orderItemModel.ProductName += $" با احتساب متعلقات و سایر";
                //    }
                //}

                foreach (var orderItemModel in orderModel.Items)
                {
                    // Retrieve generic attribute by entity ID, group, and key
                    var genericAttr = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(orderItemModel.Id, nameof(OrderItem), GenericAttributeKeys.OrderItemCalculationsKey);

                    // Ensure genericAttr is not null and has a valid value
                    if (genericAttr != null && !string.IsNullOrEmpty(genericAttr.Value))
                    {
                        // Deserialize the value into GoldCalculatedInfoModel
                        var goldCalculatedInfo = JsonConvert.DeserializeObject<GoldCalulatedInfoModel>(genericAttr.Value);

                        // Check if BelongingPrice is not zero
                        if (goldCalculatedInfo.BelongingPrice != 0)
                        {
                            // Append text to the product name if it hasn't been added already
                            if (!orderItemModel.ProductName.Contains(" با احتساب متعلقات و سایر"))
                            {
                                orderItemModel.ProductName += " با احتساب متعلقات و سایر";
                            }
                        }
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        #endregion
    }
}
