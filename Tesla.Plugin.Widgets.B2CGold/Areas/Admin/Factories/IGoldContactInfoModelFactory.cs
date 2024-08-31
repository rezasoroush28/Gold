using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the GoldContactInfo model factory
    /// </summary>
    public partial interface IGoldContactInfoModelFactory
    {
        /// <summary>
        /// Prepare GoldContactInfo search model
        /// </summary>
        /// <param name="searchModel">GoldContactInfo search model</param>
        /// <returns>GoldContactInfo search model</returns>
        GoldContactInfoSearchModel PrepareGoldContactInfoSearchModel(GoldContactInfoSearchModel searchModel);

        /// <summary>
        /// Prepare paged GoldContactInfo list model
        /// </summary>
        /// <param name="searchModel">GoldContactInfo search model</param>
        /// <returns>GoldContactInfo list model</returns>
        GoldContactInfoListModel PrepareGoldContactInfoListModel(GoldContactInfoSearchModel searchModel);

        /// <summary>
        /// Prepare GoldContactInfo model
        /// </summary>
        /// <param name="model">GoldContactInfo model</param>
        /// <param name="GoldContactInfo">GoldContactInfo</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>GoldContactInfo model</returns>
        GoldContactInfoModel PrepareGoldContactInfoModel(GoldContactInfoModel model, GoldContactInfo GoldContactInfo, bool excludeProperties = false);
        void PrepareGoldContactInfoTypes(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null);
    }
}