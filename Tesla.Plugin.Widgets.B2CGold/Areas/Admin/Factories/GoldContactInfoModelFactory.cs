using System;
using System.Collections.Generic;
using System.Linq;

using DNTPersianUtils.Core;

using Microsoft.AspNetCore.Mvc.Rendering;

using Nop.Services;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldContactInfo model factory implementation
    /// </summary>
    public partial class GoldContactInfoModelFactory : IGoldContactInfoModelFactory
    {
        #region Fields

        private readonly IGoldContactInfoService _goldContactInfoService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public GoldContactInfoModelFactory(IGoldContactInfoService GoldContactInfoService,
            ILocalizationService localizationService)
        {
            _goldContactInfoService = GoldContactInfoService;
            _localizationService = localizationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare GoldContactInfo model
        /// </summary>
        /// <param name="model">GoldContactInfo model</param>
        /// <param name="goldContactInfo">GoldContactInfo</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldContactInfo model</returns>
        public virtual GoldContactInfoModel PrepareGoldContactInfoModel(GoldContactInfoModel model, GoldContactInfo goldContactInfo, bool excludeProperties = false)
        {
            //Manage 4 state for model preparation

            model = model ?? goldContactInfo?.ToModel<GoldContactInfoModel>() ?? new GoldContactInfoModel();

            PrepareGoldContactInfoTypes(model.AvailableGoldContactInfoTypes);
            PrepareGoldContactInfoDays(model.AvailableDays);
            model.StartDayofWeek = 6;
            model.EndDayofWeek = 6;

            return model;
        }

        /// <summary>
        /// Prepare GoldContactInfo search model
        /// </summary>
        /// <param name="searchModel">GoldContactInfo search model</param>
        /// <returns>GoldContactInfo search model</returns>
        public virtual GoldContactInfoSearchModel PrepareGoldContactInfoSearchModel(GoldContactInfoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged GoldContactInfo list model
        /// </summary>
        /// <param name="searchModel">GoldContactInfo search model</param>
        /// <returns>GoldContactInfo list model</returns>
        public virtual GoldContactInfoListModel PrepareGoldContactInfoListModel(GoldContactInfoSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get categories
            var goldContactInfos = _goldContactInfoService.GetAllGoldContactInfo(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new GoldContactInfoListModel().PrepareToGrid(searchModel, goldContactInfos, () =>
            {
                return goldContactInfos.Select(goldContactInfo =>
                {
                    //fill in model values from the entity
                    var goldContactInfoModel = goldContactInfo.ToModel<GoldContactInfoModel>();

                    //Convert GoldContactInfoType => Resource to show on grid
                    goldContactInfoModel.GoldContactInfoTypeName = _localizationService.GetLocalizedEnum(goldContactInfo.GoldContactInfoType);
                    goldContactInfoModel.StartDayofWeekName = goldContactInfo.StartDayofWeekEnum.GetPersianWeekDayName();
                    goldContactInfoModel.EndDayofWeekName = goldContactInfo.EndDayofWeekEnum.GetPersianWeekDayName();

                    return goldContactInfoModel;
                });
            });

            return model;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare available product types
        /// </summary>
        /// <param name="items">Product type items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual void PrepareGoldContactInfoTypes(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
                
            //prepare available product types
            var availableGoldContactTypeInfoItems = GoldContactInfoType.Telegram.ToSelectList(false);
            foreach (var productTypeItem in availableGoldContactTypeInfoItems)
            {
                items.Add(productTypeItem);
            }
        }

        public virtual void PrepareGoldContactInfoDays(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
                
            //prepare available product types
            foreach (var day in GetShamsiWeekDays())
            {
                string persianDayName = day.GetPersianWeekDayName();
                items.Add(new SelectListItem
                {
                    Text = persianDayName,
                    Value = ((int)day).ToString()
                });
            }
        }

        public static List<DayOfWeek> GetShamsiWeekDays()
        {
            // Define the start of the Shamsi week (Saturday)
            var shamsiStartDay = DayOfWeek.Saturday;

            // Get all days of the week starting from Sunday (default order in DayOfWeek)
            var defaultDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

            // Find the index of the Shamsi start day in the default order
            int startIndex = defaultDaysOfWeek.IndexOf(shamsiStartDay);

            // Create the list for Shamsi ordered days
            var shamsiWeekDays = new List<DayOfWeek>();

            // Add days starting from the Shamsi start day
            for (int i = 0; i < defaultDaysOfWeek.Count; i++)
            {
                int index = (startIndex + i) % defaultDaysOfWeek.Count;
                shamsiWeekDays.Add(defaultDaysOfWeek[index]);
            }

            return shamsiWeekDays;
        }

        #endregion
    }
}