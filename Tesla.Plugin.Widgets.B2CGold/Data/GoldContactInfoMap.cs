using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Tesla.Plugin.Widgets.B2CGold.Data
{
    public partial class GoldContactInfoMap : NopEntityTypeConfiguration<GoldContactInfo>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<GoldContactInfo> builder)
        {
            builder.ToTable(nameof(GoldContactInfo));
            builder.HasKey(goldContactInfo => goldContactInfo.Id);
            builder.Ignore(goldContactInfo => goldContactInfo.GoldContactInfoType);
            builder.Ignore(goldContactInfo => goldContactInfo.StartDayofWeekEnum);
            builder.Ignore(goldContactInfo => goldContactInfo.EndDayofWeekEnum);
            builder.Property(goldContactInfo => goldContactInfo.Value)
                   .HasMaxLength(100);

            base.Configure(builder);
        }

        #endregion
    }
}
