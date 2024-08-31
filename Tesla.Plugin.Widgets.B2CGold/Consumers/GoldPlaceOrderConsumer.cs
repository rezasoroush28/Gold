using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Orders;

using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using Tesla.Plugin.Widgets.Gold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Consumers
{
    public class GoldPlaceOrderConsumer : IConsumer<OrderPlacedEvent>
    {
        #region Fields

        private readonly B2CGoldSettings _b2CGoldSettings;
        private readonly IGoldPriceInfoService _goldPriceInfoService;
        private readonly IGoldPriceCalculationService _goldPriceCalculationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDbContext _dbContext;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IRepository<GoldPriceInfo> _productGoldInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGoldPriceInfoService _goldInfoService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IGoldPriceService _goldPriceService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IPriceWorkContext _priceWorkContext;

        #endregion

        #region Ctor

        public GoldPlaceOrderConsumer(IGenericAttributeService genericAttributeService,
            IOrderService orderService, IWorkContext workContext,
            IRepository<GoldPriceInfo> productGoldInfoRepository, IHttpContextAccessor httpContextAccessor,
            IDbContext dbContext, IGoldPriceInfoService goldInfoService,
            IProductAttributeParser productAttributeParser,
            IGoldPriceCalculationService goldPriceCalculationService, IGoldPriceService goldPriceService,
            IGoldPriceInfoService goldPriceInfoService,
            IGoldProductBelongingCalculationService goldBelongingPayingService, IPriceWorkContext priceWorkContext = null, B2CGoldSettings b2CGoldSettings = null)
        {
            _genericAttributeService = genericAttributeService;
            _productGoldInfoRepository = productGoldInfoRepository;
            _orderService = orderService;
            _workContext = workContext;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _goldInfoService = goldInfoService;
            _productAttributeParser = productAttributeParser;
            _goldPriceCalculationService = goldPriceCalculationService;
            _goldPriceService = goldPriceService;
            _goldPriceInfoService = goldPriceInfoService;
            _goldBelongingPayingService = goldBelongingPayingService;
            _priceWorkContext = priceWorkContext;
            _b2CGoldSettings = b2CGoldSettings;
        }

        #endregion

        #region Methods

        public void HandleEvent(OrderPlacedEvent eventMassage)
        {
            var order = eventMassage.Order;
            foreach (var item in order.OrderItems)
            {
                var genAtrribute = new GenericAttribute
                {
                    EntityId = item.Id,
                    KeyGroup = nameof(OrderItem),
                    Key = GenericAttributeKeys.OrderItemCalculationsKey,
                    StoreId = 0
                };

                var productId = item.Product.Id;
                var attrXml = item.AttributesXml;
                var product = item.Product;
                var produtIsPreOrder = item.Product.AvailableForPreOrder;
                var attributeValues = _productAttributeParser.ParseProductAttributeValues(attrXml);
                var totalWeight = _goldPriceCalculationService.GetGoldWeight(product, attributeValues);
                var realTimeGoldPrice = _priceWorkContext.CurrentPrice;
                var priceInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(productId);
                var goldProductBelongingCalculation = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);
                var calculations = _goldPriceCalculationService.GetGoldCalulatedInfoModel(totalWeight, priceInfo, goldProductBelongingCalculation, realTimeGoldPrice, product.AvailableForPreOrder);
                genAtrribute.Value = JsonConvert.SerializeObject(calculations);
                _genericAttributeService.InsertAttribute(genAtrribute);
            }

            var createdOn = order.CreatedOnUtc;
            var timeGenAtrribute = new GenericAttribute
            {
                EntityId = order.Id,
                KeyGroup = nameof(Order),
                Key = GenericAttributeKeys.OrderCreatedOrUpdatedOnKey,
                Value = createdOn.ToString(),
                StoreId = 0
            };

            _genericAttributeService.InsertAttribute(timeGenAtrribute);

        }

        #endregion
    }
}
