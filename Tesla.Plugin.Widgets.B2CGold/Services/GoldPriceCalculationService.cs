using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Discounts;

using System;
using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;
using Nop.Services.Shipping;
using Nop.Core.Infrastructure;
using System.Globalization;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Services
{
    public partial class GoldPriceCalculationService : PriceCalculationService, IGoldPriceCalculationService
    {
        #region Fields

        private readonly IGoldPriceCalculatorFactory _goldPriceCalculatorFactory;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly IGoldPriceService _goldPriceService;
        private readonly B2CGoldSettings _b2CGoldSettings;
        private readonly IGoldPriceInfoService _goldPriceInfoService;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly IPriceWorkContext _priceWorkContext;

        #endregion

        #region Ctor

        public GoldPriceCalculationService(CatalogSettings catalogSettings, CurrencySettings currencySettings,
            ICategoryService categoryService, ICurrencyService currencyService, IDiscountService discountService,
            IManufacturerService manufacturerService, IProductAttributeParser productAttributeParser,
            IProductService productService, IStaticCacheManager cacheManager,
            IStoreContext storeContext, IWorkContext workContext, ShoppingCartSettings shoppingCartSettings,
            IGoldPriceService goldPriceService, B2CGoldSettings b2CGoldSettings,
            IGoldPriceInfoService goldPriceInfoService, IGoldProductBelongingCalculationService goldProductBelongingCalculationServic,
            IGoldPriceCalculatorFactory goldPriceCalculatorFactory,
            IGoldBelongingService goldBelongingService, IPriceWorkContext priceWorkContext = null) :
            base(catalogSettings, currencySettings, categoryService, currencyService, discountService, manufacturerService,
                productAttributeParser, productService, cacheManager, storeContext, workContext, shoppingCartSettings)
        {
            _cacheManager = cacheManager;
            _storeContext = storeContext;
            _catalogSettings = catalogSettings;
            _productAttributeParser = productAttributeParser;
            _shoppingCartSettings = shoppingCartSettings;
            _goldPriceService = goldPriceService;
            _b2CGoldSettings = b2CGoldSettings;
            _goldPriceInfoService = goldPriceInfoService;
            _goldBelongingPayingService = goldProductBelongingCalculationServic;
            _goldBelongingService = goldBelongingService;
            _goldPriceCalculatorFactory = goldPriceCalculatorFactory;
            _priceWorkContext = priceWorkContext;
        }

        #endregion

        #region Methods

        public decimal GetProductBelongingPrice(decimal totalWeight, int productId, decimal weightToPriceTransformer)
        {
            var productManufactureCommission = (decimal)0;
            var belongingPayingMethod = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(productId);
            var belongingPrice = (decimal)0;

            switch (belongingPayingMethod.GoldBelongingCalculationType)
            {
                case GoldBelongingCalculationType.SolidPrice:
                    {
                        belongingPrice = belongingPayingMethod.Value;
                        break;
                    }
                case GoldBelongingCalculationType.BaseProductGoldPrice:
                    {
                        belongingPrice = totalWeight * weightToPriceTransformer * belongingPayingMethod.Value / 100;
                        break;
                    }
                case GoldBelongingCalculationType.GoldFinalPrice:
                    {
                        belongingPrice = (productManufactureCommission + totalWeight * weightToPriceTransformer) * belongingPayingMethod.Value / 100;
                        break;
                    }
            }

            return belongingPrice;
        }

        public decimal GetGoldWeight(Product product, IList<ProductAttributeValue> attributeValues)
        {
            return EngineContext.Current.Resolve<IShippingService>().GetShoppingCartItemWeight(product, attributeValues);
        }

        public override decimal GetUnitPrice(Product product,
         Customer customer,
         ShoppingCartType shoppingCartType,
         int quantity,
         string attributesXml,
         decimal customerEnteredPrice,
         DateTime? rentalStartDate, DateTime? rentalEndDate,
         bool includeDiscounts,
         out decimal discountAmount,
         out List<DiscountForCaching> appliedDiscounts, out ProductAttributeCombination productAttributeCombination)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            //1. Get product's wieght (Service)
            //2. Get gold's realtime price
            //3. Get Gold Setting for tax (inject)
            //4. Fetch Gold info record (Cache)
            //5. Fetch Belonging record (Cache)
            //6. Do Calculations

            discountAmount = decimal.Zero;
            appliedDiscounts = new List<DiscountForCaching>();
            decimal finalPrice;

            productAttributeCombination = _productAttributeParser.FindProductAttributeCombination(product, attributesXml);

            //summarize price of all attributes
            var attributesTotalPrice = decimal.Zero;

            var attributeValues = _productAttributeParser.ParseProductAttributeValues(attributesXml);
            //1. Get product's wieght (Service)
            var productWeight = GetGoldWeight(product, attributeValues);
            //2. Get gold's realtime price
            var goldRealTimePrice = _priceWorkContext.CurrentPrice;
            //3. Get Gold Setting for tax (inject)
            //4. Fetch Gold info record (Cache) (Ojrat and Sood)
            var goldInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(product.Id);
            //5. Fetch Belonging record (Cache)
            var belongingPayingMethod = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);
            var calculations = GetGoldCalulatedInfoModel(productWeight, goldInfo, belongingPayingMethod, goldRealTimePrice, product.AvailableForPreOrder);

            if (attributeValues != null)
            {
                foreach (var attributeValue in attributeValues)
                {
                    attributesTotalPrice += GetProductAttributeValuePriceAdjustment(attributeValue, customer, product.CustomerEntersPrice ? (decimal?)customerEnteredPrice : null);
                }
            }

            //get price of a product (with previously calculated price of all attributes)
            if (product.AvailableForPreOrder)
            {
                finalPrice = calculations.PreOrderPrice;
            }
            else
            {
                finalPrice = calculations.TotalPrice;
            }

            return finalPrice;
        }

        public GoldCalulatedInfoModel GetGoldCalulatedInfoModel(decimal goldProductWeight, GoldPriceInfo goldPriceInfo,
            GoldProductBelongingCalculation goldProductBelongingCalculation, decimal goldRealTimePrice, bool availablePreOrder = false)
        {
            var theCalculator = _goldPriceCalculatorFactory.GetGoldPriceCalculator();
            var priceTable = theCalculator.GetPriceTable(goldProductWeight, goldPriceInfo, goldProductBelongingCalculation, goldRealTimePrice);
            return priceTable;
        }

        /// <summary>
        /// Gets the final price
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="customer">The customer</param>
        /// <param name="overriddenProductPrice">Overridden product price. If specified, then it'll be used instead of a product price. For example, used with product attribute combinations</param>
        /// <param name="additionalCharge">Additional charge</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for final price computation</param>
        /// <param name="quantity">Shopping cart item quantity</param>
        /// <param name="rentalStartDate">Rental period start date (for rental products)</param>
        /// <param name="rentalEndDate">Rental period end date (for rental products)</param>
        /// <param name="discountAmount">Applied discount amount</param>
        /// <param name="appliedDiscounts">Applied discounts</param>
        /// <returns>Final price</returns>
        public override decimal GetFinalPrice(Product product,
            Customer customer,
            decimal? overriddenProductPrice,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            DateTime? rentalStartDate,
            DateTime? rentalEndDate,
            out decimal discountAmount,
            out List<DiscountForCaching> appliedDiscounts)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            discountAmount = decimal.Zero;
            appliedDiscounts = new List<DiscountForCaching>();

            var cacheKey = string.Format(NopCatalogDefaults.ProductPriceModelCacheKey,
                product.Id,
                overriddenProductPrice?.ToString(CultureInfo.InvariantCulture),
                additionalCharge.ToString(CultureInfo.InvariantCulture),
                includeDiscounts,
                quantity,
                string.Join(",", customer.GetCustomerRoleIds()),
                _storeContext.CurrentStore.Id);

            var cacheTime = _catalogSettings.CacheProductPrices ? 60 : 0;
            //we do not cache price for rental products
            //otherwise, it can cause memory leaks (to store all possible date period combinations)

            var cachedPrice = _cacheManager.Get(cacheKey, () =>
            {
                var result = new ProductPriceForCaching();
                //1. Get product's wieght (Service)
                var productWeight = GetGoldWeight(product, new List<ProductAttributeValue>());
                //2. Get gold's realtime price
                var goldRealTimePrice = _priceWorkContext.CurrentPrice;
                //3. Get Gold Setting for tax (inject)
                var taxPercentage = _b2CGoldSettings.GoldTaxRate;
                //4. Fetch Gold info record (Cache) (Ojrat and Sood)
                var goldInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(product.Id);
                //5. Fetch Belonging record (Cache)
                var belongingPayingMethod = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);

                var goldCalulatedInfoModel = GetGoldCalulatedInfoModel(productWeight, goldInfo, belongingPayingMethod, goldRealTimePrice);

                var price = goldCalulatedInfoModel.TotalPrice;
                //additional charge
                price += additionalCharge;
                result.Price = price;

                return result;
            }, cacheTime);

            if (!includeDiscounts)
                return cachedPrice.Price;

            if (!cachedPrice.AppliedDiscounts.Any())
                return cachedPrice.Price;

            appliedDiscounts.AddRange(cachedPrice.AppliedDiscounts);
            discountAmount = cachedPrice.AppliedDiscountAmount;

            return cachedPrice.Price;
        }

        /// <summary>
        /// Gets the final price
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="customer">The customer</param>
        /// <param name="overriddenProductPrice">Overridden product price. If specified, then it'll be used instead of a product price. For example, used with product attribute combinations</param>
        /// <param name="additionalCharge">Additional charge</param>
        /// <param name="includeDiscounts">A value indicating whether include discounts or not for final price computation</param>
        /// <param name="quantity">Shopping cart item quantity</param>
        /// <param name="rentalStartDate">Rental period start date (for rental products)</param>
        /// <param name="rentalEndDate">Rental period end date (for rental products)</param>
        /// <param name="discountAmount">Applied discount amount</param>
        /// <param name="appliedDiscounts">Applied discounts</param>
        /// <returns>Final price</returns>
        public decimal GetFinalPrice(Product product,
            Customer customer,
            string xmlAttribute,
            decimal additionalCharge,
            bool includeDiscounts,
            int quantity,
            DateTime? rentalStartDate,
            DateTime? rentalEndDate,
            out decimal discountAmount,
            out List<DiscountForCaching> appliedDiscounts)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            discountAmount = decimal.Zero;
            appliedDiscounts = new List<DiscountForCaching>();

            //1. Get product's wieght (Service)
            var productWeight = GetGoldWeight(product, new List<ProductAttributeValue>());
            //2. Get gold's realtime price
            var goldRealTimePrice = _priceWorkContext.CurrentPrice;
            //3. Get Gold Setting for tax (inject)
            var taxPercentage = _b2CGoldSettings.GoldTaxRate;
            //4. Fetch Gold info record (Cache) (Ojrat and Sood)
            var goldInfo = _goldPriceInfoService.GetGoldPriceInfoByProductId(product.Id);
            //5. Fetch Belonging record (Cache)
            var belongingPayingMethod = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(product.Id);

            var goldCalulatedInfoModel = GetGoldCalulatedInfoModel(productWeight, goldInfo, belongingPayingMethod, goldRealTimePrice);

            var price = goldCalulatedInfoModel.TotalPrice;
            //additional charge
            price += additionalCharge;

            return price;
        }

        #endregion
    }
}