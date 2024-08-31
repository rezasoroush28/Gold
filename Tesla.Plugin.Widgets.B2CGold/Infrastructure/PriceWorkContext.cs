using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nop.Core;
using Nop.Data;
using Nop.Data.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Data;
using Tesla.Plugin.Widgets.Gold.Services;
using System.Threading.Tasks;

namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure
{
    /// <summary>
    /// Represents plugin object context
    /// </summary>
    public class PriceWorkContext : IPriceWorkContext
    {

        #region Field

        private decimal _cashedPrice;
        private DateTime _lastPriceUpadate;
        private static bool _isActiveSubDomain;
        private readonly IGoldPriceService _priceService;
        private readonly B2CGoldSettings _b2CGoldSettings;
        #endregion

        #region Ctor

        public PriceWorkContext(IGoldPriceService priceService, B2CGoldSettings b2CGoldSettings)
        {
            _priceService = priceService;
            _b2CGoldSettings = b2CGoldSettings;
        }

        #endregion

        #region Properties

        public decimal CurrentPrice
        {
            get
            {
                if (_cashedPrice == 0)
                {
                    _cashedPrice = _priceService.GetGoldPrice();
                }
                return _priceService.GetGoldPrice();
            }
            set
            {
                _cashedPrice = value;
            }
        }

        public DateTime LastUpdatePrice
        {
            get
            {
                if (_lastPriceUpadate == null)
                {
                    _lastPriceUpadate = _priceService.GetLastUpdatePrice();
                }
                return _priceService.GetLastUpdatePrice();
            }
            set
            {
                _lastPriceUpadate = value;
            }
        }

        public async Task<bool> IsSubdomainActive()
        {
            if (!_isActiveSubDomain)
            {
                _isActiveSubDomain = await _priceService.IsSubdomainActive(_b2CGoldSettings.ApiGoldPriceUrl);
            }
            return _isActiveSubDomain = await _priceService.IsSubdomainActive(_b2CGoldSettings.ApiGoldPriceUrl);
        }

        #endregion
    }
}
