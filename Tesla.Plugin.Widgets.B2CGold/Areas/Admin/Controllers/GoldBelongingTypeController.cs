using Microsoft.AspNetCore.Mvc;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Controllers;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Services.Directory;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public class GoldBelongingTypeController : BaseAdminController
    {
        #region Fields

        private readonly IGoldBelongingTypeService _goldBelongingTypeService;
        private readonly IMeasureService _measureService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public GoldBelongingTypeController(IGoldBelongingTypeService goldBelongingTypeService,
            IMeasureService measureService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IPermissionService permissionService)
        {
            _goldBelongingTypeService = goldBelongingTypeService;
            _measureService = measureService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _permissionService = permissionService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult Create()
        {
            var model = new GoldBelongingTypeAdminModel();
            return View(AdminViewPathKeys.B2CGoldBelongingTypeCreatViewPath, model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Create(GoldBelongingTypeAdminModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var goldBelongingType = model.ToEntity<GoldBelongingType>();

                if (_goldBelongingTypeService.CheckName(goldBelongingType))
                {
                    _goldBelongingTypeService.InsertBelongingType(goldBelongingType);
                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
                }
                else
                {
                    _notificationService.WarningNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Name.Error"));
                    return RedirectToAction(nameof(Create));
                }

                if (continueEditing)
                {
                    return RedirectToAction(nameof(Edit), new { id = goldBelongingType.Id });
                }
            }

            return RedirectToAction(nameof(ListBelongingTypes));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var model = new GoldBelongingAdminModel();
            var goldBelongingType = _goldBelongingTypeService.GetBelongingTypeById(id);
            var model = goldBelongingType.ToModel<GoldBelongingTypeAdminModel>();
            
            return View(AdminViewPathKeys.B2CGoldBelongingTypeEditViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(GoldBelongingTypeAdminModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var goldBelongingType = model.ToEntity<GoldBelongingType>();

                if (_goldBelongingTypeService.CheckName(goldBelongingType))
                {
                    _goldBelongingTypeService.UpdateBelongingType(goldBelongingType);
                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
                }
                else
                {
                    _notificationService.WarningNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Name.Error"));
                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }


                if (continueEditing)
                {
                    return RedirectToAction(nameof(Edit), new { id = model.Id });
                }
            }

            return RedirectToAction(nameof(ListBelongingTypes));
        }

        [HttpPost]
        public JsonResult List()
        {
            var list = _goldBelongingTypeService.GetAllGoldBelongingTypes();
            var gridModel = new DataSourceResult
            {
                Data = list,
                Total = list.Count
            };

            return Json(gridModel);
        }


       

        [HttpGet]
        public ActionResult ListBelongingTypes()
        {
            return View(AdminViewPathKeys.B2CGoldBelongingTypeListViewPath);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            var glodBelongingType = _goldBelongingTypeService.GetBelongingTypeById(id);

            if (glodBelongingType == null)
            {
                return RedirectToAction(nameof(ListBelongingTypes));
            }

            _goldBelongingTypeService.DeleteBelongingType(glodBelongingType);
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
            return RedirectToAction(nameof(ListBelongingTypes));
        }

        [HttpPost]
        public ActionResult Delete(GoldBelongingTypeAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var glodBelongingType = _goldBelongingTypeService.GetBelongingTypeById(model.Id);


                if (glodBelongingType == null)
                {
                    return RedirectToAction(nameof(ListBelongingTypes));
                }

                _goldBelongingTypeService.DeleteBelongingType(glodBelongingType);
                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
            }

            return RedirectToAction(nameof(ListBelongingTypes));
        }

        #endregion
    }
}
