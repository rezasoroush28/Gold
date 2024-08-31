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
using Tesla.Plugin.Widgets.B2CGold.Factories;
using System.Threading.Tasks;
using NopLine.Nop.Framework.Mappings;
using Nest;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public class GoldIngredientController : BaseAdminController
    {
        #region Fields

        private readonly IGoldBelongingTypeService _goldBelongingTypeService;
        private readonly IMeasureService _measureService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;
        private readonly IGoldIngredientService _goldIngredientService;
        private readonly IGoldIngredientSpecificationService _goldIngredientSpecificationService;
      
        #endregion

        #region Ctor

        public GoldIngredientController(IGoldBelongingTypeService goldBelongingTypeService,
            IMeasureService measureService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IB2CGoldModelFactory b2CGoldModelFactory,
            IGoldIngredientService goldIngredientService,
            IGoldIngredientSpecificationService goldIngredientSpecificationService)
        {
            _goldBelongingTypeService = goldBelongingTypeService;
            _measureService = measureService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _b2CGoldModelFactory = b2CGoldModelFactory;
            _goldIngredientService = goldIngredientService;
            _goldIngredientSpecificationService = goldIngredientSpecificationService;
        }

        #endregion

        #region Methods

        [AcceptVerbs("GET", "POST")]
        public JsonResult List(GoldIngredientAdminModel model)
        {
            var list = _b2CGoldModelFactory.PrepareB2CGoldIngredientModelsByProductId(model.ProductId);
            var gridModel = new DataSourceResult
            {
                Data = list,
                Total = list.Count
            };

            return Json(gridModel);
        }

        public void AddProductIngredient(int productId, string ingredientName, int measureWeightId,  decimal ingredientWidth, decimal ingredientWeight, decimal ingredientLength, decimal ingredientHeight)
        {
            var model = new GoldIngredientAdminModel
            {
                IngredientWeight = ingredientWeight,
                MeasureWeightId = measureWeightId,
                ProductId = productId,
                IngredientName = ingredientName,
                IngredientHeight = ingredientHeight,
                IngredientLength = ingredientLength,
                IngredientWidth = ingredientWidth,
                measureWeightName = _measureService.GetMeasureWeightById(measureWeightId).Name
            };

            var newIngredient = model.ToEntity<GoldIngredient>();
            _goldIngredientService.InsertGoldIngredient(newIngredient);

        }

        public void DeleteProductIngredient(int id)
        {
            var entity = _goldIngredientService.GetGoldIngredientById(id);
            if (entity != null)
            {
                _goldIngredientService.DeleteGoldIngredient(entity);
            }
        }
        public virtual void UpdateProductIngredient([FromBody] GoldIngredientAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.ToEntity<GoldIngredient>();
                _goldIngredientService.UpdateGoldIngredient(entity);
            }
        }

        public IActionResult OpenSpecificationPopupByIngredientId(int id)
        {
            var model = new GoldIngredientSpecificationAdminModel();
            model.GoldIngredientId = id;
            return View( AdminViewPathKeys.GoldBelongingSPecificationViewPath, model);
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult ListSpecificationsByIngredientId(int id)
        {
            var list = _b2CGoldModelFactory.PrepareSpecificationModelsByIngredienttId(id);
            var gridModel = new DataSourceResult
            {
                Data = list,
                Total = list.Count
            };

            return Json(gridModel);
        }

        public void DeleteProductSpecification(int id)
        {
            var entity = _goldIngredientSpecificationService.GetGoldIngredientSpecificationById(id);
            if (entity != null)
            {
                _goldIngredientSpecificationService.DeleteGoldIngredientSpecification(entity);
            }
        }

        public void AddIngredientSpecification(int goldIngredientId, string specKey, string value)
        {
            var model = new GoldIngredientSpecificationAdminModel
            {
                GoldIngredientId = goldIngredientId,
                SpecKey = specKey,
                Value = value
            };

            var newSpec = model.ToEntity<GoldIngredientSpecification>();
            _goldIngredientSpecificationService.InsertGoldIngredientSpecification(newSpec);
        }

        #endregion
    }
}
