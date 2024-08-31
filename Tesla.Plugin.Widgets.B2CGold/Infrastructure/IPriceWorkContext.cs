using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

using System;
using System.Threading.Tasks;

namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure
{
    public interface IPriceWorkContext
    {
        decimal CurrentPrice { get; set; }
        DateTime LastUpdatePrice { get; set; }
        Task<bool> IsSubdomainActive();
    }
}