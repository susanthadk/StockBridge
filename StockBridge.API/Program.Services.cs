using Microsoft.Extensions.DependencyInjection;
using StockBridge.API;
using StockBridge.Application.Interfaces;
using StockBridge.Application.Services;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using StockBridge.Infrastructure.Repositories;

public partial class Program
{
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IRepository<Account>, Repository<Account>>();
        services.AddScoped<IAccountService, AccountService>();

        // Entity service registrations will be added here as they are implemented.
    }
}