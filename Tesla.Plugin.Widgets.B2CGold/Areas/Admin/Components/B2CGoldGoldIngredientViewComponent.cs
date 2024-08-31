using Microsoft.AspNetCore.Mvc;

using Nop.Web.Areas.Admin.Models.Catalog;

using NopLine.Nop.Framework.Components;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Services.Directory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Components
{
    [ViewComponent(Name = "B2CGoldGoldIngredient")]
    public class B2CGoldGoldIngredientViewComponent : BaseNopLineComponent
    {
        #region Fields 

        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;
        private readonly IGoldIngredientService _goldIngredientService;
        private readonly IGoldIngredientSpecificationService _goldIngredientSpecificationService;
        private readonly IMeasureService _measureService;

        #endregion

        #region Ctor

        public B2CGoldGoldIngredientViewComponent(/*IB2CGoldModelFactory b2CGoldModelFactory, */IGoldIngredientService goldIngredientService, IGoldIngredientSpecificationService goldIngredientSpecificationService, IMeasureService measureService)
        {
            //_b2CGoldModelFactory = b2CGoldModelFactory;
            _goldIngredientService = goldIngredientService;
            _goldIngredientSpecificationService = goldIngredientSpecificationService;
            _measureService = measureService;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(object additionalData)
        {
            var productModel = additionalData as ProductModel;
            var  model = new GoldIngredientAdminModel();
            model.ProductId = productModel.Id;
            var availableMeasurments = _measureService.GetAllMeasureWeights();
            foreach (var item in availableMeasurments)
            {
                model.AvailableMeasurments.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = false
                });
            }
            return View(AdminViewPathKeys.B2CGoldGoldIngredientViewPath , model);
        } 
        
        #endregion
    }
}