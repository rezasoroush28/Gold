using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldIngredientSpecificationMap : NopEntityTypeConfiguration<GoldIngredientSpecification>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldIngredientSpecification> builder)
        {
            builder.ToTable(nameof(GoldIngredientSpecification));
            builder.HasKey(GoldIngredientSpecification => GoldIngredientSpecification.Id);

            base.Configure(builder);
        }

        #endregion
    }
}
