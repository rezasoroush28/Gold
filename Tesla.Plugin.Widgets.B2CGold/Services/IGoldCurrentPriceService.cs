using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;

using System.Collections.Generic;
using System.Linq;

using Tesla.Plugin.Widgets.B2CGold;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldCurrentPriceService 
    {
        IList<GoldCurrentPrice> GetAllCurrentPrices();
    }
}
