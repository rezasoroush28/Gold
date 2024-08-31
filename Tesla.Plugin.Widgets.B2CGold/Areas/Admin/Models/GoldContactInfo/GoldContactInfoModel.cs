using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo
{
    public class GoldContactInfoModel : BaseNopEntityModel
    {
        #region Ctor

        public GoldContactInfoModel()
        {
            AvailableGoldContactInfoTypes = new List<SelectListItem>();
            AvailableDays = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.Value")]
        public string Value { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.GoldContactInfoType")]
        public int GoldContactInfoTypeId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.GoldContactInfoType")]
        public string GoldContactInfoTypeName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.GoldContactInfoType")]
        public GoldContactInfoType GoldContactInfoType
        {
            get
            {
                return (GoldContactInfoType)GoldContactInfoTypeId;
            }
            set
            {
                GoldContactInfoTypeId = (int)value;
            }
        }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.AllDaysOfWeek")]
        public bool AllDaysOfWeek { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.StartDayofWeekName")]
        public string StartDayofWeekName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.StartDayofWeekName")]
        public int StartDayofWeek { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.EndDayofWeekName")]
        public string EndDayofWeekName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.EndDayofWeekName")]
        public int EndDayofWeek { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.FromHour")]
        public int FromHour { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.ToHour")]
        public int ToHour { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.B2CGold.GoldContactInfo.Enabled")]
        public bool Enabled { get; set; }

        public List<SelectListItem> AvailableGoldContactInfoTypes { get; set; }

        public List<SelectListItem> AvailableDays { get; set; }

        #endregion
    }
}