using MainApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainApp.EntitiesConfiguration;

public sealed class BookingConfig:IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.ProviderId, x.ServiceInfoId, x.StartDateTime, x.EndDateTime }).IsUnique();
        builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.HasCheckConstraint("CHK_TotalPrice_Positive", "\"TotalPrice\" >= 0");
        builder.HasOne(x => x.User).WithMany(b => b.Bookings).HasForeignKey(b => b.UserId);
        builder.HasOne(x => x.ServiceInfo).WithMany(x => x.Bookings).HasForeignKey(x => x.ServiceInfoId);
        builder.HasOne(x => x.Provider).WithMany(x => x.Bookings).HasForeignKey(x => x.ProviderId);
    }
}