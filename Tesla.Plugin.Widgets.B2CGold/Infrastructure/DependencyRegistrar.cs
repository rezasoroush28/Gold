using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Infrastructure.Extensions;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Factories;
using Tesla.Plugin.Widgets.Gold.Services;
using Tesla.Plugin.Widgets.B2CGold.CalculationFormula;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Factories;
using Nop.Services.Catalog;
using Tesla.Plugin.Widgets.B2CGold.Services;
using NopLine.Nop.Framework.ActionFilters;
using Tesla.Plugin.Widgets.B2CGold.ActionFilters;

namespace Tesla.Plugin.Widgets.B2CGold.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = nameof(B2CGoldContext);
        private const string PRICE_CONTEXT_NAME = nameof(GoldPriceGrabberContext);

        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterPluginDataContext<B2CGoldContext>(CONTEXT_NAME);

            builder.RegisterPluginDataContext<GoldPriceGrabberContext>(PRICE_CONTEXT_NAME);

            builder.RegisterType<B2CGoldModelFactory>().As<IB2CGoldModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldContactInfoModelFactory>().As<IGoldContactInfoModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldProposalValueModelFactory>().As<IGoldProposalValueModelFactory>().InstancePerLifetimeScope();
           
            builder.RegisterType<GoldPriceCalculatorFactory>().As<IGoldPriceCalculatorFactory>().InstancePerLifetimeScope();
            
            builder.RegisterType<EfRepository<ProductGoldBelongingMapping>>().As<IRepository<ProductGoldBelongingMapping>>();

            builder.RegisterType<EfRepository<GoldPriceInfo>>().As<IRepository<GoldPriceInfo>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
            .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldPriceScheduleTask>>().As<IRepository<GoldPriceScheduleTask>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>(PRICE_CONTEXT_NAME))
            .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldBelongingType>>().As<IRepository<GoldBelongingType>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldBelonging>>().As<IRepository<GoldBelonging>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldIngredient>>().As<IRepository<GoldIngredient>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldIngredientSpecification>>().As<IRepository<GoldIngredientSpecification>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<ProductGoldBelongingMapping>>().As<IRepository<ProductGoldBelongingMapping>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldContactInfo>>().As<IRepository<GoldContactInfo>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldProductBelongingCalculation>>().As<IRepository<GoldProductBelongingCalculation>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GoldProposalValue>>().As<IRepository<GoldProposalValue>>()
           .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
           .InstancePerLifetimeScope();

            builder.RegisterType<GoldPriceCalculationService>().As<IPriceCalculationService>().InstancePerLifetimeScope();
            
            builder.RegisterType<GoldPriceCalculationService>().As<IGoldPriceCalculationService>().InstancePerLifetimeScope();
            
            builder.RegisterType<GoldBelongingTypeService>().As<IGoldBelongingTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldBelongingService>().As<IGoldBelongingService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldPriceInfoService>().As<IGoldPriceInfoService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldPriceService>().As<IGoldPriceService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldIngredientService>().As<IGoldIngredientService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldIngredientSpecificationService>().As<IGoldIngredientSpecificationService>().InstancePerLifetimeScope();

            builder.RegisterType<ProductGoldBelongingMappingService>().As<IProductGoldBelongingMappingService>().InstancePerLifetimeScope();

            builder.RegisterType<B2CGoldModelFactory>().As<IB2CGoldModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldContactInfoService>().As<IGoldContactInfoService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldProposalValueService>().As<IGoldProposalValueService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldProposalValueViewModelFactory>().As<IGoldProposalValueViewModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldContactInfoViewModelFactory>().As<IGoldContactInfoViewModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldPriceInfoModelFactory>().As<IGoldPriceInfoModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<GoldProductBelongingCalculationService>().As<IGoldProductBelongingCalculationService>().InstancePerLifetimeScope();

            builder.RegisterType<GoldOrderDetailsModelFactory>().As<IGoldOrderDetailsModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<PriceWorkContext>().As<IPriceWorkContext>().InstancePerLifetimeScope();

            
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}