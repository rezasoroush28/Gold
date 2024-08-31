using System;
using System.Collections.Generic;

using Amazon.Runtime.Internal.Util;

using Microsoft.AspNetCore.Http;

using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.ShoppingCart;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using NUglify;
using System.Linq;
using Nop.Web.Factories;
using Nop.Core.Domain;
using Nop;
using Nop.Web;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public class B2CGoldShoppingCartModelFactory : Nop.Web.Factories.ShoppingCartModelFactory
    {
        #region Fields

        private readonly AddressSettings _addressSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly Nop.Core.Domain.Common.CommonSettings _commonSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly IAddressModelFactory _addressModelFactory;
        private readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly IDownloadService _downloadService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IGiftCardService _giftCardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IPaymentPluginManager _paymentPluginManager;
        private readonly IPaymentService _paymentService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IProductService _productService;
        private readonly IShippingPluginManager _shippingPluginManager;
        private readonly IShippingService _shippingService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVendorService _vendorService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly OrderSettings _orderSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly TaxSettings _taxSettings;
        private readonly VendorSettings _vendorSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly ISettingService _settingService;
        private readonly StoreInformationCustomFrameworkSettings _storeInformationCustomFrameworkSettings;
        private readonly IGoldIngredientService _goldIngredientService;
        private readonly IGoldIngredientSpecificationService _goldIngredientSpecificationService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IRepository<ProductGroupBelonging> _productGroupBelongingRepository;
        private readonly IRepository<ProductGoldBelongingMapping> _productGoldBelongingMappingRepository;
        private readonly IRepository<MeasureWeight> _measureWeightRepository;
        private readonly IRepository<GoldIngredient> _goldIngredientRepository;
        private readonly IRepository<GoldIngredientSpecification> _goldIngredientSpecificationRepository;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IProductGoldBelongingMappingService _productGoldBelongingMappingService;
        private readonly IGoldBelongingTypeService _goldBelongingTypeService;
        private readonly IMeasureService _measureService;
        private readonly ILogger _logger;
        private readonly IProductAttributeParser _productAttributeParser;

        #endregion

        #region Ctor

        public B2CGoldShoppingCartModelFactory(AddressSettings addressSettings, CaptchaSettings captchaSettings,
            CatalogSettings catalogSettings, Nop.Core.Domain.Common.CommonSettings commonSettings,
            CustomerSettings customerSettings, IAddressModelFactory addressModelFactory, ICheckoutAttributeFormatter checkoutAttributeFormatter,
            ICheckoutAttributeParser checkoutAttributeParser, ICheckoutAttributeService checkoutAttributeService, ICountryService countryService,
            ICurrencyService currencyService, ICustomerService customerService, IDiscountService discountService, IDownloadService downloadService,
            IGenericAttributeService genericAttributeService, IGiftCardService giftCardService, IHttpContextAccessor httpContextAccessor,
            ILocalizationService localizationService, IOrderProcessingService orderProcessingService, IOrderTotalCalculationService orderTotalCalculationService,
            IPaymentPluginManager paymentPluginManager, IPaymentService paymentService, IPermissionService permissionService, IPictureService pictureService,
            IPriceCalculationService priceCalculationService, IPriceFormatter priceFormatter, IProductAttributeFormatter productAttributeFormatter,
            IProductService productService, IShippingPluginManager shippingPluginManager, IShippingService shippingService, IShoppingCartService shoppingCartService,
            IStateProvinceService stateProvinceService, IStaticCacheManager cacheManager, IStoreContext storeContext,
            ITaxService taxService, IUrlRecordService urlRecordService, IVendorService vendorService, IWebHelper webHelper,
            IWorkContext workContext, MediaSettings mediaSettings, OrderSettings orderSettings, RewardPointsSettings rewardPointsSettings,
            ShippingSettings shippingSettings, ShoppingCartSettings shoppingCartSettings, TaxSettings taxSettings, VendorSettings vendorSettings,
            IEventPublisher eventPublisher, ISettingService settingService) : base(addressSettings, captchaSettings, catalogSettings, commonSettings,
                customerSettings, addressModelFactory, checkoutAttributeFormatter, checkoutAttributeParser, checkoutAttributeService, countryService,
                currencyService, customerService, discountService, downloadService, genericAttributeService, giftCardService, httpContextAccessor,
                localizationService, orderProcessingService, orderTotalCalculationService, paymentPluginManager, paymentService, permissionService,
                pictureService, priceCalculationService, priceFormatter, productAttributeFormatter, productService, shippingPluginManager, shippingService,
                shoppingCartService, stateProvinceService, cacheManager, storeContext, taxService, urlRecordService, vendorService, webHelper, workContext,
                mediaSettings, orderSettings, rewardPointsSettings, shippingSettings, shoppingCartSettings, taxSettings, vendorSettings, eventPublisher, settingService)
        {
            _addressSettings = addressSettings;
            _captchaSettings = captchaSettings;
            _catalogSettings = catalogSettings;
            _commonSettings = commonSettings;
            _customerSettings = customerSettings;
            _addressModelFactory = addressModelFactory;
            _checkoutAttributeFormatter = checkoutAttributeFormatter;
            _checkoutAttributeParser = checkoutAttributeParser;
            _checkoutAttributeService = checkoutAttributeService;
            _countryService = countryService;
            _currencyService = currencyService;
            _customerService = customerService;
            _discountService = discountService;
            _downloadService = downloadService;
            _genericAttributeService = genericAttributeService;
            _giftCardService = giftCardService;
            _httpContextAccessor = httpContextAccessor;
            _localizationService = localizationService;
            _orderProcessingService = orderProcessingService;
            _orderTotalCalculationService = orderTotalCalculationService;
            _paymentPluginManager = paymentPluginManager;
            _paymentService = paymentService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _priceCalculationService = priceCalculationService;
            _priceFormatter = priceFormatter;
            _productAttributeFormatter = productAttributeFormatter;
            _productService = productService;
            _shippingPluginManager = shippingPluginManager;
            _shippingService = shippingService;
            _shoppingCartService = shoppingCartService;
            _stateProvinceService = stateProvinceService;
            _cacheManager = cacheManager;
            _storeContext = storeContext;
            _taxService = taxService;
            _urlRecordService = urlRecordService;
            _vendorService = vendorService;
            _webHelper = webHelper;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _orderSettings = orderSettings;
            _rewardPointsSettings = rewardPointsSettings;
            _shippingSettings = shippingSettings;
            _shoppingCartSettings = shoppingCartSettings;
            _taxSettings = taxSettings;
            _vendorSettings = vendorSettings;
            _eventPublisher = eventPublisher;
            _settingService = settingService;
            _storeInformationCustomFrameworkSettings = _settingService.LoadSetting<StoreInformationCustomFrameworkSettings>(_storeContext.CurrentStore.Id);

        }

        #endregion

        #region Methods

        public override OrderTotalsModel PrepareOrderTotalsModel(IList<ShoppingCartItem> cart, bool isEditable, bool? isNotShippable = null)
        {
            var model = new OrderTotalsModel
            {
                IsEditable = isEditable,
                IsNotShippable = isNotShippable ?? !cart.Any(sci => sci.Product.IsShipEnabled)
            };

            model.CartHasWarnings = _shoppingCartService.CartHasWarnings(cart);

            if (cart.Any())
            {
                var subTotalIncludingTax = _workContext.TaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal;
                _orderTotalCalculationService.GetShoppingCartSubTotal(cart, subTotalIncludingTax, out var orderSubTotalDiscountAmountBase, out var _, out var subTotalWithoutDiscountBase, out var _);
                var subtotalBase = subTotalWithoutDiscountBase;
                var subtotal = _currencyService.ConvertFromPrimaryStoreCurrency(subtotalBase, _workContext.WorkingCurrency);
                var subtotalStringify = _priceFormatter.FormatPrice(subtotal, false, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);

                if (orderSubTotalDiscountAmountBase > decimal.Zero)
                {
                    var orderSubTotalDiscountAmount = _currencyService.ConvertFromPrimaryStoreCurrency(orderSubTotalDiscountAmountBase, _workContext.WorkingCurrency);
                    model.SubTotalDiscount = _priceFormatter.FormatPrice(orderSubTotalDiscountAmount, true, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);
                    var subTotalDiscountValueString = _priceFormatter.FormatPrice(-orderSubTotalDiscountAmount, false, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);
                    subTotalDiscountValueString.ConvertStringifiedPriceToDecimal(out decimal subTotalDiscountValue);
                    model.SubTotalDiscountValue = Math.Abs(subTotalDiscountValue);
                }

                var checkoutAttributes = new List<Tuple<string, decimal>>();
                var customer = _workContext.CurrentCustomer;
                var checkoutAttributesXml = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.CheckoutAttributes, _storeContext.CurrentStore.Id);
                var attributeValues = _checkoutAttributeParser.ParseCheckoutAttributeValues(checkoutAttributesXml);
                if (attributeValues != null && attributeValues.Any())
                {
                    foreach (var attributeValue in attributeValues)
                    {
                        var caExclTax = _taxService.GetCheckoutAttributePrice(attributeValue, false, customer, out var taxRate);
                        checkoutAttributes.Add(new Tuple<string, decimal>(attributeValues.FirstOrDefault().Name, caExclTax));
                    }
                }

                decimal subTotalValue = decimal.Zero;

                if (!string.IsNullOrEmpty(subtotalStringify))
                {
                    subtotalStringify.ConvertStringifiedPriceToDecimal(out subTotalValue);

                    if (checkoutAttributes.Count > 0)
                    {
                        subTotalValue -= checkoutAttributes.Sum(a => a.Item2);
                    }

                }

                model.CheckoutAttributes = checkoutAttributes;
                model.SubTotalValue = subTotalValue;
                model.SubTotal = _priceFormatter.FormatPrice(subTotalValue, false, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);
                model.WorkingCurrencyName = _workContext.WorkingCurrency.Name;
                var productCount = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, ShoppingCartType.ShoppingCart).Sum(a => a.Quantity);
                model.ProductCount = productCount;
                var _urlPath = _httpContextAccessor.HttpContext.Request.Path;
                bool isCart = string.Compare(_urlPath, "/cart", true) == 0;
                bool isShipping = false;
                bool isPayment = false;
                var orderSummeryViewType = OrderSummeryViewType.Cart;
                var endPageUrlExpression = Nop.Web.Helper.Context.GetAbsoluteUrl().ToLower().Split("/").LastOrDefault();

                switch (endPageUrlExpression)
                {
                    case "shipping":
                    case "billingaddress":
                        orderSummeryViewType = OrderSummeryViewType.Shipping;
                        isCart = false;
                        isShipping = true;
                        isPayment = false;
                        break;
                    case "payment":
                    case "paymentmethod":
                        orderSummeryViewType = OrderSummeryViewType.Payment;
                        isCart = false;
                        isShipping = false;
                        isPayment = true;
                        break;
                }
                model.IsCartStep = isCart;
                model.IsShippingStep = isShipping;
                model.IsPaymentStep = isPayment;
                model.OrderSummeryViewType = orderSummeryViewType;
                var shippingRateComputationMethods = _shippingPluginManager.LoadActivePlugins(_workContext.CurrentCustomer, _storeContext.CurrentStore.Id);
                model.RequiresShipping = _shoppingCartService.ShoppingCartRequiresShipping(cart);

                if (model.RequiresShipping)
                {
                    var shoppingCartShippingBase = _orderTotalCalculationService.GetShoppingCartShippingTotal(cart, shippingRateComputationMethods);
                    if (shoppingCartShippingBase.HasValue)
                    {
                        var shoppingCartShipping = _currencyService.ConvertFromPrimaryStoreCurrency(shoppingCartShippingBase.Value, _workContext.WorkingCurrency);
                        model.Shipping = _priceFormatter.FormatShippingPrice(shoppingCartShipping, true);
                       var shippingOption = _genericAttributeService.GetAttribute<ShippingOption>(_workContext.CurrentCustomer,
                        NopCustomerDefaults.SelectedShippingOptionAttribute, _storeContext.CurrentStore.Id);
                        
                        if (shippingOption != null)
                        {
                            model.SelectedShippingMethod = shippingOption.Name;
                            model.IsFreightForward = shoppingCartShipping == decimal.Zero && shippingOption.FreightForward;
                        }
                    }
                }
                else
                {
                    model.HideShippingTotal = _shippingSettings.HideShippingTotal;
                }
                var paymentMethodSystemName = _genericAttributeService.GetAttribute<string>(_workContext.CurrentCustomer, NopCustomerDefaults.SelectedPaymentMethodAttribute, _storeContext.CurrentStore.Id);
                var paymentMethodAdditionalFee = _paymentService.GetAdditionalHandlingFee(cart, paymentMethodSystemName);
                var paymentMethodAdditionalFeeWithTaxBase = _taxService.GetPaymentMethodAdditionalFee(paymentMethodAdditionalFee, _workContext.CurrentCustomer);
                
                if (paymentMethodAdditionalFeeWithTaxBase > decimal.Zero)
                {
                    var paymentMethodAdditionalFeeWithTax = _currencyService.ConvertFromPrimaryStoreCurrency(paymentMethodAdditionalFeeWithTaxBase, _workContext.WorkingCurrency);
                    model.PaymentMethodAdditionalFee = _priceFormatter.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeWithTax, true);
                }

                var displayTax = true;
                var displayTaxRates = true;
                
                if (_taxSettings.HideTaxInOrderSummary && _workContext.TaxDisplayType == TaxDisplayType.IncludingTax)
                {
                    displayTax = false;
                    displayTaxRates = false;
                }
                else
                {
                    var shoppingCartTaxBase = _orderTotalCalculationService.GetTaxTotal(cart, shippingRateComputationMethods, out var taxRates);
                    var shoppingCartTax = _currencyService.ConvertFromPrimaryStoreCurrency(shoppingCartTaxBase, _workContext.WorkingCurrency);

                    if (shoppingCartTaxBase == 0 && _taxSettings.HideZeroTax)
                    {
                        displayTax = false;
                        displayTaxRates = false;
                    }
                    else
                    {
                        displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
                        displayTax = !displayTaxRates;
                        model.Tax = _priceFormatter.FormatPrice(shoppingCartTax, true, false);
                        foreach (var tr in taxRates)
                        {
                            model.TaxRates.Add(new OrderTotalsModel.TaxRate
                            {
                                Rate = _priceFormatter.FormatTaxRate(tr.Key),
                                Value = _priceFormatter.FormatPrice(_currencyService.ConvertFromPrimaryStoreCurrency(tr.Value, _workContext.WorkingCurrency), true, false),
                            });
                        }
                    }
                }

                model.DisplayTaxRates = displayTaxRates;
                model.DisplayTax = displayTax;
                var shoppingCartTotalBase = _orderTotalCalculationService.GetShoppingCartTotal(cart,
                    out var orderTotalDiscountAmountBase,
                    out var appliedDiscountsToCalculateOrderTotal,
                    out var appliedGiftCards,
                    out var redeemedRewardPoints,
                    out var redeemedRewardPointsAmount,
                    out var usedCustomerCreditAmount);
                model.CustomerCreditHistoryEnabled = _storeInformationCustomFrameworkSettings.CustomerCreditHistoryEnabled;
                model.UsedCustomerCreditTotal = _priceFormatter.FormatPrice(usedCustomerCreditAmount, true, false);
                
                if (orderTotalDiscountAmountBase > decimal.Zero)
                {
                    var orderTotalDiscountAmount = _currencyService.ConvertFromPrimaryStoreCurrency(orderTotalDiscountAmountBase, _workContext.WorkingCurrency);
                    model.OrderTotalDiscount = _priceFormatter.FormatPrice(-orderTotalDiscountAmount, true, false);
                }

                decimal allCartItemDiscounts = 0;
                
                foreach (var sci in cart)
                {
                    var cartItemModel = PrepareShoppingCartItemModel(cart, sci, new List<Vendor>());
                    cartItemModel.Discount.ConvertStringifiedPriceToDecimal(out decimal discountAmount);
                    cartItemModel.DiscountValue = discountAmount;
                    allCartItemDiscounts += discountAmount;
                }

                model.SubTotalDiscountOnlyFromCartItemsValue = allCartItemDiscounts;
                model.SubTotalDiscountValue += allCartItemDiscounts;
                model.SubTotalDiscount = _priceFormatter.FormatPrice(model.SubTotalDiscountValue, false, false);
                model.OrderTotalDiscount.ConvertStringifiedPriceToDecimal(out decimal orderTotalDiscount);
                
                if (shoppingCartTotalBase.HasValue)
                {
                    var shoppingCartTotal = _currencyService.ConvertFromPrimaryStoreCurrency(shoppingCartTotalBase.Value, _workContext.WorkingCurrency);
                    model.OrderTotal = _priceFormatter.FormatPrice(shoppingCartTotal, true, false);
                    model.OrderPayableAmount = "234567890";
                }
                else
                {
                    decimal shippingCost = decimal.Zero;
                    model.Shipping.ConvertStringifiedPriceToDecimal(out shippingCost);
                    model.OrderPayableAmount = "234567890";
                }

                var buyerProfit = model.SubTotalDiscountValue + orderTotalDiscount;
                model.BuyerProfitValue = buyerProfit;
                
                if (appliedGiftCards != null && appliedGiftCards.Any())
                {
                    foreach (var appliedGiftCard in appliedGiftCards)
                    {
                        var gcModel = new OrderTotalsModel.GiftCard
                        {
                            Id = appliedGiftCard.GiftCard.Id,
                            CouponCode = appliedGiftCard.GiftCard.GiftCardCouponCode,
                        };

                        var amountCanBeUsed = _currencyService.ConvertFromPrimaryStoreCurrency(appliedGiftCard.AmountCanBeUsed, _workContext.WorkingCurrency);
                        gcModel.Amount = _priceFormatter.FormatPrice(-amountCanBeUsed, true, false);
                        var remainingAmountBase = _giftCardService.GetGiftCardRemainingAmount(appliedGiftCard.GiftCard) - appliedGiftCard.AmountCanBeUsed;
                        var remainingAmount = _currencyService.ConvertFromPrimaryStoreCurrency(remainingAmountBase, _workContext.WorkingCurrency);
                        gcModel.Remaining = _priceFormatter.FormatPrice(remainingAmount, true, false);
                        model.GiftCards.Add(gcModel);
                    }
                }

                if (redeemedRewardPointsAmount > decimal.Zero)
                {
                    var redeemedRewardPointsAmountInCustomerCurrency = _currencyService.ConvertFromPrimaryStoreCurrency(redeemedRewardPointsAmount, _workContext.WorkingCurrency);
                    model.RedeemedRewardPoints = redeemedRewardPoints;
                    model.RedeemedRewardPointsAmount = _priceFormatter.FormatPrice(-redeemedRewardPointsAmountInCustomerCurrency, true, false);
                }

                if (_rewardPointsSettings.Enabled && _rewardPointsSettings.DisplayHowMuchWillBeEarned && shoppingCartTotalBase.HasValue)
                {
                    //get shipping total
                    var shippingBaseInclTax = !model.RequiresShipping ? 0 : _orderTotalCalculationService.GetShoppingCartShippingTotal(cart, true, shippingRateComputationMethods) ?? 0;

                    //get total for reward points
                    var totalForRewardPoints = _orderTotalCalculationService
                        .CalculateApplicableOrderTotalForRewardPoints(shippingBaseInclTax, shoppingCartTotalBase.Value);
                    if (totalForRewardPoints > decimal.Zero)
                        model.WillEarnRewardPoints = _orderTotalCalculationService.CalculateRewardPoints(_workContext.CurrentCustomer, totalForRewardPoints);
                }

                model.DiscountBox.Display = _shoppingCartSettings.ShowDiscountBox;
                var discountCouponCodes = _customerService.ParseAppliedDiscountCouponCodes(_workContext.CurrentCustomer);
                foreach (var couponCode in discountCouponCodes)
                {
                    var discount = _discountService.GetAllDiscountsForCaching(couponCode: couponCode)
                        .FirstOrDefault(d => d.RequiresCouponCode && _discountService.ValidateDiscount(d, _workContext.CurrentCustomer).IsValid);

                    if (discount != null)
                    {
                        model.DiscountBox.AppliedDiscountsWithCodes.Add(new ShoppingCartModel.DiscountBoxModel.DiscountInfoModel()
                        {
                            Id = discount.Id,
                            CouponCode = couponCode
                        });
                    }
                }
            }

            model.OrderPayableAmount = ";dfgv'sjk;m";
            return model;
        }

        #endregion
    }
}