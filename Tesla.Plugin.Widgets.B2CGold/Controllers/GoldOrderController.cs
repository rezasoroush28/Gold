using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver.Linq;

using Newtonsoft.Json;

using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.ResponseModel;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Shipping;
using Nop.Web.Factories;

using Tesla.Plugin.Widgets.B2CGold;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Tesla.Plugin.Widgets.Gold.Services;

namespace Nop.Web.Controllers
{
    public partial class GoldOrderController : BasePublicController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IGoldPriceInfoService _goldPriceInfoService;
        private readonly IGoldPriceCalculationService _goldPriceCalculationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDbContext _dbContext;
        private readonly IWorkContext _workContext;
        private readonly IRepository<GoldPriceInfo> _productGoldInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGoldPriceInfoService _goldInfoService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IGoldPriceService _goldPriceService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IPriceWorkContext _priceWorkContext;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IPdfService _pdfService;
        private readonly IShipmentService _shipmentService;
        private readonly IWebHelper _webHelper;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly B2CGoldSettings _b2CGoldSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public GoldOrderController(IOrderModelFactory orderModelFactory,
            IOrderProcessingService orderProcessingService,
            IOrderService orderService,
            IPaymentService paymentService,
            IPdfService pdfService,
            IShipmentService shipmentService,
            IWebHelper webHelper,
            IWorkContext workContext,
            RewardPointsSettings rewardPointsSettings,
            ICacheManager cacheManager,
            IPictureService pictureService,
            ISettingService settingService,
            IGoldPriceInfoService goldPriceInfoService,
            IGoldPriceCalculationService goldPriceCalculationService,
            IGenericAttributeService genericAttributeService,
            IDbContext dbContext,
            IRepository<GoldPriceInfo> productGoldInfoRepository,
            IHttpContextAccessor httpContextAccessor,
            IGoldPriceInfoService goldInfoService,
            IProductAttributeParser productAttributeParser,
            IGoldPriceService goldPriceService,
            IGoldProductBelongingCalculationService goldBelongingPayingService,
            IPriceWorkContext priceWorkContext,
            IProductService productService,
            B2CGoldSettings b2CGoldSettings)
        {
            _orderModelFactory = orderModelFactory;
            _orderProcessingService = orderProcessingService;
            _orderService = orderService;
            _paymentService = paymentService;
            _pdfService = pdfService;
            _shipmentService = shipmentService;
            _webHelper = webHelper;
            _workContext = workContext;
            _rewardPointsSettings = rewardPointsSettings;
            _cacheManager = cacheManager;
            _pictureService = pictureService;
            _goldPriceInfoService = goldPriceInfoService;
            _goldPriceCalculationService = goldPriceCalculationService;
            _genericAttributeService = genericAttributeService;
            _dbContext = dbContext;
            _productGoldInfoRepository = productGoldInfoRepository;
            _httpContextAccessor = httpContextAccessor;
            _goldInfoService = goldInfoService;
            _productAttributeParser = productAttributeParser;
            _goldPriceService = goldPriceService;
            _goldBelongingPayingService = goldBelongingPayingService;
            _priceWorkContext = priceWorkContext;
            _productService = productService;
            _b2CGoldSettings = b2CGoldSettings;
        }

        #endregion

        #region Methods

        [HttpPost]
        //My account / Orders
        public IActionResult RecalculateGoldPrice(int orderId)
        {
            if (orderId == 0)
            {
                throw new ArgumentException(nameof(orderId));
            }

            var order = _orderService.GetOrderById(orderId);
            var orderItemIds = order.OrderItems.Select(a => a.Id).ToList();
            var genAtrributes = _genericAttributeService.GetGenericAttributesListByKeyAndIds(GenericAttributeKeys.OrderItemCalculationsKey, orderItemIds);

            foreach (var item in order.OrderItems)
            {
                var genricAttr = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(item.Id, nameof(OrderItem), GenericAttributeKeys.OrderItemCalculationsKey);
                var product = _productService.GetProductById(item.ProductId);
                var produtIsPreOrder = product.AvailableForPreOrder;
                var totalWeight = JsonConvert.DeserializeObject<GoldCalulatedInfoModel>(genricAttr.Value).TotalWeight;
                var realTimeGoldPrice = _priceWorkContext.CurrentPrice;
                var priceInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(item.ProductId);
                var goldProductBelongingCalculation = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);
                var calculations = _goldPriceCalculationService.GetGoldCalulatedInfoModel(totalWeight, priceInfo,
                    goldProductBelongingCalculation, realTimeGoldPrice, product.AvailableForPreOrder);
                genricAttr.Value = JsonConvert.SerializeObject(calculations);
                _genericAttributeService.UpdateAttribute(genricAttr);
                item.UnitPriceInclTax = calculations.TotalPrice;
            }

            order.OrderTotal = order.OrderItems.Sum(a => a.UnitPriceInclTax);
            var orderCreatedOn = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(order, GenericAttributeKeys.OrderCreatedOrUpdatedOnKey);
            var validDateTime = DateTime.UtcNow;
            orderCreatedOn.Value = validDateTime.ToString();
            _genericAttributeService.UpdateAttribute(orderCreatedOn);

            var response = new GeneralResponseModel<int>
            {
                Success = true,
                Data = _b2CGoldSettings.OrderValidateTimeInSeconds
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetTheCountDownStarterPoint(int orderId)
        {
            var response = new GeneralResponseModel<int>();

            if (orderId == 0)
            {
                throw new ArgumentException(nameof(orderId));
            }

            var order = _orderService.GetOrderById(orderId);
            var orderCreatedOnString = _genericAttributeService.GetGenericAttributeByEntityIdGroupAndKey(order.Id, nameof(Order), GenericAttributeKeys.OrderCreatedOrUpdatedOnKey).Value;
            var orderCreatedOn = DateTime.Parse(orderCreatedOnString);
            var distance = orderCreatedOn.Subtract(DateTime.UtcNow);
            orderCreatedOn = new DateTime(orderCreatedOn.Year, orderCreatedOn.Month, orderCreatedOn.Day, orderCreatedOn.Hour, orderCreatedOn.Minute, orderCreatedOn.Second); // Example earlier date
            var dateNow = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second); // Example earlier date
            double totalSeconds = (dateNow - orderCreatedOn ).TotalSeconds;
            if (totalSeconds < _b2CGoldSettings.OrderValidateTimeInSeconds)
            {
                response = new GeneralResponseModel<int>
                {
                    Success = true,
                    Data = (int)(_b2CGoldSettings.OrderValidateTimeInSeconds - totalSeconds)
                };
            }

            return Json(response);

        }

        #endregion
    }
}