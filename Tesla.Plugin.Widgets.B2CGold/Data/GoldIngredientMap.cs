using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldIngredientMap : NopEntityTypeConfiguration<GoldIngredient>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldIngredient> builder)
        {
            builder.ToTable(nameof(GoldIngredient));
            builder.HasKey(GoldIngredient => GoldIngredient.Id);

            base.Configure(builder);
        }

        #endregion
    }
}
