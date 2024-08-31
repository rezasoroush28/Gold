using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Configuration;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.Gold.Services;
using Nop.Services.Localization;
using System.Linq;
using Nop.Services.Directory;
using Nop.Services.Catalog;
using Nop.Services.Shipping;
using Nop.Services.Logging;
using Nop.Core.Caching;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using DNTPersianUtils.Core;
using System.Threading.Tasks;
using System;
using Nop.Services.Helpers;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public class B2CGoldModelFactory : IB2CGoldModelFactory
    {
        #region Fields

        private readonly IGoldIngredientService _goldIngredientService;
        private readonly IGoldIngredientSpecificationService _goldIngredientSpecificationService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IGoldBelongingService _goldBelongingService;
        private readonly IRepository<ProductGroupBelonging> _productGroupBelongingRepository;
        private readonly IRepository<ProductGoldBelongingMapping> _productGoldBelongingMappingRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<MeasureWeight> _measureWeightRepository;
        private readonly IRepository<GoldIngredient> _goldIngredientRepository;
        private readonly IRepository<GoldIngredientSpecification> _goldIngredientSpecificationRepository;
        private readonly IGoldProductBelongingCalculationService _goldBelongingPayingService;
        private readonly IProductGoldBelongingMappingService _productGoldBelongingMappingService;
        private readonly IGoldBelongingTypeService _goldBelongingTypeService;
        private readonly IMeasureService _measureService;
        private readonly IShippingService _shippingService;
        private readonly IProductService _productService;
        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IPriceWorkContext _priceWorkContext;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region Ctor

        public B2CGoldModelFactory(IStoreContext storeContext,
            ISettingService settingService,
            IRepository<ProductGroupBelonging> productGroupBelongingRepository,
            IRepository<ProductGoldBelongingMapping> productGroupBelongingMappingRepository,
            IRepository<MeasureWeight> measureWeightRepository,
            ILocalizationService localizationService,
            IGoldBelongingService goldBelongingService,
            IGoldIngredientService goldIngredientService,
            IRepository<GoldIngredient> goldIngredientRepository,
            IRepository<GoldIngredientSpecification> goldIngredientSpecificationRepository,
            IProductGoldBelongingMappingService productGoldBelongingMappingService,
            IGoldBelongingTypeService goldBelongingTypeService,
            IMeasureService measureService,
            IGoldIngredientSpecificationService goldIngredientSpecificationService,
            IGoldProductBelongingCalculationService goldBelongingPayingService,
            IProductService productService,
            ILogger logger,
            ICacheManager cacheManager,
            IProductAttributeParser productAttributeParser,
            IPriceWorkContext priceWorkContext,
            IDateTimeHelper dateTimeHelper)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _productGroupBelongingRepository = productGroupBelongingRepository;
            _productGoldBelongingMappingRepository = productGroupBelongingMappingRepository;
            _measureWeightRepository = measureWeightRepository;
            _localizationService = localizationService;
            _goldBelongingService = goldBelongingService;
            _goldIngredientService = goldIngredientService;
            _goldIngredientRepository = goldIngredientRepository;
            _goldIngredientSpecificationRepository = goldIngredientSpecificationRepository;
            _productGoldBelongingMappingService = productGoldBelongingMappingService;
            _goldBelongingTypeService = goldBelongingTypeService;
            _measureService = measureService;
            _goldIngredientSpecificationService = goldIngredientSpecificationService;
            _goldBelongingPayingService = goldBelongingPayingService;
            _productService = productService;
            _logger = logger;
            _cacheManager = cacheManager;
            _productAttributeParser = productAttributeParser;
            _priceWorkContext = priceWorkContext;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Methods

        public virtual async Task<B2CGoldSettingsModel> PrepareB2CGoldSettingsModel()
        {
            var storeId = _storeContext.ActiveStoreScopeConfiguration;
            var b2cGoldSettings = _settingService.LoadSetting<B2CGoldSettings>(storeId);
            var mainModel = b2cGoldSettings.ToSettingsModel<B2CGoldSettingsModel>();
            mainModel.ActiveStoreScopeConfiguration = storeId;
            var types = ReflectionHelper.FindImplementations<IGoldPriceCalculator>();
            var typeNames = types.Select(x => x.Name).ToList();
            mainModel.AvailableCalculationMethods = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            
            foreach (var name in typeNames)
            {
                mainModel.AvailableCalculationMethods.Add(new Microsoft.AspNetCore.Mvc.Rendering.
                    SelectListItem { Text = _localizationService.GetResource("Admin.Configure.B2CGold."+name.ToString()), Value = name, Selected = (name == mainModel.PriceCalculationMethodName) } );
            }

            mainModel.GoldCurrentPriceOnline = _priceWorkContext.CurrentPrice;
            mainModel.SellerProfitPercentage_OverrideForStore = _settingService.SettingExists(b2cGoldSettings, x => x.SellerProfitPercentage, storeId);
            mainModel.GoldPrepurchasePercentage_OverrideForStore = _settingService.SettingExists(b2cGoldSettings, x => x.GoldPreOrderPercentage, storeId);
            mainModel.ApiPriceGoldActive = await _priceWorkContext.IsSubdomainActive();
            mainModel.LastUpdatePriceGold = PersianDateTimeUtils.ToPersianDateTimeString(_dateTimeHelper.ConvertToUserTime(_priceWorkContext.LastUpdatePrice, DateTimeKind.Utc).AddHours(-1), "yyyy/MM/dd HH:mm:ss");
            mainModel.ApiGoldPriceUrl = b2cGoldSettings.ApiGoldPriceUrl;
            mainModel.GoldPriceUsedIsActive_OverrideForStore = _settingService.SettingExists(b2cGoldSettings, x => x.GoldPriceUsedIsActive, storeId);
            mainModel.IsActiveOnlineChat_OverrideForStore = _settingService.SettingExists(b2cGoldSettings, x => x.IsActiveOnlineChat, storeId);
            return mainModel;
        }

        public List<ProductGoldBelongingMappingModel> PrepareB2CGoldBelongingMappingModelsByProductId(int productId)
        {
            var mappings = _productGoldBelongingMappingRepository.Table.Where(x => x.ProductId == productId).ToList();
            var mappingModels = new List<ProductGoldBelongingMappingModel>();
            foreach (var item in mappings)
            {
                var entity = _goldBelongingService.GetGoldBelongingById(item.GoldBelongingId);
                var model = item.ToModel<ProductGoldBelongingMappingModel>();
                model.GoldBelongingName = entity.Name;
                mappingModels.Add(model);
            }

            return mappingModels;
            //return null;
        }

        public List<GoldIngredientAdminModel> PrepareB2CGoldIngredientModelsByProductId(int id)
        {
            var ingredients = _goldIngredientRepository.TableNoTracking.Where(c => c.ProductId == id).ToList();
            var ingredientModels = new List<GoldIngredientAdminModel>();

            foreach (var item in ingredients)
            {
                var model = item.ToModel<GoldIngredientAdminModel>();
                model.measureWeightName = _measureService.GetMeasureWeightById(item.MeasureWeightId).Name;
                ingredientModels.Add(model);
            }
            return ingredientModels;
        }

        public List<GoldIngredientSpecificationAdminModel> PrepareSpecificationModelsByIngredienttId(int id)
        {
            var entities = _goldIngredientSpecificationRepository.Table.Where(c => c.GoldIngredientId == id).ToList();
            var models = new List<GoldIngredientSpecificationAdminModel>();

            foreach (var entity in entities)
            {
                models.Add(entity.ToModel<GoldIngredientSpecificationAdminModel>());
            }

            return models;
        }

        public ProductGoldBelongingsModel PrepareB2CProductGoldBelongingModelsByProductId(int productId)
        {
            var motherModel = new ProductGoldBelongingsModel();
            var mappings = _productGoldBelongingMappingRepository.Table.Where(x => x.ProductId == productId).ToList();
            var mappingModels = new List<GoldBelongingMappingModel>();

            foreach (var item in mappings)
            {
                var entityBelonging = _goldBelongingService.GetGoldBelongingById(item.GoldBelongingId);
                var belongingType = _goldBelongingTypeService.GetBelongingTypeById(entityBelonging.TypeId);
                var measureWeight = _measureService.GetMeasureWeightById(entityBelonging.MeasureWeightId);
                var model = item.ToModel<GoldBelongingMappingModel>();
                model.BelongingName = entityBelonging.Name;
                model.BelongingCount = item.Count;
                model.BelongingWeight = item.Weight;
                model.BelongingType = belongingType.Name;
                model.BelongingMeasureWeight = measureWeight.Name;
                mappingModels.Add(model);
            }
            
            motherModel.ProductGoldBelongingsList = mappingModels;
            return motherModel;
        }

        public List<GoldIngredientModel> PrepareB2CGoldIngredientViewModelsByProductId(int productId)
        {
            var ingredients = _goldIngredientService.GetGoldIngredientByProductId(productId);
            var ingredientModels = new List<GoldIngredientModel>();

            foreach (var item in ingredients)
            {
                var ingredientSpecificationModels = new List<ProductGoldIngredientSpecificationModel>();
                var model = item.ToModel<GoldIngredientModel>();
                var specifications = _goldIngredientSpecificationService.GetIngredientSpecificationsByIngredientId(item.Id);
                var measureWeightName = _measureService.GetMeasureWeightById(item.MeasureWeightId).Name;

                foreach (var spec in specifications)
                {
                    ingredientSpecificationModels.Add(spec.ToModel<ProductGoldIngredientSpecificationModel>());
                }

                model.IngredientName = item.IngredientName;
                model.IngredientWidth = item.IngredientWidth;
                model.IngredientHeight = item.IngredientHeight;
                model.IngredientLength = item.IngredientLength;
                model.IngredientWeight = item.IngredientWeight;
                model.IngredientMeasureWeightName = measureWeightName;
                model.SpecificationList = ingredientSpecificationModels;
                ingredientModels.Add(model);
            }

            return ingredientModels;
        }


        #endregion

        #region Utilities

        private List<GoldIngredientSpecificationAdminModel> PrepareSpecificationModels(List<GoldIngredientSpecification> specifications)
        {
            var models = new List<GoldIngredientSpecificationAdminModel>();

            foreach (var specification in specifications)
            {
                models.Add(specification.ToModel<GoldIngredientSpecificationAdminModel>());
            }
            return models;
        }

        private string GetGoldCalculationTypeName(GoldBelongingCalculationType type)
        {
            switch (type)
            {
                case GoldBelongingCalculationType.GoldFinalPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.GoldFinalPriceGoldFinalPrice");
                case GoldBelongingCalculationType.SolidPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.SolidPrice");
                case GoldBelongingCalculationType.BaseProductGoldPrice:
                    return _localizationService.GetResource("Admin.B2CGold.BelongingPaying.BaseProductGoldPrice");
            }
            return type.ToString();
        }
        #endregion
    }

}