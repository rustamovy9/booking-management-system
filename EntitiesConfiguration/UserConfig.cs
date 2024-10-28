using MainApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainApp.EntitiesConfiguration;

public sealed class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired().HasColumnType("varchar");
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired().HasColumnType("varchar");
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired().HasColumnType("varchar");
        builder.HasIndex(x=>x.Email).IsUnique();
        builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired().HasColumnType("varchar");
        builder.HasCheckConstraint("CHK_PhoneNumber_Length", "LENGTH(\"PhoneNumber\") >= 10");
        builder.HasIndex(x=>x.PhoneNumber).IsUnique();
        builder.HasMany(x => x.Bookings).WithOne(b => b.User).HasForeignKey(b => b.UserId);
    }
}