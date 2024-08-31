using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Services.Configuration;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Controllers;
using Nop.Web.Framework.Mvc;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Shipping.Date;
using Nop.Services.Tax;
using Nop.Web.Factories;
using Nop.Core.Domain.Common;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop;
using System;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using System.Linq;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Models.Catalog;
using Nop.Core.Configuration;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Controllers
{
    public class B2CShoppingCartGoldController : ShoppingCartController
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly IDownloadService _downloadService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IGiftCardService _giftCardService;
        private readonly ILocalizationService _localizationService;
        private readonly INopFileProvider _fileProvider;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly MediaSettings _mediaSettings;
        private readonly OrderSettings _orderSettings;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly TaxSettings _taxSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly CatalogCustomFrameworkSettings _catalogCustomFrameworkSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly CommonSettings _commonSettings;
        private readonly ISettingService _settingService;
        private readonly IDateRangeService _dateRangeService;
        private readonly IB2CGoldModelFactory _b2cGoldModelFactory;
        private readonly IGoldPriceInfoService _goldPriceInfoService;
        private readonly IGoldPriceService _GoldPriceService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IGoldPriceCalculationService _goldPriceCalculationService;
        private readonly B2CGoldSettings _b2CGoldSettings;
        private readonly IPriceWorkContext _priceWorkContext;

        #endregion

        #region Ctor

        public B2CShoppingCartGoldController(CaptchaSettings captchaSettings, CustomerSettings customerSettings,
           ICheckoutAttributeParser checkoutAttributeParser, ICheckoutAttributeService checkoutAttributeService,
           ICurrencyService currencyService, ICustomerActivityService customerActivityService,
           IGoldPriceCalculationService goldPriceCalculationService,
           ICustomerService customerService, IDiscountService discountService, IDownloadService downloadService,
           IGenericAttributeService genericAttributeService, IGiftCardService giftCardService,
           ILocalizationService localizationService, INopFileProvider fileProvider,
           INotificationService notificationService, IPermissionService permissionService, IPictureService pictureService,
           IPriceCalculationService priceCalculationService, IPriceFormatter priceFormatter, IProductAttributeParser productAttributeParser,
           IProductAttributeService productAttributeService, IProductService productService, IShoppingCartModelFactory shoppingCartModelFactory,
           IShoppingCartService shoppingCartService, IStaticCacheManager cacheManager,
           IStoreContext storeContext, ITaxService taxService, IUrlRecordService urlRecordService, IWebHelper webHelper, IWorkContext workContext,
           IWorkflowMessageService workflowMessageService,
           ICategoryService categoryService, IManufacturerService manufacturerService, IOrderTotalCalculationService orderTotalCalculationService,
           MediaSettings mediaSettings, OrderSettings orderSettings, ShoppingCartSettings shoppingCartSettings, TaxSettings taxSettings, ShippingSettings
           shippingSettings, IEventPublisher eventPublisher,
           CommonSettings commonSettings, ISettingService settingService, IDateRangeService dateRangeService, IB2CGoldModelFactory b2cGoldModelFactory,
           IGoldPriceInfoService goldPriceInfoService, IGoldPriceService goldPriceService, IGoldProductBelongingCalculationService goldBelongingPayingService,
           B2CGoldSettings b2CGoldSettings, IPriceWorkContext priceWorkContext = null) :
           base(captchaSettings, customerSettings, checkoutAttributeParser, checkoutAttributeService, currencyService,
               customerActivityService, customerService, discountService, downloadService, genericAttributeService, giftCardService,
               localizationService, fileProvider, notificationService,
               permissionService, pictureService, priceCalculationService, priceFormatter, productAttributeParser, productAttributeService, productService, shoppingCartModelFactory,
               shoppingCartService, cacheManager, storeContext, taxService, urlRecordService, webHelper, workContext, workflowMessageService, categoryService,
               manufacturerService, orderTotalCalculationService,
               mediaSettings, orderSettings, shoppingCartSettings, taxSettings, shippingSettings, eventPublisher, commonSettings, settingService, dateRangeService)
        {
            _goldPriceCalculationService = goldPriceCalculationService;
            _captchaSettings = captchaSettings;
            _customerSettings = customerSettings;
            _checkoutAttributeParser = checkoutAttributeParser;
            _checkoutAttributeService = checkoutAttributeService;
            _currencyService = currencyService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _discountService = discountService;
            _downloadService = downloadService;
            _genericAttributeService = genericAttributeService;
            _giftCardService = giftCardService;
            _localizationService = localizationService;
            _fileProvider = fileProvider;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _priceCalculationService = priceCalculationService;
            _priceFormatter = priceFormatter;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _shoppingCartModelFactory = shoppingCartModelFactory;
            _shoppingCartService = shoppingCartService;
            _cacheManager = cacheManager;
            _storeContext = storeContext;
            _taxService = taxService;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _workContext = workContext;
            _workflowMessageService = workflowMessageService;
            _mediaSettings = mediaSettings;
            _orderSettings = orderSettings;
            _shoppingCartSettings = shoppingCartSettings;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _taxSettings = taxSettings;
            _orderTotalCalculationService = orderTotalCalculationService;
            _shippingSettings = shippingSettings;
            _eventPublisher = eventPublisher;
            _commonSettings = commonSettings;
            _settingService = settingService;
            _catalogCustomFrameworkSettings = _settingService.LoadSetting<CatalogCustomFrameworkSettings>(_storeContext.CurrentStore.Id);
            _dateRangeService = dateRangeService;
            _b2cGoldModelFactory = b2cGoldModelFactory;
            _goldPriceInfoService = goldPriceInfoService;
            _GoldPriceService = goldPriceService;
            _goldBelongingPayingService = goldBelongingPayingService;
            _goldPriceCalculationService = goldPriceCalculationService;
            _b2CGoldSettings = b2CGoldSettings;
            _priceWorkContext = priceWorkContext;
        }
        #endregion

        #region Methods

        [HttpPost]
        public override IActionResult ProductDetails_AttributeChange(int productId, bool validateAttributeConditions, bool loadPicture, IFormCollection form)
        {
            var product = _productService.GetProductById(productId);
            
            if (product == null)
            {
                return new NullJsonResult();
            }

            var errors = new List<string>();
            var attributeXml = ParseProductAttributes(product, form, errors);
            var sku = _productService.FormatSku(product, attributeXml);
            var mpn = _productService.FormatMpn(product, attributeXml);
            
            var gtin = _productService.FormatGtin(product, attributeXml);
            var attributeValues = _productAttributeParser.ParseProductAttributeValues(attributeXml);
            var totalWeight = _goldPriceCalculationService.GetGoldWeight(product, attributeValues);
            var realTimeGoldPrice = _priceWorkContext.CurrentPrice;
            var priceInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(productId);
            var goldProductBelongingCalculation = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);
            var calculations = _goldPriceCalculationService.GetGoldCalulatedInfoModel(totalWeight, priceInfo, goldProductBelongingCalculation, realTimeGoldPrice, product.AvailableForPreOrder);
            var productAvailabilityRangeId = product.ProductAvailabilityRangeId;
            product.DeliveryDateId = productAvailabilityRangeId;
            

            if (product.AvailableForPreOrder)
            {
                calculations.PreOrderTime = _dateRangeService.GetAllProductAvailabilityRangesFromCache().FirstOrDefault(a => a.Id == productAvailabilityRangeId)?.Name ?? "";
            }

            return Json(calculations);
        }

        [HttpPost]
        public override IActionResult AddProductToCart_Details(int productId, int shoppingCartTypeId, IFormCollection form)
        {
            var cartType = (ShoppingCartType)shoppingCartTypeId;
            var product = _productService.GetProductById(productId);

            if (product == null)
            {
                return Json(new
                {
                    redirect = Url.RouteUrl("Homepage")
                });
            }

            if (product.ProductType != ProductType.SimpleProduct && cartType == ShoppingCartType.ShoppingCart)
            {
                return Json(new
                {
                    success = false,
                    message = "Only simple products could be added to the cart"
                });
            }

            var updatecartitemid = 0;
            foreach (var formKey in form.Keys)
            {
                if (formKey.Equals($"addtocart_{productId}.UpdatedShoppingCartItemId", StringComparison.InvariantCultureIgnoreCase))
                {
                    int.TryParse(form[formKey], out updatecartitemid);
                    break;
                }
            }

            ShoppingCartItem updatecartitem = null;
            bool isNewCartitem = false;
            var cart = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, (ShoppingCartType)shoppingCartTypeId, _storeContext.CurrentStore.Id);
            var addToCartWarnings = new List<string>();
            var attributes = ParseProductAttributes(product, form, addToCartWarnings);
            var customerEnteredPriceConverted = decimal.Zero;
            updatecartitem = cart.FirstOrDefault(x => x.Id == updatecartitemid);

            if (updatecartitem == null)
            {
                updatecartitem = _shoppingCartService.FindShoppingCartItemInTheCart(cart, cartType, product, attributes, customerEnteredPriceConverted, null, null);

                if (updatecartitem == null)
                {
                    isNewCartitem = true;
                }
            }

            if (_shoppingCartSettings.AllowCartItemEditing && updatecartitemid > 0)
            {
                if (updatecartitem != null && product.Id != updatecartitem.ProductId)
                {
                    return Json(new
                    {
                        success = false,
                        message = "This product does not match a passed shopping cart item identifier"
                    });
                }
            }

            int productMinimumOrderQuantity = _productService.GetProductOrderMinimumQuantity(product);
            var quantity = 1;

            foreach (var formKey in form.Keys)
            {
                if (formKey.Equals($"addtocart_{productId}.EnteredQuantity", StringComparison.InvariantCultureIgnoreCase))
                {
                    int.TryParse(form[formKey], out quantity);
                    break;
                }
            }

            if (quantity < productMinimumOrderQuantity)
            {
                if (isNewCartitem)
                {
                    quantity = productMinimumOrderQuantity;
                }
                else if (quantity == productMinimumOrderQuantity - 1)
                {
                    quantity = 0;
                }
            }

            if (quantity > 0 &&
                !string.IsNullOrWhiteSpace(product.AllowedQuantities))
            {
                var allowedQuantities = _productService.GetProductAllowedQuantities(product);
                if (allowedQuantities != null && allowedQuantities.Any())
                {
                    if (isNewCartitem)
                    {
                        quantity = allowedQuantities.FirstOrDefault();
                    }
                    else
                    {
                        var quantityIndex = allowedQuantities.BinarySearch(quantity);

                        if (quantityIndex < 0)
                        {
                            var maxAllowedQuantitiesIndex = allowedQuantities.Count - 1;
                            var correctIndex = Math.Abs(quantityIndex);
                            var currentShoppingCartItemValue = updatecartitem?.Quantity ?? 0;

                            if (correctIndex <= maxAllowedQuantitiesIndex)
                            {
                                int quantityToBeSet = quantity;

                                if (quantity > currentShoppingCartItemValue)
                                {
                                    quantityToBeSet = allowedQuantities[correctIndex - 1];
                                }
                                else if (correctIndex > 1)
                                {
                                    quantityToBeSet = allowedQuantities[correctIndex - 2];
                                }
                                else if (correctIndex == 1 && maxAllowedQuantitiesIndex > 0)
                                {
                                    var currentQuantityIndex = allowedQuantities.BinarySearch(currentShoppingCartItemValue);

                                    if (currentQuantityIndex == 0 && quantity < currentShoppingCartItemValue)
                                    {
                                        quantityToBeSet = 0;
                                    }
                                    else if (currentQuantityIndex >= 0 && currentQuantityIndex < maxAllowedQuantitiesIndex)
                                    {
                                        quantityToBeSet = allowedQuantities[currentQuantityIndex + 1];
                                    }
                                }
                                quantity = quantityToBeSet;
                            }
                        }
                    }
                }
            }

            cartType = updatecartitem == null ? (ShoppingCartType)shoppingCartTypeId :
            updatecartitem.ShoppingCartType;
            
            SaveItem(updatecartitem, addToCartWarnings, product, cartType, attributes, customerEnteredPriceConverted, null, null, quantity);
            return this.GetProductToCartDetails(addToCartWarnings, cartType, product);
        }
        protected override IActionResult GetProductToCartDetails(List<string> addToCartWarnings, ShoppingCartType cartType,
        Product product)
        {
            var _currentCustomer = _workContext.CurrentCustomer;
            if (addToCartWarnings.Any())
            {
                return Json(new
                {
                    success = false,
                    message = addToCartWarnings.ToArray()
                });
            }

            switch (cartType)
            {
                case ShoppingCartType.ShoppingCart:
                default:
                    {
                        _customerActivityService.InsertActivity("PublicStore.AddToShoppingCart",
                            string.Format(_localizationService.GetResource("ActivityLog.PublicStore.AddToShoppingCart"), product.Name), product);

                        if (_shoppingCartSettings.DisplayCartAfterAddingProduct)
                        {
                            var miniShoppingCartModel = _shoppingCartModelFactory.PrepareMiniShoppingCartModel(_currentCustomer);
                            
                            return Json(new
                            {
                                redirect = Url.RouteUrl("ShoppingCart"),
                                UpdateFlyoutCartSectionHtml = _shoppingCartSettings.MiniShoppingCartEnabled
                                ? RenderViewComponentToString("FlyoutShoppingCart") : "",
                                SubTotal = miniShoppingCartModel.SubTotal,
                                SubTotalValue = miniShoppingCartModel.SubTotalValue,
                                TotalProducts = miniShoppingCartModel.TotalProducts,
                                MinOrderSubtotalAmount = _orderSettings.MinOrderSubtotalAmount,
                                MiniShoppingCartModel = miniShoppingCartModel,
                                FreeShippingOverXEnabled = _shippingSettings.FreeShippingOverXEnabled,
                                FreeShippingOverXValue = _shippingSettings.FreeShippingOverXValue,
                                UseSuperMarketAbilityOnFlyoutShoppingCart = _catalogCustomFrameworkSettings.UseSuperMarketAbilityOnFlyoutShoppingCart

                            });
                        }

                        var shoppingCarts = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, ShoppingCartType.ShoppingCart, _storeContext.CurrentStore.Id);
                        var updatetopcartsectionhtml = string.Format(
                            _localizationService.GetResource("ShoppingCart.HeaderQuantity"),
                            shoppingCarts.Sum(item => item.Quantity));
                        var updateflyoutcartsectionhtml = _shoppingCartSettings.MiniShoppingCartEnabled
                            ? RenderViewComponentToString("FlyoutShoppingCart")
                            : "";

                        return Json(new
                        {
                            success = true,
                            message = string.Format(_localizationService.GetResource("Products.ProductHasBeenAddedToTheCart.Link"),
                                Url.RouteUrl("ShoppingCart")),
                            updatetopcartsectionhtml,
                            updateflyoutcartsectionhtml
                        });
                    }
            }
        }

        #endregion
    }
}
