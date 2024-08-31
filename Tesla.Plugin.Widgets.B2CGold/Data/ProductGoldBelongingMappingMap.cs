using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public class ProductGoldBelongingMappingMap : NopEntityTypeConfiguration<ProductGoldBelongingMapping>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ProductGoldBelongingMapping> builder)
        {
            builder.ToTable(nameof(ProductGoldBelongingMapping));
            builder.HasKey(ProductGoldBelongingMapping => ProductGoldBelongingMapping.Id);

            base.Configure(builder);
        }

        #endregion    
    }
}
