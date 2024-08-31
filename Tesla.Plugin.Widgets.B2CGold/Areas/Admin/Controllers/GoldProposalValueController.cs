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
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Services;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using System.IO;
using Nop.Core.Infrastructure;
using System;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public partial class GoldProposalValueController : BaseAdminController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IGoldProposalValueModelFactory _goldProposalValueModelFactory;
        private readonly IGoldProposalValueService _goldProposalValueService;
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
        private readonly INopFileProvider _nopFileProvider;
        private readonly IGoldProposalValueViewModelFactory _goldProposalValueViewModelFactory;

        public static string UploadsPath => "~/wwwroot/Uploaded/svg/B2CGold";

        #endregion

        #region Ctor

        public GoldProposalValueController(IAclService aclService,
            IGoldProposalValueModelFactory GoldProposalValueModelFactory,
            IGoldProposalValueService GoldProposalValueService,
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
            IPaymentPluginManager paymentPluginManager,
            INopFileProvider nopFileProvider,
            IGoldProposalValueViewModelFactory goldProposalValueViewModelFactory)
        {
            _aclService = aclService;
            _goldProposalValueModelFactory = GoldProposalValueModelFactory;
            _goldProposalValueService = GoldProposalValueService;
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
            _nopFileProvider = nopFileProvider;
            _goldProposalValueViewModelFactory = goldProposalValueViewModelFactory;
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
            var model = _goldProposalValueModelFactory.PrepareGoldProposalValueSearchModel(new GoldProposalValueSearchModel());

            return View(AdminViewPathKeys.GoldProposalValueListViewPath, model);
        }

        [HttpPost]
        public virtual IActionResult List(GoldProposalValueSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedDataTablesJson();
            }

            //prepare model
            var model = _goldProposalValueModelFactory.PrepareGoldProposalValueListModel(searchModel);

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
            var model = _goldProposalValueModelFactory.PrepareGoldProposalValueModel(new GoldProposalValueModel(), null);

            return View(AdminViewPathKeys.GoldProposalValueCreateViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(GoldProposalValueModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            var goldProposalValueViewModel = _goldProposalValueViewModelFactory.PrepareGoldProposalValueViewModel();
            var valueTitleExists = goldProposalValueViewModel.GoldProposalValues.Any(goldProposalValueModel => goldProposalValueModel.ValueTitle == model.ValueTitle);
            if (valueTitleExists)
            {
                _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.Duplicate.Error"));
                ModelState.AddModelError("Duplicate Data", "There is another data with the same Title");
            }

            if (Request.Form.Files.Count == 0 || Path.GetExtension(Request.Form.Files[0].FileName).ToLowerInvariant() != ".svg")
            {
                _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.UploadedIcon.Required.Error"));
                ModelState.AddModelError("Icon File", "You did not upload any file or the file you uploaded is not a svg file");
            }

            if (ModelState.IsValid)
            {
                var directory = _nopFileProvider.MapPath(UploadsPath);
                _nopFileProvider.CreateDirectory(directory);

                var fileName = Path.GetFileName(Request.Form.Files[0].FileName);
                var uniqueFileName = Path.Combine($"{Guid.NewGuid()}_{fileName}");
                var filePath = Path.Combine(directory, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(fileStream);
                }

                var filePathName = _nopFileProvider.Combine("/Uploaded/svg/B2CGold", uniqueFileName);
                filePathName = $"/{filePathName.Replace('\\', '/')}";
                model.IconPath = filePathName;
                var goldProposalValue = model.ToEntity<GoldProposalValue>();
                var insertResult = _goldProposalValueService.InsertGoldProposalValue(goldProposalValue);
                if (!insertResult.Success)
                {
                    if (insertResult.Error.Contains("duplicate"))
                    {
                        _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.ValueTitle.DuplicateError"));
                    }

                    return View(AdminViewPathKeys.GoldProposalValueCreateViewPath, model);
                }

                //activity log
                _customerActivityService.InsertActivity("AddNewGoldProposalValue",
                    string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.AddNewGoldProposalValue"), goldProposalValue.Id), goldProposalValue);

                _notificationService.SuccessNotification(_localizationService.GetResource("Plugin.Widgets.B2CGold.GoldProposalValue.Added"));

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(List));
                }

                return RedirectToAction(nameof(Edit), new { id = goldProposalValue.Id });
            }

            //prepare model
            model = _goldProposalValueModelFactory.PrepareGoldProposalValueModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(AdminViewPathKeys.GoldProposalValueCreateViewPath, model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a GoldProposalValue with the specified id
            var goldProposalValue = _goldProposalValueService.GetGoldProposalValueById(id);
            if (goldProposalValue == null)
            {
                return RedirectToAction(nameof(Edit));
            }

            //prepare model
            var model = _goldProposalValueModelFactory.PrepareGoldProposalValueModel(null, goldProposalValue);

            return View(AdminViewPathKeys.GoldProposalValueEditViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(GoldProposalValueModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a category with the specified id
            var goldProposalValue = _goldProposalValueService.GetGoldProposalValueById(model.Id);
            if (goldProposalValue == null)
            {
                return RedirectToAction(nameof(List));
            }

            var goldProposalValueViewModel = _goldProposalValueViewModelFactory.PrepareGoldProposalValueViewModel();
            var valueTitleExists = goldProposalValueViewModel.GoldProposalValues.Any(goldProposalValueModel =>
            goldProposalValueModel.ValueTitle == model.ValueTitle && goldProposalValueModel.Id != model.Id);
            if (valueTitleExists)
            {
                _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.Duplicate.Error"));
                ModelState.AddModelError("Duplicate Data", "There is another data with the same Title");
            }

            if (Request.Form.Files.Count != 0)
            {
                if (Path.GetExtension(Request.Form.Files[0].FileName).ToLowerInvariant() != ".svg")
                {
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.UploadedIcon.Required.Error"));

                    ModelState.AddModelError("Icon File", "You can upload only svg files");
                }
            }

            if (ModelState.IsValid)
            {
                var directory = _nopFileProvider.MapPath(UploadsPath);
                _nopFileProvider.CreateDirectory(directory);

                var fileName = Path.GetFileName(Request.Form.Files[0].FileName);
                var uniqueFileName = Path.Combine($"{Guid.NewGuid()}_{fileName}");
                var filePath = Path.Combine(directory, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(fileStream);
                }

                var filePathName = _nopFileProvider.Combine("/Uploaded/svg/B2CGold", uniqueFileName);
                filePathName = $"/{filePathName.Replace('\\', '/')}";
                model.IconPath = filePathName;
                goldProposalValue = model.ToEntity(goldProposalValue);
                var updateResult = _goldProposalValueService.UpdateGoldProposalValue(goldProposalValue);
                if (!updateResult.Success)
                {
                    if (updateResult.Error.Contains("duplicate"))
                    {
                        _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.ValueTitle.DuplicateError"));
                    }

                    return View(AdminViewPathKeys.GoldProposalValueEditViewPath, model);
                }

                //activity log
                _customerActivityService.InsertActivity("EditGoldProposalValue",
                    string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.EditGoldProposalValue"), goldProposalValue.Id), goldProposalValue);

                _notificationService.SuccessNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.Updated"));

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(List));
                }

                return RedirectToAction(nameof(Edit), new { id = goldProposalValue.Id });
            }

            //prepare model
            model = _goldProposalValueModelFactory.PrepareGoldProposalValueModel(model, goldProposalValue, true);

            //if we got this far, something failed, redisplay form
            return View(AdminViewPathKeys.GoldProposalValueEditViewPath, model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
            {
                return AccessDeniedView();
            }

            //try to get a GoldProposalValue with the specified id
            var goldProposalValue = _goldProposalValueService.GetGoldProposalValueById(id);
            if (goldProposalValue == null)
            {
                return RedirectToAction(nameof(List));
            }

            _goldProposalValueService.DeleteGoldProposalValue(goldProposalValue);

            //activity log
            _customerActivityService.InsertActivity("DeleteGoldProposalValue",
                string.Format(_localizationService.GetResource("Plugins.Widgets.B2CGold.ActivityLog.DeleteGoldProposalValue"), goldProposalValue.Id), goldProposalValue);

            _notificationService.SuccessNotification(_localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.Deleted"));

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Utilities

        private string GenerateUniqueFileName(string originalFileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            var extension = Path.GetExtension(originalFileName);
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            return $"{fileNameWithoutExtension}_{timestamp}{extension}";
        }

        #endregion
    }
}