using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldPriceInfoMap : NopEntityTypeConfiguration<GoldPriceInfo>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldPriceInfo> builder)
        {
            builder.ToTable(nameof(GoldPriceInfo));
            builder.HasKey(ProductGoldInfoMap => ProductGoldInfoMap.Id);

            base.Configure(builder);
        }

        #endregion
    }
}
