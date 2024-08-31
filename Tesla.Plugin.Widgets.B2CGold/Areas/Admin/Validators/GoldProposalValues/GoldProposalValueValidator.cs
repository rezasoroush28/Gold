using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Validators.GoldProposalValues
{
    public partial class GoldProposalValueValidator : BaseNopValidator<GoldProposalValueModel>
    {
        public GoldProposalValueValidator(ILocalizationService localizationService,
            IDbContext dbContext)
        {
            //form fields
            RuleFor(x => x.ValueTitle)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.ValueTitle.Required.Error"));

            RuleFor(x => x.ValueDescription)
                .NotEmpty()
                .NotNull()
                .WithMessage(localizationService.GetResource("Plugins.Widgets.B2CGold.GoldProposalValue.ValueDescription.Required.Error"));
          
            SetDatabaseValidationRules<GoldProposalValue>(dbContext);
        }
    }
}