using Tesla.Plugin.Widgets.B2CGold.Models;
using System.Collections.Generic;
using System.Linq;
using Nop.Web.Models.Order;
using Nop.Services.Common;
using Newtonsoft.Json;
using DNTPersianUtils.Core;
using Nop.Core.Domain.Orders;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public class GoldOrderDetailsModelFactory : IGoldOrderDetailsModelFactory
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;

        #endregion

        #region Ctor

        public GoldOrderDetailsModelFactory(IGenericAttributeService genericAttributeService = null)
        {
            _genericAttributeService = genericAttributeService;
        }


        #endregion

        public OrderDetailsGoldModel PrepareOrderDetailsGoldModel(OrderDetailsModel orderModel, OrderDetailsGoldModel model,Order OrderDomain)
        {
            var orderItemIds = orderModel.Items.Select(o => o.Id).ToList();
            var genericAttribiuteItems = _genericAttributeService.GetGenericAttributesListByKeyAndIds("OrderItemCalculations", orderItemIds);
            var allCalculationJsons = genericAttribiuteItems.Select(a => a.Value).ToList();
            var allCalculation = new List<GoldCalulatedInfoModel>();

            foreach (var item in allCalculationJsons)
            {
                allCalculation.Add(JsonConvert.DeserializeObject<GoldCalulatedInfoModel>(item));
            }

            model.TotalGoldPrice = 0;

            foreach (var item in allCalculation)
            {
                model.TotalGoldPrice += item.ProductGoldPrice;
                model.TotalWeightCorrection += item.TotalWeight;
                model.PreOrderPrices += item.PreOrderPrice;
            }

            model.TotalDiscount = orderModel.SumOfAllOrderDiscountsAmount;
            model.TotalOrderPrice = model.TotalGoldPrice - model.TotalDiscount + model.ShippingCost;
            model.LatestCurrentGoldPrice = allCalculation[0].GoldCurrentPrice;
            model.ShippingCost = OrderDomain.OrderShippingInclTax;
            model.TotalOrderPriceInLetter = model.TotalOrderPrice.NumberToText(Language.Persian);
            
            return model;
        }

        #region Methods

        #endregion

        #region Utilities

        #endregion
    }

}