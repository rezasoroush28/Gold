using Nop.Core.Configuration;

namespace Tesla.Plugin.Widgets.B2CGold
{
    public class B2CGoldPayingListSettings : ISettings
    {
        public string ApiUrl { get; set; }
        public string PriceElement { get; set; }
    }
}
