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

        services.AddScoped<IRepository<Category>, Repository<Category>>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IRepository<Company>, Repository<Company>>();
        services.AddScoped<ICompanyService, CompanyService>();

        services.AddScoped<IRepository<Customer>, Repository<Customer>>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IRepository<Department>, Repository<Department>>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        // Entity service registrations will be added here as they are implemented.
    }
}