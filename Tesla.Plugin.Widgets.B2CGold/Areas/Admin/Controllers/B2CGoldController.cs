using Microsoft.AspNetCore.Mvc;

using Nop.Core;
using Nop.Core.Data;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using System.Threading.Tasks;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.B2CGold.Services;


namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Controllers
{
    public class B2CGoldController : BaseAdminController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IB2CGoldModelFactory _b2cGoldModelFactory;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IRepository<GoldPriceInfo> _productGoldInfoRepository;
        private readonly IRepository<ProductGroupBelonging> _productGroupBelongingRepository;
        private readonly IB2CGoldModelFactory _b2CGoldModelFactory;
        private readonly IGoldContactInfoService _contactInfoService;
        private readonly IRepository<ProductGoldBelongingMapping> _productGoldBelongingMappingRepository;
        
        #endregion

        #region Ctor

        public B2CGoldController(IPermissionService permissionService, IB2CGoldModelFactory b2cGoldModelFactory,
            IStoreContext storeContext, ISettingService settingService, ICustomerActivityService customerActivityService,
            IB2CGoldModelFactory b2CGoldModelFactory, IGoldContactInfoService contactInfoService,
            ILocalizationService localizationService = null, INotificationService notificationService = null,
            IRepository<GoldPriceInfo> productGoldInfoRepository = null,
            IRepository<ProductGoldBelongingMapping> productGoldBelongingMappingRepository = null)
        {
            _permissionService = permissionService;
            _b2cGoldModelFactory = b2cGoldModelFactory;
            _storeContext = storeContext;
            _settingService = settingService;
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _productGoldInfoRepository = productGoldInfoRepository;
            _productGoldBelongingMappingRepository = productGoldBelongingMappingRepository;
            _b2CGoldModelFactory = b2CGoldModelFactory;
            _contactInfoService = contactInfoService;
        }

        #endregion

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePluginInfo))
            {
                return AccessDeniedView();
            }

            var model = await _b2cGoldModelFactory.PrepareB2CGoldSettingsModel();
            return View(AdminViewPathKeys.GoldSettingViewPath, model);
        }

        [HttpPost]
        public IActionResult Configure(B2CGoldSettingsModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePluginInfo))
            {
                return AccessDeniedView();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var b2cGoldSettings = _settingService.LoadSetting<B2CGoldSettings>(storeScope);
            b2cGoldSettings = model.ToSettings(b2cGoldSettings);
            _settingService.SaveSetting(b2cGoldSettings);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.GoldPreOrderPercentage, model.GoldPrepurchasePercentage_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.SellerProfitPercentage, model.SellerProfitPercentage_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.GoldTaxRate, model.GoldTaxRate_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.GoldCurrentPriceInput, model.GoldCurrentPriceInput_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.Enabled, model.Enabled_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.GoldPriceUsedIsActive, model.GoldPriceUsedIsActive_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.IsActiveOnlineChat, model.IsActiveOnlineChat_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(b2cGoldSettings, x => x.OrderValidateTimeInSeconds, model.OrderValidateTimeInSeconds_OverrideForStore, storeScope, false);
            
            _settingService.ClearCache();

            _customerActivityService.InsertActivity("EditSettings", _localizationService.GetResource("ActivityLog.EditSettings"));
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.Updated"));

            return RedirectToAction(nameof(Configure));
        }

        #endregion
    }
}
