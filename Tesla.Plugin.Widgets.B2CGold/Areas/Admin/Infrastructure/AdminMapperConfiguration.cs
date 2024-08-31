using AutoMapper;

using Nop.Core.Infrastructure.Mapper;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldContactInfo;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldPriceInfo;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue;

using static Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.B2CGoldSettingsModel;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Infrastructure
{
    public class AdminMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public AdminMapperConfiguration()
        {
            CreateMap<GoldBelonging, GoldBelongingAdminModel>().ReverseMap();
            CreateMap<GoldPriceInfo, GoldPriceInfoModel>().ReverseMap();
            CreateMap<GoldBelongingType, GoldBelongingTypeAdminModel>().ReverseMap();
            CreateMap<B2CGoldSettings, B2CGoldSettingsModel>().ReverseMap();
            CreateMap<B2CGoldPayingListSettings, B2CGoldPayingListSettingsModel>().ReverseMap();
            CreateMap<GoldIngredient, GoldIngredientAdminModel>().ReverseMap(); 
            CreateMap<GoldIngredientSpecification, GoldIngredientSpecificationAdminModel>().ReverseMap();
            CreateMap<GoldContactInfo, GoldContactInfoModel>().ReverseMap();
            CreateMap<GoldProposalValue, GoldProposalValueModel>().ReverseMap();
            CreateMap<GoldProposalValue, GoldProposalValueViewModel>().ReverseMap();
            CreateMap<GoldProductBelongingCalculation, GoldBelongingPayingAdminModel>().ReverseMap();
            CreateMap<ProductGoldBelongingMappingModel, ProductGoldBelongingMapping>()
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.BelongingWeight))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.BelongingCount));
            CreateMap<ProductGoldBelongingMapping, ProductGoldBelongingMappingModel>()
            .ForMember(dest => dest.BelongingWeight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.BelongingCount, opt => opt.MapFrom(src => src.Count));
        }

        public int Order => 0;
    }
}
