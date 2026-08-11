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

        services.AddScoped<IRepository<Employee>, Repository<Employee>>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IRepository<Item>, Repository<Item>>();
        services.AddScoped<IItemService, ItemService>();

        services.AddScoped<IRepository<MainLocation>, Repository<MainLocation>>();
        services.AddScoped<IMainLocationService, MainLocationService>();

        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<ISupplierService, SupplierService>();

        services.AddScoped<IRepository<SalesRepresentative>, Repository<SalesRepresentative>>();
        services.AddScoped<ISalesRepresentativeService, SalesRepresentativeService>();

        services.AddScoped<IRepository<AccountInformation>, Repository<AccountInformation>>();
        services.AddScoped<IAccountInformationService, AccountInformationService>();

        services.AddScoped<IRepository<AreaRoute>, Repository<AreaRoute>>();
        services.AddScoped<IAreaRouteService, AreaRouteService>();

        services.AddScoped<IRepository<DayOff>, Repository<DayOff>>();
        services.AddScoped<IDayOffService, DayOffService>();

        services.AddScoped<IRepository<HotItem>, Repository<HotItem>>();
        services.AddScoped<IHotItemService, HotItemService>();

        services.AddScoped<IRepository<MultiItem>, Repository<MultiItem>>();
        services.AddScoped<IMultiItemService, MultiItemService>();

        services.AddScoped<IRepository<PriceList>, Repository<PriceList>>();
        services.AddScoped<IPriceListService, PriceListService>();

        services.AddScoped<IGoodsReceiptTemporaryRepository, GoodsReceiptTemporaryRepository>();
        services.AddScoped<IGoodsReceiptTemporaryService, GoodsReceiptTemporaryService>();

        services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
        services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();

        services.AddScoped<IFormulaRepository, FormulaRepository>();
        services.AddScoped<IFormulaService, FormulaService>();

        services.AddScoped<IVoucherInventoryRepository, VoucherInventoryRepository>();
        services.AddScoped<IVoucherInventoryService, VoucherInventoryService>();

        // Entity service registrations will be added here as they are implemented.
    }
}