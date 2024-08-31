using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;

namespace Tesla.Plugin.Widgets.B2CGold.Models.GoldContactInfo
{
    public class GoldContactInfoViewModel
    {
        public GoldContactInfoViewModel()
        {
            GoldContactInfos = new List<GoldContactInfoModel>();
        }

        public List<GoldContactInfoModel> GoldContactInfos { get; set; }
    }
}
