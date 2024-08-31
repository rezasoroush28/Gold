using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using MongoDB.Driver.Linq;

using Nop.Services.Common;

using System;

using Tesla.Plugin.Widgets.B2CGold;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.ActionFilters
{
    public class FinalPurchaseActionfilter : ActionFilterAttribute
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly B2CGoldSettings _b2CGoldSettings;
        #endregion

        #region Ctor

        public FinalPurchaseActionfilter(IGenericAttributeService genericAttributeService, B2CGoldSettings b2CGoldSettings)
        {
            _genericAttributeService = genericAttributeService;
            _b2CGoldSettings = b2CGoldSettings;
        }

        #endregion

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get the orderId from the query string
            string orderIdStr = filterContext.HttpContext.Request.Query["orderId"];
            // You can now use orderId as needed
            if (!string.IsNullOrEmpty(orderIdStr) && int.TryParse(orderIdStr, out var orderId))
            {
                // 1. Get the generic attribute of LatestCalculatedDateTimeOnUtc
                var genericAttr = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(
                    orderId,
                    nameof(Order),
                    GenericAttributeKeys.OrderCreatedOrUpdatedOnKey
                );

                // 2. Check if the attribute exists and is a valid DateTime
                if (genericAttr != null && DateTime.TryParse(genericAttr.Value, out var orderCreatedOrUpdatedOnUtc))
                {
                    // 3. Check the validity of time
                    if (DateTime.UtcNow.Subtract(orderCreatedOrUpdatedOnUtc).Seconds <= _b2CGoldSettings.OrderValidateTimeInSeconds)
                    {
                        // A. Ok do nothing to continue
                        base.OnActionExecuting(filterContext);
                        return;
                    }

                    filterContext.Result = new RedirectResult($"/orderdetails/{orderId}");
                }
                // B. Redirect to Order Details to prevent payment if time is invalid or attribute is missing
            }
        }

        #endregion
    }
}

