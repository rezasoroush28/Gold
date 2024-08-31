using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldProposalValueMap : NopEntityTypeConfiguration<GoldProposalValue>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldProposalValue> builder)
        {
            builder.ToTable(nameof(GoldProposalValue));
            builder.HasKey(goldProposalValue => goldProposalValue.Id);
            builder.Property(goldProposalValue => goldProposalValue.ValueTitle)
                   .HasMaxLength(200);
            builder.Property(goldProposalValue => goldProposalValue.ValueDescription)
                   .HasMaxLength(500);
            builder.Property(goldProposalValue => goldProposalValue.IconPath)
                   .HasMaxLength(500);

            base.Configure(builder);
        }

        #endregion
    }
}
