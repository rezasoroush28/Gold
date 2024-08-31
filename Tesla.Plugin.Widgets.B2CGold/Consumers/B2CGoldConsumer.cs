using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Orders;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Models;
using System;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.Gold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.Consumers
{
    public class GoldInfoEventConsumer : IConsumer<EntityUpdatedEvent<Order>>, IConsumer<ModelReceivedEvent<BaseNopModel>>
    {
        #region Field

        private readonly IDbContext _dbContext;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IRepository<GoldPriceInfo> _productGoldInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGoldPriceInfoService _goldInfoService;

        #endregion

        #region Ctor

        public GoldInfoEventConsumer(IOrderService orderService, IWorkContext workContext,
            IRepository<GoldPriceInfo> productGoldInfoRepository, IHttpContextAccessor httpContextAccessor, IDbContext dbContext, IGoldPriceInfoService goldInfoService)
        {
            this._productGoldInfoRepository = productGoldInfoRepository;
            this._orderService = orderService;
            this._workContext = workContext;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _goldInfoService = goldInfoService;
        }

        #endregion

        #region Methods

        public void HandleEvent(ModelReceivedEvent<BaseNopModel> eventMessage)
        {
            BaseNopModel model = eventMessage.Model;
            GoldPriceInfo productGoldInfo = null;


            if (model is ProductModel productModel)
            {
                productGoldInfo = _productGoldInfoRepository.Table.SingleOrDefault(a => a.ProductId == productModel.Id);
                if (productGoldInfo != null)
                {
                    productGoldInfo.ManufacturerCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.ManufacturerCommissionPercentage)]);
                    productGoldInfo.VendorCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.VendorCommissionPercentage)]);
                    productGoldInfo.BonakdarCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.BonakdarCommissionPercentage)]);

                    _goldInfoService.UpdateGoldPriceInfo(productGoldInfo);
                }

                else
                {
                    productGoldInfo = new GoldPriceInfo()
                    {
                        ProductId = productModel.Id,
                        ManufacturerCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.ManufacturerCommissionPercentage)]),
                        VendorCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.VendorCommissionPercentage)]),
                        BonakdarCommissionPercentage = Convert.ToDecimal(_httpContextAccessor.HttpContext.Request.Form[nameof(GoldPriceInfo.BonakdarCommissionPercentage)])
                    };

                    _goldInfoService.InsertGoldPriceInfo(productGoldInfo);
                }
            }

        }
        public void HandleEvent(EntityUpdatedEvent<Order> eventMessage)
        {

        } 

        #endregion
    }





}
