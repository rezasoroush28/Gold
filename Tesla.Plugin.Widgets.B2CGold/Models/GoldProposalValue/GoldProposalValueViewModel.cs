using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;


namespace Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue
{
    public class GoldProposalValueViewModel
    {
        public GoldProposalValueViewModel()
        {
            GoldProposalValues = new List<GoldProposalValueModel>();
        }

        public List<GoldProposalValueModel> GoldProposalValues { get; set; }
    }
}
