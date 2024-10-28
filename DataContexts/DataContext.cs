using MainApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainApp.DataContexts;

public sealed class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<ServiceInfo> ServiceInfos { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}