using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Configuration;
using Tesla.Plugin.Widgets.B2CGold.Components;

namespace Tesla.Plugin.Widgets.B2CGold
{
    /// <summary>
    /// Plugin
    /// </summary>
    public class B2CGoldPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly B2CGoldContext _b2CGoldContext;
        private readonly GoldPriceGrabberContext _goldPriceGrabberContext;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public B2CGoldPlugin(IWebHelper webHelper,
            B2CGoldContext b2CGoldContext,
            IPermissionService permissionService,
            ILocalizationService localizationService,
            ISettingService settingService,
            GoldPriceGrabberContext goldPriceGrabberContext)
        {
            _webHelper = webHelper;
            _b2CGoldContext = b2CGoldContext;
            _permissionService = permissionService;
            _localizationService = localizationService;
            _settingService = settingService;
            _goldPriceGrabberContext = goldPriceGrabberContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
               AdminWidgetZones.ProductDetailsBlock,
               PublicWidgetZones.OrderDetailsPageBottom,
               PublicWidgetZones.OrderPayTimeCountDown
            };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/B2CGold/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone == PublicWidgetZones.OrderDetailsPageBottom)
            {
                return nameof(B2CGoldOrderDetailsViewComponent);
            }

            else if (widgetZone == AdminWidgetZones.ProductDetailsBlock)
            {
                return "B2CGoldGoldProductDetailsPanels";
            }

            else if(widgetZone ==  PublicWidgetZones.OrderPayTimeCountDown)
            {

            }

            return null;

        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            _b2CGoldContext.Install();
            _goldPriceGrabberContext.Install();
            var b2CGoldSettings = new B2CGoldSettings
            {
                ApiUrl = "https://www.tgju.org/profile/geram18",
                PriceElement = "//*[@data-col='info.last_trade.PDrCotVal']",
                PriceGrabberConnectionString = "Data Source=.;Initial Catalog=GoldPriceDb;Integrated Security=False;Encrypt=false;TrustServerCertificate=True;MultipleActiveResultSets=True;User ID=sa;Password=B2CGold",
            };

            _settingService.SaveSetting(b2CGoldSettings);

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _b2CGoldContext.Uninstall();
            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
            {
                return;
            }

            var goldB2CRootNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.Root"),
                Visible = true,
                SystemName = "Plugin.Widgets.B2CGold.Root",
                IconClass = "fa fa-dot-circle-o",
            };

            const string adminUrlPart = "Admin/";

            var goldBelongingTypeNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.GoldBelongingType"),
                Visible = true,
                SystemName = "Widgets.B2CGold.GoldBelongingType",
                IconClass = "fa fa-dot-circle-o",
                Url = $"{_webHelper.GetStoreLocation()}{adminUrlPart}GoldBelongingType/ListBelongingTypes",
            };

            var b2cGoldSettingNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.B2CGoldSetting"),
                Visible = true,
                SystemName = "Widgets.B2CGold.B2CGoldSetting",
                IconClass = "fa fa-cog",
                Url = $"{_webHelper.GetStoreLocation()}{adminUrlPart}/B2CGold/Configure",
            };

            var goldBelongingNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.GoldBelonging"),
                Visible = true,
                SystemName = "Widgets.B2CGold.GoldBelonging",
                IconClass = "fa fa-dot-circle-o",
                Url = $"{_webHelper.GetStoreLocation()}{adminUrlPart}GoldBelonging/ListBelongings",
            };

            var goldProposalValueNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue"),
                Visible = true,
                SystemName = "Widgets.B2CGold.GoldProposalValue",
                IconClass = "fa fa-dot-circle-o",
                Url = $"{_webHelper.GetStoreLocation()}{adminUrlPart}GoldProposalValue/List",
            };

            var goldContactInfoNode = new SiteMapNode
            {
                Title = _localizationService.GetResource("Plugins.Widgets.B2CGold.GoldContactInfo"),
                Visible = true,
                SystemName = "Widgets.B2CGold.GoldContactInfo",
                IconClass = "fa fa-dot-circle-o",
                Url = $"{_webHelper.GetStoreLocation()}{adminUrlPart}GoldContactInfo/List",
            };

            if (!goldB2CRootNode.ChildNodes.Contains(b2cGoldSettingNode))
            {
                goldB2CRootNode.ChildNodes.Add(b2cGoldSettingNode);
            }

            if (!rootNode.ChildNodes.Contains(goldB2CRootNode))
            {
                rootNode.ChildNodes.Add(goldB2CRootNode);
            }

            if (!goldB2CRootNode.ChildNodes.Contains(goldBelongingNode))
            {
                goldB2CRootNode.ChildNodes.Add(goldBelongingNode);
            }

            if (!goldB2CRootNode.ChildNodes.Contains(goldBelongingTypeNode))
            {
                goldB2CRootNode.ChildNodes.Add(goldBelongingTypeNode);
            }

            if (!goldB2CRootNode.ChildNodes.Contains(goldProposalValueNode))
            {
                goldB2CRootNode.ChildNodes.Add(goldProposalValueNode);
            }

            if (!goldB2CRootNode.ChildNodes.Contains(goldContactInfoNode))
            {
                goldB2CRootNode.ChildNodes.Add(goldContactInfoNode);
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;

        #endregion
    }
}