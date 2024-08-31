using Microsoft.AspNetCore.Http;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue
{
    public class GoldProposalValueModel : BaseNopEntityModel
    {
        [Required]
        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldProposalValue.ValueTitle")]
        public string ValueTitle { get; set; }

        [Required]
        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldProposalValue.ValueDescription")]
        public string ValueDescription { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldProposalValue.IconPath")]
        public string IconPath { get; set; }

        public IFormFile UploadedIcon { get; set; }
    }
}