using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;

using System;
using System.Collections.Generic;
using System.Linq;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Helper
{
    public class Helper
    {
        public static decimal RoundToThousend(decimal number)
        {
            return (Math.Round(number / 1000) * 1000);
        }
    }
}
