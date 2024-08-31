using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldPriceService
    {
        decimal GetGoldPrice();
        DateTime GetLastUpdatePrice();
        Task<bool> IsSubdomainActive(string url);
    }
}