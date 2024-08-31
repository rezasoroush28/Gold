using Microsoft.AspNetCore.Mvc;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Controllers;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.Gold.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Services.Directory;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public class GoldBelongingController : BaseAdminController
    {
        #region Fields

        private readonly IGoldBelongingTypeService _goldBelongingTypeService;
        private readonly IProductGoldBelongingMappingService _productGoldBelongingMappingService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IMeasureService _measureService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;

        #endregion

        #region Ctor

        public GoldBelongingController(IGoldBelongingTypeService goldBelongingTypeService,
            IGoldBelongingService goldBelongingService,
            IMeasureService measureService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IProductGoldBelongingMappingService productGoldBelongingMappingService,
            IB2CGoldModelFactory b2CGoldModelFactory,
            IGoldProductBelongingCalculationService goldBelongingPayingService)
        {
            _goldBelongingTypeService = goldBelongingTypeService;
            _goldBelongingService = goldBelongingService;
            _measureService = measureService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _productGoldBelongingMappingService = productGoldBelongingMappingService;
            _b2CGoldModelFactory = b2CGoldModelFactory;
            _goldBelongingPayingService = goldBelongingPayingService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult Create()
        {
            var model = new GoldBelongingAdminModel();
            var availableMeasurments = _measureService.GetAllMeasureWeights();
            var availableGoldBelongingTypes = _goldBelongingTypeService.GetAllGoldBelongingTypes();

            foreach (var type in availableGoldBelongingTypes)
            {
                model.AvailableTypes.Add(new SelectListItem
                {
                    Text = type.Name,
                    Value = type.Id.ToString(),
                    Selected = false
                });
            }

            foreach (var item in availableMeasurments)
            {
                model.AvailableMeasurments.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = false
                });
            }
            return View(AdminViewPathKeys.B2CGoldBelongingCreateViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Create(GoldBelongingAdminModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var goldBelonging = model.ToEntity<GoldBelonging>();

                if (_goldBelongingService.CheckName(goldBelonging))
                {
                    _goldBelongingService.InsertGoldBelonging(goldBelonging);
                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
                }
                else
                {
                    _notificationService.WarningNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Name.Error"));
                    return RedirectToAction(nameof(Create));
                }

                if (continueEditing)
                {
                    return RedirectToAction(nameof(Edit), new { id = goldBelonging.Id });
                }
            }

            return RedirectToAction(nameof(ListBelongings));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var goldBelonging = _goldBelongingService.GetGoldBelongingById(id);
            var model = goldBelonging.ToModel<GoldBelongingAdminModel>();
            var availableMeasurments = _measureService.GetAllMeasureWeights();
            var availableGoldBelongingTypes = _goldBelongingTypeService.GetAllGoldBelongingTypes();

            foreach (var type in availableGoldBelongingTypes)
            {
                model.AvailableTypes.Add(new SelectListItem
                {
                    Text = type.Name,
                    Value = type.Id.ToString(),
                    Selected = false
                });
            }

            foreach (var item in availableMeasurments)
            {
                model.AvailableMeasurments.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = false
                });
            }
            return View(AdminViewPathKeys.B2CGoldBelongingEditViewPath, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(GoldBelongingAdminModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var goldBelonging = model.ToEntity<GoldBelonging>();

                if (_goldBelongingService.CheckName(goldBelonging))
                {
                    _goldBelongingService.UpdateGoldBelonging(goldBelonging);
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

            return RedirectToAction(nameof(ListBelongings));
        }

        [HttpPost]
        public JsonResult List()
        {
            var list = _goldBelongingService.GetAllGoldBelongings();
            var gridModel = new DataSourceResult
            {
                Data = list,
                Total = list.Count
            };

            return Json(gridModel);
        }

        [HttpGet]
        public ActionResult ListBelongings()
        {
            return View(AdminViewPathKeys.B2CGoldBelongingListViewPath);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var glodBelonging = _goldBelongingService.GetGoldBelongingById(id);
            if (glodBelonging == null)
            {
                return RedirectToAction(nameof(ListBelongings));
            }

            _goldBelongingService.DeleteGoldBelonging(glodBelonging);
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
            return RedirectToAction(nameof(ListBelongings));
        }

        [HttpPost]
        public ActionResult Delete(GoldBelongingAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var glodBelonging = _goldBelongingService.GetGoldBelongingById(model.Id);
                if (glodBelonging == null)
                {
                    return RedirectToAction(nameof(ListBelongings));
                }

                _goldBelongingService.DeleteGoldBelonging(glodBelonging);
                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelonging.Updated"));
            }

            return RedirectToAction(nameof(ListBelongings));
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult ListProductBelongings(int id)
        {
            if (id != 0)
            {
                var listModel = _b2CGoldModelFactory.PrepareB2CGoldBelongingMappingModelsByProductId(id);
                var gridModel = new DataSourceResult
                {
                    Data = listModel,
                    Total = listModel.Count
                };
                return Json(gridModel);
            }
            return null;
        }

        public virtual void AddProductGoldBelongingMapping(int productId, int goldBelongingId, int belongingCount, decimal belongingWeight)
        {
            var model = new ProductGoldBelongingMappingModel
            {
                ProductId = productId,
                GoldBelongingId = goldBelongingId,
                BelongingCount = belongingCount,
                BelongingWeight = belongingWeight
            };

            var newMapping = model.ToEntity<ProductGoldBelongingMapping>();
            _productGoldBelongingMappingService.InsertProductGoldBelongingMapping(newMapping);
        }

        public virtual void DeleteProductGoldBelongingMapping(int id)
        {
            if (ModelState.IsValid)
            {
                var glodBelongingMapping = _productGoldBelongingMappingService.GetProductGoldBelongingMappingById(id);
                if (glodBelongingMapping != null)
                {
                    _productGoldBelongingMappingService.DeleteProductGoldBelongingMapping(glodBelongingMapping);
                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.B2CGoldBelongingMapping.Updated"));
                }
            }
        }

        public virtual void UpdateProductBelonging([FromBody] ProductGoldBelongingMappingModel model)
        {
            if (ModelState.IsValid)
            {
                var mapping = model.ToEntity<ProductGoldBelongingMapping>();
                _productGoldBelongingMappingService.UpdateProductGoldBelongingMapping(mapping);
            }
        }

        public virtual void AddProductGcoldBelongingPaying(int productId,int goldBelongingCalculationTypeId, decimal value)
        {
            
            var entity = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(productId);
            var isThere = entity != null;

            if (entity == null)
            {
                entity = new GoldProductBelongingCalculation();
            }

            entity.Value = value;
            entity.ProductId = productId;
            entity.GoldBelongingCalculationTypeId = goldBelongingCalculationTypeId;
            
            if (isThere)
            {
                _goldBelongingPayingService.UpdateGoldProductBelongingCalculation(entity);
                return;
            }

            _goldBelongingPayingService.InsertGoldProductBelongingCalculation(entity);

        }

        [HttpGet]
        [Route("GoldBelonging/GetThePayingMethodByProductId/{id}")]
        public JsonResult GetThePayingMethodByProductId(int id)
        {
            if (id != 0)
            {
                var paying = _goldBelongingPayingService.GetGoldProductBelongingCalculationByProductId(id);
                var model = paying.ToModel<GoldBelongingPayingAdminModel>();
                
                return Json(model);
            }

            return null;
        }

        #endregion
    }
}
