using MainApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainApp.EntitiesConfiguration;

public sealed class ServiceInfoConfig:IEntityTypeConfiguration<ServiceInfo>
{
    public void Configure(EntityTypeBuilder<ServiceInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Bookings).WithOne(x => x.ServiceInfo).HasForeignKey(x => x.ServiceInfoId);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired().HasColumnType("varchar");
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.HasCheckConstraint("CHK_Price_Positive", "\"Price\" >= 0");
    }
}