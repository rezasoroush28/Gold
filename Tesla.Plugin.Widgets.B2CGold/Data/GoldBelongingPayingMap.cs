using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldProductBelongingMap : NopEntityTypeConfiguration<GoldProductBelongingCalculation>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldProductBelongingCalculation> builder)
        {
            builder.ToTable(nameof(GoldProductBelongingCalculation));
            builder.Ignore(a => a.GoldBelongingCalculationType);
            builder.HasKey(GoldProductBelonging => GoldProductBelonging.Id);

            base.Configure(builder);
        }

        #endregion
    }
}
