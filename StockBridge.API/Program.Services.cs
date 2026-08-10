using Microsoft.Extensions.DependencyInjection;
using StockBridge.API;
using StockBridge.Domain.Interfaces;

public partial class Program
{
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Entity service registrations will be added here as they are implemented.
    }
}