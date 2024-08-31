using AutoMapper;

using Nop.Core.Configuration;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure.Mapper;
using Nop.Web.Areas.Admin.Models.Settings;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;

using static Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.B2CGoldSettingsModel;
using static Tesla.Plugin.Widgets.B2CGold.B2CGoldSettings;

namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public AutoMapperProfile()
        {
            CreateMap<CatalogCustomFrameworkSettings, CatalogCustomFrameworkSettingsModel>().ReverseMap();
            CreateMap<B2CGoldSettings, B2CGoldSettingsModel>().ReverseMap();
            CreateMap<ProductGoldBelongingMapping, GoldBelongingMappingModel>().ReverseMap();
            CreateMap<GoldIngredient, GoldIngredientModel>().ReverseMap();
            CreateMap<GoldIngredientSpecification, ProductGoldIngredientSpecificationModel>().ReverseMap();
            CreateMap<GoldProductBelongingCalculation, GoldBelongingPayingModel>().ReverseMap();
        }

        #endregion

        public int Order => 1000;
    }
}