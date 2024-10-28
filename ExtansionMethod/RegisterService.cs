using MainApp.DataContexts;
using MainApp.Services.BookingServices;
using MainApp.Services.ProviderServices;
using MainApp.Services.ScheduleServices;
using MainApp.Services.ServiceInfoServices;
using MainApp.Services.UserServices;
using Microsoft.EntityFrameworkCore;

namespace MainApp.ExtansionMethod;

public static class RegisterService
{
    public static void AddRegistration(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        serviceCollection.AddScoped<IBookingService, BookingService>();
        serviceCollection.AddScoped<IProviderService, ProviderService>();
        serviceCollection.AddScoped<IScheduleService, ScheduleService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IServiceInfoService,ServiceInfoService>();
        serviceCollection.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    }
}