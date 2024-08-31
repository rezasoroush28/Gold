using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nop.Core;
using Nop.Data.Mapping;

using System;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public partial class GoldPriceScheduleTaskMap : NopEntityTypeConfiguration<GoldPriceScheduleTask>
    {
        public override void Configure(EntityTypeBuilder<GoldPriceScheduleTask> builder)
        {
            builder.ToTable("ScheduleTask");
            builder.HasKey(GoldPriceScheduleTask => GoldPriceScheduleTask.Id);
            base.Configure(builder);
        }
    }

}
