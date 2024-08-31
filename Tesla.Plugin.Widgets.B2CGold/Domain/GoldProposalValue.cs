using Nop.Core;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldProposalValue : BaseEntity
    {
        public string ValueTitle { get; set; }

        public string ValueDescription { get; set; }

        public string IconPath { get; set; }
    }
}
