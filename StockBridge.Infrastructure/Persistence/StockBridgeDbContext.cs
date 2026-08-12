using Microsoft.EntityFrameworkCore;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using StockBridge.Infrastructure.Persistence.ModelConfiguration;
using System.Data;

namespace StockBridge.API.Data;

public partial class StockBridgeDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public StockBridgeDbContext(DbContextOptions<StockBridgeDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        this._currentUserService = currentUserService;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountInformation> AccountInformations { get; set; }

    public virtual DbSet<AreaRoute> AreaRoutes { get; set; }

    public virtual DbSet<AttendanceDetail> AttendanceDetails { get; set; }

    public virtual DbSet<BalanceTemporary> BalanceTemporaries { get; set; }

    public virtual DbSet<BarcodeLabelDatum> BarcodeLabelData { get; set; }

    public virtual DbSet<CashBankTransaction> CashBankTransactions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CreditHeader> CreditHeaders { get; set; }

    public virtual DbSet<CreditSale> CreditSales { get; set; }

    public virtual DbSet<CreditSaleSummary> CreditSaleSummaries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DayOff> DayOffs { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DocumentNumber> DocumentNumbers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeSale> EmployeeSales { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<FormulaHeader> FormulaHeaders { get; set; }

    public virtual DbSet<FormulaLine> FormulaLines { get; set; }

    public virtual DbSet<GoodsReceiptTemporaryDetail> GoodsReceiptTemporaryDetails { get; set; }

    public virtual DbSet<GoodsReceiptTemporaryHeader> GoodsReceiptTemporaryHeaders { get; set; }

    public virtual DbSet<HeaderWriteTemporary> HeaderWriteTemporaries { get; set; }

    public virtual DbSet<HotItem> HotItems { get; set; }

    public virtual DbSet<InventoryHeaderTransaction> InventoryHeaderTransactions { get; set; }

    public virtual DbSet<InventoryHeaderVoucher> InventoryHeaderVouchers { get; set; }

    public virtual DbSet<InventoryLineTransaction> InventoryLineTransactions { get; set; }

    public virtual DbSet<InventoryWarehouseTransaction> InventoryWarehouseTransactions { get; set; }

    public virtual DbSet<InventoryWarehouseTransactionReturn> InventoryWarehouseTransactionReturns { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<MainLocation> MainLocations { get; set; }

    public virtual DbSet<MultiItem> MultiItems { get; set; }

    public virtual DbSet<OperationHeader> OperationHeaders { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<Programs> Programs { get; set; }

    public virtual DbSet<SalaryDetail> SalaryDetails { get; set; }

    public virtual DbSet<SalesCheque> SalesCheques { get; set; }

    public virtual DbSet<SalesChequeTemporary> SalesChequeTemporaries { get; set; }

    public virtual DbSet<SalesRepresentative> SalesRepresentatives { get; set; }

    public virtual DbSet<SalesRepresentativeStockOnHand> SalesRepresentativeStockOnHands { get; set; }

    public virtual DbSet<SalesTemporary> SalesTemporaries { get; set; }

    public virtual DbSet<SignOn> SignOns { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockAnalysis> StockAnalyses { get; set; }

    public virtual DbSet<StockDetail> StockDetails { get; set; }

    public virtual DbSet<StockOnHandSummary> StockOnHandSummaries { get; set; }

    public virtual DbSet<StockVariance> StockVariances { get; set; }

    public virtual DbSet<StoreTransferTransaction> StoreTransferTransactions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierType> SupplierTypes { get; set; }

    public virtual DbSet<Systems> Systems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroupPermission> UserGroupPermissions { get; set; }

    public virtual DbSet<VersionHeader> VersionHeaders { get; set; }

    public virtual DbSet<VoucherHeader> VoucherHeaders { get; set; }

    public virtual DbSet<VoucherInventoryHeader> VoucherInventoryHeaders { get; set; }

    public virtual DbSet<VoucherInventoryLine> VoucherInventoryLines { get; set; }

    public virtual DbSet<VoucherTemporary> VoucherTemporaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=StockBridgeNew;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountInformationConfiguration());
        modelBuilder.ApplyConfiguration(new AreaRouteConfiguration());
        modelBuilder.ApplyConfiguration(new AttendanceDetailConfiguration());
        modelBuilder.ApplyConfiguration(new BalanceTemporaryConfiguration());
        modelBuilder.ApplyConfiguration(new BarcodeLabelDatumConfiguration());
        modelBuilder.ApplyConfiguration(new CashBankTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CreditHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new CreditSaleConfiguration());
        modelBuilder.ApplyConfiguration(new CreditSaleSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new DayOffConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryMethodConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentNumberConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeSaleConfiguration());
        modelBuilder.ApplyConfiguration(new FamilyConfiguration());
        modelBuilder.ApplyConfiguration(new FormulaHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new FormulaLineConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptTemporaryDetailConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptTemporaryHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new HeaderWriteTemporaryConfiguration());
        modelBuilder.ApplyConfiguration(new HotItemConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryHeaderTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryHeaderVoucherConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryLineTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryWarehouseTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryWarehouseTransactionReturnConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new MainLocationConfiguration());
        modelBuilder.ApplyConfiguration(new MultiItemConfiguration());
        modelBuilder.ApplyConfiguration(new OperationHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentDetailConfiguration());
        modelBuilder.ApplyConfiguration(new PriceListConfiguration());
        modelBuilder.ApplyConfiguration(new ProgramsConfiguration());
        modelBuilder.ApplyConfiguration(new SalaryDetailConfiguration());
        modelBuilder.ApplyConfiguration(new SalesChequeConfiguration());
        modelBuilder.ApplyConfiguration(new SalesChequeTemporaryConfiguration());
        modelBuilder.ApplyConfiguration(new SalesRepresentativeConfiguration());
        modelBuilder.ApplyConfiguration(new SalesRepresentativeStockOnHandConfiguration());
        modelBuilder.ApplyConfiguration(new SalesTemporaryConfiguration());
        modelBuilder.ApplyConfiguration(new SignOnConfiguration());
        modelBuilder.ApplyConfiguration(new SizeConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new StockAnalysisConfiguration());
        modelBuilder.ApplyConfiguration(new StockDetailConfiguration());
        modelBuilder.ApplyConfiguration(new StockOnHandSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new StockVarianceConfiguration());
        modelBuilder.ApplyConfiguration(new StoreTransferTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SystemsConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new VersionHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new VoucherHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new VoucherInventoryHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new VoucherInventoryLineConfiguration());
        modelBuilder.ApplyConfiguration(new VoucherTemporaryConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId ?? 0;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedBy = userId;
                entry.Entity.ModifiedOn = DateTime.UtcNow;
            }
        }

        //await ExcludeMissingColumnsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    ///// <summary>
    ///// When the physical database table is missing columns that the mapped entity declares,
    ///// INSERT/UPDATE statements that reference them fail with "Invalid column name 'X'".
    ///// This marks such properties as unmodified (cached per table, checked once per process),
    ///// so EF simply omits them from the generated INSERT/UPDATE statements.
    ///// </summary>
    //private async Task ExcludeMissingColumnsAsync(CancellationToken cancellationToken)
    //{
    //    foreach (var group in ChangeTracker.Entries()
    //        .Where(e => e.State is EntityState.Added or EntityState.Modified)
    //        .GroupBy(e => e.Metadata))
    //    {
    //        var entityType = group.Key;
    //        var tableName = entityType.GetTableName();
    //        if (string.IsNullOrEmpty(tableName))
    //            continue;

    //        var schemaName = entityType.GetSchema() ?? Model.GetDefaultSchema();

    //        var columns = await GetTableColumnsAsync(schemaName, tableName, cancellationToken);
    //        if (columns == null || columns.Count == 0)
    //            continue;

    //        foreach (var entry in group)
    //        {
    //            foreach (var property in entityType.GetProperties())
    //            {
    //                if (property.IsShadowProperty())
    //                    continue;

    //                var columnName = property.GetColumnName() ?? property.Name;
    //                if (!columns.Contains(columnName))
    //                {
    //                    entry.Property(property.Name).IsModified = false;
    //                }
    //            }
    //        }
    //    }
    //}

    //private async Task<IReadOnlySet<string>?> GetTableColumnsAsync(string? schema, string table, CancellationToken cancellationToken)
    //{
    //    var cacheKey = $"{(schema ?? "dbo")}.{table}";
    //    if (TableColumnsCache.TryGetValue(cacheKey, out var cached))
    //        return cached;

    //    var connection = Database.GetDbConnection();
    //    var openedHere = connection.State != ConnectionState.Open;
    //    if (openedHere)
    //        await connection.OpenAsync(cancellationToken);

    //    try
    //    {
    //        await using var command = connection.CreateCommand();
    //        command.CommandText = schema != null
    //            ? """
    //                SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
    //                WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table
    //                """
    //            : """
    //                SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
    //                WHERE TABLE_NAME = @table
    //                """;

    //        if (schema != null)
    //        {
    //            var schemaParameter = command.CreateParameter();
    //            schemaParameter.ParameterName = "@schema";
    //            schemaParameter.Value = schema;
    //            command.Parameters.Add(schemaParameter);
    //        }

    //        var tableParameter = command.CreateParameter();
    //        tableParameter.ParameterName = "@table";
    //        tableParameter.Value = table;
    //        command.Parameters.Add(tableParameter);

    //        var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    //        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
    //        while (await reader.ReadAsync(cancellationToken))
    //            columns.Add(reader.GetString(0));

    //        TableColumnsCache[cacheKey] = columns;
    //        return columns;
    //    }
    //    finally
    //    {
    //        if (openedHere)
    //            await connection.CloseAsync();
    //    }
    //}

}
