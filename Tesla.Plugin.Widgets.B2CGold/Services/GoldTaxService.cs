using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Tax;
using Nop.Services.Logging;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Services.Discounts;
using Nop.Core.Domain.Orders;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    /// <summary>
    /// Tax service
    /// </summary>
    public partial class GoldTaxService : TaxService
    {
        #region Fields

        private readonly IB2CGoldModelFactory _b2cGoldModelFactory;
        private readonly IGoldPriceInfoService _goldPriceInfoService;
        private readonly IGoldPriceService _goldPriceService;
        private readonly IGoldPriceCalculationService _goldPriceCalculationService;
        private readonly IPriceWorkContext _priceWorkContext;

        #endregion

        #region Ctor

        public GoldTaxService(AddressSettings addressSettings, CustomerSettings customerSettings, IAddressService addressService,
            ICountryService countryService, IGenericAttributeService genericAttributeService, IGeoLookupService geoLookupService,
            ILogger logger, IStateProvinceService stateProvinceService, IStaticCacheManager cacheManager, IStoreContext storeContext,
            ITaxPluginManager taxPluginManager, IWebHelper webHelper, IWorkContext workContext, ShippingSettings shippingSettings,
            TaxSettings taxSettings, IB2CGoldModelFactory b2cGoldModelFactory, IGoldPriceInfoService goldPriceInfoService,
            IGoldPriceService goldPriceService, IGoldPriceCalculationService goldPriceCalculationService, IPriceWorkContext priceWorkContext = null) : base(addressSettings, customerSettings, addressService, countryService, genericAttributeService,
                geoLookupService, logger, stateProvinceService, cacheManager, storeContext, taxPluginManager, webHelper, workContext,
                shippingSettings, taxSettings)
        {
            _b2cGoldModelFactory = b2cGoldModelFactory;
            _goldPriceInfoService = goldPriceInfoService;
            _goldPriceService = goldPriceService;
            _goldPriceCalculationService = goldPriceCalculationService;
            _priceWorkContext = priceWorkContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets price
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="taxCategoryId">Tax category identifier</param>
        /// <param name="price">Price</param>
        /// <param name="includingTax">A value indicating whether calculated price should include tax</param>
        /// <param name="customer">Customer</param>
        /// <param name="priceIncludesTax">A value indicating whether price already includes tax</param>
        /// <param name="taxRate">Tax rate</param>
        /// <returns>Price</returns>
        public override decimal GetProductPrice(Product product, int taxCategoryId,
            decimal price, bool includingTax, Customer customer,
            bool priceIncludesTax, out decimal taxRate)
        {
            //no need to calculate tax rate if passed "price" is 0
            if (price == decimal.Zero)
            {
                taxRate = decimal.Zero;
                return taxRate;
            }

            var goldRealTimePrice = _priceWorkContext.CurrentPrice;
            var goldWeight = _goldPriceCalculationService.GetGoldWeight(product, null);
            var goldVendorCommissionPercentage = _goldPriceInfoService.GetGoldPriceInfoByProductId(product.Id)?.VendorCommissionPercentage ?? 1 / 10; ;
            var goldManufacturerCommissionPercentage = _goldPriceInfoService.GetGoldPriceInfoByProductId(product.Id)?.ManufacturerCommissionPercentage ?? 1 / 10;
            var goldBelongingPrice = _goldPriceCalculationService.GetProductBelongingPrice(goldWeight, product.Id, goldRealTimePrice);

            //Gold realtime price--
            //Gold Weight**
            //Ojrat--
            //Sood foroshande--
            //Maliat
            //belonging

            //0. Get product's wieght (Service)
            //1. Get gold's realtime price
            //1.1. Get Gold Setting for tax (inject)
            //2. Fetch Gold info record (Cache)
            //3. Fetch Belonging record (Cache)

            GetTaxRate(product, taxCategoryId, customer, price, out taxRate, out var isTaxable);

            if (priceIncludesTax)
            {
                //"price" already includes tax
                if (includingTax)
                {
                    //we should calculate price WITH tax
                    if (!isTaxable)
                    {
                        //but our request is not taxable
                        //hence we should calculate price WITHOUT tax
                        price = CalculatePrice(price, taxRate, false);
                    }
                }
                else
                {
                    //we should calculate price WITHOUT tax
                    price = CalculatePrice(price, taxRate, false);
                }
            }
            else
            {
                //"price" doesn't include tax
                if (includingTax)
                {
                    //we should calculate price WITH tax
                    //do it only when price is taxable
                    if (isTaxable)
                    {
                        price = CalculatePrice(price, taxRate, true);
                    }
                }
            }

            if (!isTaxable)
            {
                //we return 0% tax rate in case a request is not taxable
                taxRate = decimal.Zero;
            }

            //allowed to support negative price adjustments
            //if (price < decimal.Zero)
            //    price = decimal.Zero;

            return price;
        }


        /// <summary>
        /// Calculated price
        /// </summary>
        /// <param name="price">Price</param>
        /// <param name="percent">Percent</param>
        /// <param name="increase">Increase</param>
        /// <returns>New price</returns>
        protected override decimal CalculatePrice(decimal price, decimal percent, bool increase)
        {
            if (percent == decimal.Zero)
                return price;

            decimal result;
            if (increase)
            {
                result = price * (1 + percent / 100);
            }
            else
            {
                result = price - price / (100 + percent) * percent;
            }

            return result;
        }

        #endregion
    }

}
