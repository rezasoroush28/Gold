using System;
using System.Net.Http;
using HtmlAgilityPack;
using Nop.Services.Logging;
using Nop.Core.Caching;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Razor.Parser.SyntaxTree;
using Nop.Core.Data;
using Tesla.Plugin.Widgets.B2CGold.Data;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using Tesla.Plugin.Widgets.Gold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Services
{
    public class GoldPriceService : IGoldPriceService
    {
        #region Fields

        private readonly B2CGoldSettings _b2CGoldSettings;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger _logger;
        private readonly IRepository<GoldPriceScheduleTask> _priceScheduleTask;

        #endregion

        #region Ctor

        public GoldPriceService(ICacheManager cacheManager, ILogger logger, B2CGoldSettings b2CGoldSettings, IRepository<GoldPriceScheduleTask> scheduleTask)
        {
            _cacheManager = cacheManager;
            _logger = logger;
            _b2CGoldSettings = b2CGoldSettings;
            _priceScheduleTask = scheduleTask;
        }

        #endregion

        #region Methods

        private static readonly HttpClientHandler handler = new HttpClientHandler
        {
            AllowAutoRedirect = false // Prevent automatic redirection
        };

        public decimal GetGoldPrice()
        {
            if (_b2CGoldSettings.UseDefaultGoldCurrentPrice)
            {
                var goldPriceToman = _b2CGoldSettings.GoldCurrentPriceInput;
                return goldPriceToman;
            }
            else
            {
                try
                {
                    var price = _priceScheduleTask.TableNoTracking.FirstOrDefault().Price;
                    return price;
                }
                catch (Exception ex)
                {
                    _logger.Error($"Getting Gold Price from the source 'TGJU' encountered an exception of type {ex.Message}");
                    throw ex;
                }
                
            }
        }

        public DateTime GetLastUpdatePrice()
        {
            var priceSchedule = _priceScheduleTask.TableNoTracking.FirstOrDefault();
            if (priceSchedule != null)
            {
                var dateTime = priceSchedule.LastSuccessUtc;
                return dateTime.HasValue ? dateTime.Value : default;
            }
            else
            {
                return default; // or return DateTime.MinValue; or handle as needed
            }
        }

        public async Task<bool> IsSubdomainActive(string url)
        {
            HttpClient client = new HttpClient(handler);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode >= HttpStatusCode.MultipleChoices && response.StatusCode < HttpStatusCode.BadRequest)
                {
                    // Check if the location header points to the login page
                    if (response.Headers.Location != null && response.Headers.Location.AbsoluteUri.Contains("/login_up.php"))
                    {
                        return false; // Redirected to the login page, so the site is not active
                    }
                }

                // Checking for HTTP 200 OK status
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true; // The site is up and running
                }

                return false; // The status code is not 200
            }
            catch
            {
                // Return false if there is any exception, indicating the site is not up
                return false;
            }
        }

        #endregion

    }
}
