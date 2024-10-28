using MainApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainApp.EntitiesConfiguration;

public sealed class ScheduleConfig:IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Provider).WithMany(x => x.Schedules).HasForeignKey(x => x.ProviderId);
    }
}