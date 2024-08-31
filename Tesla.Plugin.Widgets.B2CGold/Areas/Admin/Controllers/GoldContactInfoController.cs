using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Core.Domain.Payments;
using Nop.Services.Payments;
using Nop.Web.Areas.Admin.Controllers;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Services;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public partial class GoldContactInfoController : BaseAdminController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IGoldContactInfoModelFactory _goldContactInfoModelFactory;
        private readonly IGoldContactInfoService _goldContactInfoService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly IExportManager _exportManager;
        private readonly IImportManager _importManager;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly PaymentSettings _paymentSettings;
        private readonly IPaymentPluginManager _paymentPluginManager;

        #endregion

        #region Ctor

        public GoldContactInfoController(IAclService aclService,
            IGoldContactInfoModelFactory GoldContactInfoModelFactory,
            IGoldContactInfoService GoldContactInfoService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            IDiscountService discountService,
            IExportManager exportManager,
            IImportManager importManager,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IProductService productService,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            PaymentSettings paymentSettings,
            IPaymentPluginManager paymentPluginManager)
        {
            _aclService = aclService;
            _goldContactInfoModelFactory = GoldContactInfoModelFactory;
            _goldContactInfoService = GoldContactInfoService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _discountService = discountService;
            _exportManager = exportManager;
            _importManager = importManager;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _productService = productService;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _paymentSettings = paymentSettings;
            _paymentPluginManager = paymentPluginManager;
        }

        #endregion

        #region List

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //prepare model
            var model = _goldContactInfoModelFactory.PrepareGoldContactInfoSearchModel(new GoldContactInfoSearchModel());

            return View(AdminViewPathKeys.GoldContactInfoListViewPath, model);
        }

        [HttpPost]
        public virtual IActionResult List(GoldContactInfoSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedDataTablesJson();
            }
                
            //prepare model
            var model = _goldContactInfoModelFactory.PrepareGoldContactInfoListModel(searchModel);

            return Json(model);
        }

        #endregion

        #region Create / Edit / Delete

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }
                
            //prepare model
            var model = _goldContactInfoModelFactory.PrepareGoldContactInfoModel(new GoldContactInfoModel(), null);

            return View(AdminViewPathKeys.GoldContactInfoCreateViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(GoldContactInfoModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var goldContactInfo = model.ToEntity<GoldContactInfo>();
                _goldContactInfoService.InsertGoldContactInfo(goldContactInfo);

                //activity log
                _customerActivityService.InsertActivity("AddNewGoldContactInfo",
                    string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.AddNewGoldContactInfo"), goldContactInfo.Value), goldContactInfo);

                _notificationService.SuccessNotification(_localizationService.GetResource("Plugin.Widgets.B2CGold.GoldContactInfo.Added"));

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(List));
                }

                return RedirectToAction(nameof(Edit), new { id = goldContactInfo.Id });
            }

            //prepare model
            model = _goldContactInfoModelFactory.PrepareGoldContactInfoModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(AdminViewPathKeys.GoldContactInfoCreateViewPath, model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a GoldContactInfo with the specified id
            var goldContactInfo = _goldContactInfoService.GetGoldContactInfoById(id);
            if (goldContactInfo == null)
            {
                return RedirectToAction(nameof(Edit));
            }

            //prepare model
            var model = _goldContactInfoModelFactory.PrepareGoldContactInfoModel(null, goldContactInfo);

            return View(AdminViewPathKeys.GoldContactInfoEditViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(GoldContactInfoModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a category with the specified id
            var goldContactInfo = _goldContactInfoService.GetGoldContactInfoById(model.Id);
            if (goldContactInfo == null)
            {
                return RedirectToAction(nameof(List));
            }
                
            if (ModelState.IsValid)
            {
                goldContactInfo = model.ToEntity(goldContactInfo);
                _goldContactInfoService.UpdateGoldContactInfo(goldContactInfo);

                //activity log
                _customerActivityService.InsertActivity("EditGoldContactInfo",
                    string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.EditGoldContactInfo"), goldContactInfo.Id), goldContactInfo);

                _notificationService.SuccessNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldContactInfo.Updated"));

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(List));
                }

                return RedirectToAction(nameof(Edit), new { id = goldContactInfo.Id });
            }

            //prepare model
            model = _goldContactInfoModelFactory.PrepareGoldContactInfoModel(model, goldContactInfo, true);

            //if we got this far, something failed, redisplay form
            return View(AdminViewPathKeys.GoldContactInfoEditViewPath, model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a GoldContactInfo with the specified id
            var goldContactInfo = _goldContactInfoService.GetGoldContactInfoById(id);
            if (goldContactInfo == null)
            {
                return RedirectToAction(nameof(List));
            }

            _goldContactInfoService.DeleteGoldContactInfo(goldContactInfo);

            //activity log
            _customerActivityService.InsertActivity("DeleteGoldContactInfo",
                string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.DeleteGoldContactInfo"), goldContactInfo.Id), goldContactInfo);

            _notificationService.SuccessNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldContactInfo.Deleted"));

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}