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

    public virtual DbSet<SalesRepresentativeMaster> SalesRepresentativeMasters { get; set; }

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

        modelBuilder.Entity<AccountInformation>(entity =>
        {
            entity.ToTable("AccountInformation");

            entity.Property(e => e.AccountDescriptioncription).HasMaxLength(30);
            entity.Property(e => e.AccountNumber).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_AccountInformation_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_AccountInformation_IsActive");
            entity.Property(e => e.SubCode).HasMaxLength(10);

            entity.HasOne(d => d.Account).WithMany(p => p.AccountInformations)
                .HasPrincipalKey(p => new { p.AccountNumber, p.SubCode })
                .HasForeignKey(d => new { d.AccountNumber, d.SubCode })
                .HasConstraintName("FK_AccountInformation_Account");
        });

        modelBuilder.Entity<AreaRoute>(entity =>
        {
            entity.ToTable("AreaRoute");

            entity.HasIndex(e => e.AreaCode, "UQ_AreaRoute_BusinessKey").IsUnique();

            entity.Property(e => e.AreaCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AreaName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_AreaRoute_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_AreaRoute_IsActive");
            entity.Property(e => e.ShortName)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AttendanceDetail>(entity =>
        {
            entity.ToTable("AttendanceDetail");

            entity.HasIndex(e => new { e.EmployeeProvidentFundNumber, e.InDate }, "UQ_AttendanceDetail_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_AttendanceDetail_CreatedOn");
            entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(10);
            entity.Property(e => e.InDate).HasColumnType("datetime");
            entity.Property(e => e.InTimee).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_AttendanceDetail_IsActive");
            entity.Property(e => e.LoginCode).HasMaxLength(10);
            entity.Property(e => e.OutDate).HasColumnType("datetime");
            entity.Property(e => e.OutTimee).HasColumnType("datetime");
        });

        modelBuilder.Entity<BalanceTemporary>(entity =>
        {
            entity.ToTable("BalanceTemporary");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Flag).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Text).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(6);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BarcodeLabelDatum>(entity =>
        {
            entity.HasKey(e => e.BarcodeLabelDataId);

            entity.Property(e => e.InitialCreate).HasMaxLength(10);
            entity.Property(e => e.ItemCode)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.SellingPrice).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.SellingPriceDescriptioncription)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StockCode)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.StockDescriptioncription)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CashBankTransaction>(entity =>
        {
            entity.ToTable("CashBankTransaction");

            entity.HasIndex(e => new { e.BankCode, e.OperationCode, e.TerminalNumber, e.VisitCode, e.StartDate }, "UQ_CashBankTransaction_BusinessKey").IsUnique();

            entity.Property(e => e.BalanceDate).HasColumnType("datetime");
            entity.Property(e => e.BankCode).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_CashBankTransaction_CreatedOn");
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_CashBankTransaction_IsActive");
            entity.Property(e => e.OperationCode).HasMaxLength(5);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.VisitCode).HasMaxLength(5);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.HasIndex(e => new { e.CategoryCode, e.DepartmentCode }, "UQ_Category_BusinessKey").IsUnique();

            entity.Property(e => e.CategoryCode).HasMaxLength(10);
            entity.Property(e => e.CategoryName).HasMaxLength(40);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Category_CreatedOn");
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Category_IsActive");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Categories)
                .HasPrincipalKey(p => p.DepartmentCode)
                .HasForeignKey(d => d.DepartmentCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Department");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.HasIndex(e => e.LocationNumber, "UQ_Company_BusinessKey").IsUnique();

            entity.Property(e => e.ConditionMsg)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Company_CreatedOn");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Company_IsActive");
            entity.Property(e => e.LocationNumber).HasMaxLength(5);
            entity.Property(e => e.Logo1).HasMaxLength(20);
            entity.Property(e => e.Logo2).HasMaxLength(20);
            entity.Property(e => e.Logo3).HasMaxLength(40);
            entity.Property(e => e.Logo4).HasMaxLength(40);
            entity.Property(e => e.Logo5).HasMaxLength(40);
            entity.Property(e => e.Mess1).HasMaxLength(16);
            entity.Property(e => e.Mess2).HasMaxLength(16);
            entity.Property(e => e.Mess3).HasColumnType("ntext");
        });

        modelBuilder.Entity<CreditHeader>(entity =>
        {
            entity.ToTable("CreditHeader");

            entity.HasIndex(e => e.CreditCode, "UQ_CreditHeader_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_CreditHeader_CreatedOn");
            entity.Property(e => e.CreditCode).HasMaxLength(10);
            entity.Property(e => e.CreditDescriptioncription).HasMaxLength(35);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_CreditHeader_IsActive");
        });

        modelBuilder.Entity<CreditSale>(entity =>
        {
            entity.ToTable("CreditSale");

            entity.HasIndex(e => new { e.CustomerCode, e.CustomerInvoiceNumber, e.InvoiceDate, e.TerminalCode, e.OperationCode, e.CreditCode }, "UQ_CreditSale_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_CreditSale_CreatedOn");
            entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreditCode).HasMaxLength(10);
            entity.Property(e => e.CreditDate).HasColumnType("datetime");
            entity.Property(e => e.CreditPeriod).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerCode).HasMaxLength(10);
            entity.Property(e => e.CustomerInvoiceNumber).HasMaxLength(15);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_CreditSale_IsActive");
            entity.Property(e => e.OperationCode).HasMaxLength(5);

            entity.HasOne(d => d.CreditCodeNavigation).WithMany(p => p.CreditSales)
                .HasPrincipalKey(p => p.CreditCode)
                .HasForeignKey(d => d.CreditCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditSale_CreditHeader");
        });

        modelBuilder.Entity<CreditSaleSummary>(entity =>
        {
            entity.ToTable("CreditSaleSummary");

            entity.HasIndex(e => new { e.InvoiceNumber, e.InvoiceDate, e.SalesRepresentativeresentativeCode, e.CompanyCode, e.CustomerCode }, "UQ_CreditSaleSummary_BusinessKey").IsUnique();

            entity.Property(e => e.CompanyCode).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_CreditSaleSummary_CreatedOn");
            entity.Property(e => e.CustomerCode).HasMaxLength(10);
            entity.Property(e => e.InvoiceAmountDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceBalancePayment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceCashDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceCashDiscountRate).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InvoiceCashReceived).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceChequeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceItemDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceNetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(15);
            entity.Property(e => e.InvoiceSpecialDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_CreditSaleSummary_IsActive");
            entity.Property(e => e.SalesRepresentativeresentativeCode).HasMaxLength(10);

            entity.HasOne(d => d.SalesRepresentativeresentativeCodeNavigation).WithMany(p => p.CreditSaleSummaries)
                .HasPrincipalKey(p => p.SalesRepresentativeresentativeCode)
                .HasForeignKey(d => d.SalesRepresentativeresentativeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditSaleSummary_SalesRepresentative");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerCode, "UQ_Customer_BusinessKey").IsUnique();

            entity.Property(e => e.AreaCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Customer_CreatedOn");
            entity.Property(e => e.CreditLimit).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.CustomerCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerFax)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CustomerTelephone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Customer_IsActive");
            entity.Property(e => e.MainLocationCode)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.AreaCodeNavigation).WithMany(p => p.Customers)
                .HasPrincipalKey(p => p.AreaCode)
                .HasForeignKey(d => d.AreaCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_AreaRoute");
        });

        modelBuilder.Entity<DayOff>(entity =>
        {
            entity.ToTable("DayOff");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_DayOff_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_DayOff_IsActive");
            entity.Property(e => e.OffDate).HasColumnType("datetime");
            entity.Property(e => e.OffNumber).HasMaxLength(10);
            entity.Property(e => e.OffTimee).HasColumnType("datetime");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentCode, "UQ_Department_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Department_CreatedOn");
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.DepartmentName).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Department_IsActive");
        });

        modelBuilder.Entity<DocumentNumber>(entity =>
        {
            entity.ToTable("DocumentNumber");

            entity.HasIndex(e => new { e.MainLocationCode, e.StationId, e.DocumentType }, "UQ_DocumentNumber_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_DocumentNumber_CreatedOn");
            entity.Property(e => e.DocumentNumber1).HasColumnName("DocumentNumber");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_DocumentNumber_IsActive");
            entity.Property(e => e.MainLocationCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumberId)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.StationId)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.MainLocationCodeNavigation).WithMany(p => p.DocumentNumbers)
                .HasPrincipalKey(p => p.MainLocCode)
                .HasForeignKey(d => d.MainLocationCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentNumber_MainLocation");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeCode, "UQ_Employee_BusinessKey").IsUnique();

            entity.Property(e => e.AddressLine1).HasMaxLength(25);
            entity.Property(e => e.AddressLine2).HasMaxLength(25);
            entity.Property(e => e.CommissionRate)
                .HasDefaultValue(0m, "DF_Employee_CommissionRate")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Employee_CreatedOn");
            entity.Property(e => e.EmployeeCode).HasMaxLength(5);
            entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(10);
            entity.Property(e => e.EmployeeStatus).HasMaxLength(1);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.IdentificationNumber).HasMaxLength(12);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Employee_IsActive");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(12);
        });

        modelBuilder.Entity<EmployeeSale>(entity =>
        {
            entity.HasKey(e => e.EmployeeSalesId);

            entity.HasIndex(e => new { e.SaleDate, e.EmployeeProvidentFundNumber }, "UQ_EmployeeSales_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_EmployeeSales_CreatedOn");
            entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_EmployeeSales_IsActive");
            entity.Property(e => e.SaleAmendedQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaleAmendedValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.SaleQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaleValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.ToTable("Family");

            entity.HasIndex(e => new { e.FamilyCode, e.CategoryCode, e.DepartmentCode }, "UQ_Family_BusinessKey").IsUnique();

            entity.Property(e => e.CategoryCode).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Family_CreatedOn");
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.FamilyCode).HasMaxLength(10);
            entity.Property(e => e.FamilyName).HasMaxLength(40);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Family_IsActive");

            entity.HasOne(d => d.Category).WithMany(p => p.Families)
                .HasPrincipalKey(p => new { p.CategoryCode, p.DepartmentCode })
                .HasForeignKey(d => new { d.CategoryCode, d.DepartmentCode })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Family_Category");
        });

        modelBuilder.Entity<FormulaHeader>(entity =>
        {
            entity.ToTable("FormulaHeader");

            entity.HasIndex(e => e.FormulaNumber, "UQ_FormulaHeader_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_FormulaHeader_CreatedOn");
            entity.Property(e => e.FormulaDate).HasColumnType("datetime");
            entity.Property(e => e.FormulaNumber).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_FormulaHeader_IsActive");
        });

        modelBuilder.Entity<FormulaLine>(entity =>
        {
            entity.ToTable("FormulaLine");

            entity.HasIndex(e => new { e.FormulaNumber, e.ItemNumber }, "UQ_FormulaLine_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_FormulaLine_CreatedOn");
            entity.Property(e => e.FormulaNumber).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_FormulaLine_IsActive");
            entity.Property(e => e.ItemNumber).HasMaxLength(7);

            entity.HasOne(d => d.FormulaNumberNavigation).WithMany(p => p.FormulaLines)
                .HasPrincipalKey(p => p.FormulaNumber)
                .HasForeignKey(d => d.FormulaNumber)
                .HasConstraintName("FK_FormulaLine_FormulaHeader");

            entity.HasOne(d => d.ItemNumberNavigation).WithMany(p => p.FormulaLines)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.ItemNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormulaLine_Item");
        });

        modelBuilder.Entity<GoodsReceiptTemporaryDetail>(entity =>
        {
            entity.ToTable("GoodsReceiptTemporaryDetail");

            entity.HasIndex(e => new { e.GoodsReceiptNumber, e.TerminalNumber, e.ItmType }, "UQ_GoodsReceiptTemporaryDetail_BusinessKey").IsUnique();

            entity.Property(e => e.GoodsReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.GoodsReceiptNumber).HasMaxLength(10);
            entity.Property(e => e.GoodsReceiptQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GoodsReceiptSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItmType).HasMaxLength(7);

            entity.HasOne(d => d.GoodsReceiptTemporaryHeader).WithMany(p => p.GoodsReceiptTemporaryDetails)
                .HasPrincipalKey(p => new { p.GoodsReceiptNumber, p.TerminalNumber })
                .HasForeignKey(d => new { d.GoodsReceiptNumber, d.TerminalNumber })
                .HasConstraintName("FK_GoodsReceiptTemporaryDetail_GoodsReceiptTemporaryHeader");
        });

        modelBuilder.Entity<GoodsReceiptTemporaryHeader>(entity =>
        {
            entity.ToTable("GoodsReceiptTemporaryHeader");

            entity.HasIndex(e => new { e.GoodsReceiptNumber, e.TerminalNumber }, "UQ_GoodsReceiptTemporaryHeader_BusinessKey").IsUnique();

            entity.Property(e => e.GoodsReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.GoodsReceiptNumber).HasMaxLength(10);
            entity.Property(e => e.GoodsReceiptSite).HasMaxLength(4);
        });

        modelBuilder.Entity<HeaderWriteTemporary>(entity =>
        {
            entity.ToTable("HeaderWriteTemporary");

            entity.HasIndex(e => new { e.LineNumber, e.TerminalNumber }, "UQ_HeaderWriteTemporary_BusinessKey").IsUnique();

            entity.Property(e => e.CatCode).HasMaxLength(50);
            entity.Property(e => e.DiscountGroup).HasMaxLength(50);
            entity.Property(e => e.DiscountRate).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.GpDisRate).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.GpDisTyp).HasMaxLength(255);
            entity.Property(e => e.GpPrint).HasMaxLength(255);
            entity.Property(e => e.GpPrintQuantity).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasMaxLength(1);
            entity.Property(e => e.IsReturn).HasMaxLength(1);
            entity.Property(e => e.ItemDescriptioncription).HasMaxLength(30);
            entity.Property(e => e.ItemNumber).HasMaxLength(10);
            entity.Property(e => e.ItemType).HasMaxLength(7);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalesmanCode).HasMaxLength(5);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<HotItem>(entity =>
        {
            entity.ToTable("HotItem");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_HotItem_CreatedOn");
            entity.Property(e => e.Descriptioncription).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_HotItem_IsActive");
            entity.Property(e => e.ItemCode).HasMaxLength(7);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0m, "DF_HotItem_Quantity")
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.HotItems)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.ItemCode)
                .HasConstraintName("FK_HotItem_Item");
        });

        modelBuilder.Entity<InventoryHeaderTransaction>(entity =>
        {
            entity.ToTable("InventoryHeaderTransaction");

            entity.HasIndex(e => new { e.InventoryHeaderType, e.InventoryHeaderDocumentNumber, e.InventoryHeaderDate, e.InventoryHeaderOperationCode, e.TerminalNumber }, "UQ_InventoryHeaderTransaction_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryHeaderTransaction_CreatedOn");
            entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreditReceivedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderAddedByUserCode).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderAddedDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderAmendedByUserCode).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderAmendedDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderAreaCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.InventoryHeaderCashAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderCashDiscountAmount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InventoryHeaderCashDiscountPc)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("InventoryHeaderCashDiscountPC");
            entity.Property(e => e.InventoryHeaderCashDiscountcount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderCashDiscountcountRate).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InventoryHeaderCashHandoverTime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderChqAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderCompanyCode).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderCreditAccountNumber).HasMaxLength(20);
            entity.Property(e => e.InventoryHeaderCreditBankCode).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderCreditBilno)
                .HasMaxLength(8)
                .HasColumnName("InventoryHeaderCreditBILNO");
            entity.Property(e => e.InventoryHeaderCreditDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderCreditFlag).HasMaxLength(1);
            entity.Property(e => e.InventoryHeaderCreditName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InventoryHeaderCreditNumber).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderCreditType)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.InventoryHeaderCustomer).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderCustomerNam)
                .HasMaxLength(40)
                .HasColumnName("InventoryHeaderCustomerNAM");
            entity.Property(e => e.InventoryHeaderDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderDiscount).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InventoryHeaderDiscount01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderDiscount02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderDiscount1).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.InventoryHeaderDiscount1Amount1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderDiscount2Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderDiscount2Amount2).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderDiscountPc)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("InventoryHeaderDiscountPC");
            entity.Property(e => e.InventoryHeaderDocumentNumber).HasMaxLength(15);
            entity.Property(e => e.InventoryHeaderEntryUserCode).HasMaxLength(30);
            entity.Property(e => e.InventoryHeaderEtimee)
                .HasColumnType("datetime")
                .HasColumnName("InventoryHeaderETimee");
            entity.Property(e => e.InventoryHeaderExciseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderFrstk)
                .HasMaxLength(7)
                .HasColumnName("InventoryHeaderFRSTK");
            entity.Property(e => e.InventoryHeaderGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderLocation).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderLocationTf)
                .HasMaxLength(6)
                .HasColumnName("InventoryHeaderLocationTF");
            entity.Property(e => e.InventoryHeaderNetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderNetVatAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderNumberAme).HasColumnName("InventoryHeaderNumberAME");
            entity.Property(e => e.InventoryHeaderOperationCode).HasMaxLength(5);
            entity.Property(e => e.InventoryHeaderPaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderPaytyp)
                .HasMaxLength(3)
                .HasColumnName("InventoryHeaderPAYTYP");
            entity.Property(e => e.InventoryHeaderPost)
                .HasMaxLength(1)
                .HasColumnName("InventoryHeaderPOST");
            entity.Property(e => e.InventoryHeaderPrintFlg)
                .HasMaxLength(1)
                .HasColumnName("InventoryHeaderPrintFLG");
            entity.Property(e => e.InventoryHeaderQuantity).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity01).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity02).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity03).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity04).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity05).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity06).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity07).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity08).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity09).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity10).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity11).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity12).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity13).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderQuantity14).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryHeaderRebateVoucher01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher03).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderReference).HasMaxLength(20);
            entity.Property(e => e.InventoryHeaderReferenceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderReturnDocumentNumber).HasMaxLength(15);
            entity.Property(e => e.InventoryHeaderReturnGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderReturnQuantityAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderSaleTyp)
                .HasMaxLength(6)
                .HasColumnName("InventoryHeaderSaleTYP");
            entity.Property(e => e.InventoryHeaderSalesRepresentativeresentativeCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.InventoryHeaderSrepresentative)
                .HasMaxLength(10)
                .HasColumnName("InventoryHeaderSRepresentative");
            entity.Property(e => e.InventoryHeaderStimee)
                .HasColumnType("datetime")
                .HasColumnName("InventoryHeaderSTimee");
            entity.Property(e => e.InventoryHeaderSupplier).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderTostk)
                .HasMaxLength(7)
                .HasColumnName("InventoryHeaderTOSTK");
            entity.Property(e => e.InventoryHeaderTotalQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderTotalVoucher).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderTotalVoucherAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderType).HasMaxLength(2);
            entity.Property(e => e.InventoryHeaderVatAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVouCat01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVouCat02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVouCat03).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher01Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher01Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher02Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher02Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher03Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher03Code).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryHeaderTransaction_IsActive");
            entity.Property(e => e.TotalCreditAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<InventoryHeaderVoucher>(entity =>
        {
            entity.ToTable("InventoryHeaderVoucher");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryHeaderVoucher_CreatedOn");
            entity.Property(e => e.InventoryHeaderDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderDocumentNumber).HasMaxLength(15);
            entity.Property(e => e.InventoryHeaderOperationCode).HasMaxLength(5);
            entity.Property(e => e.InventoryHeaderRebateVoucher04).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher05).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher06).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher07).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher08).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher09).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher10).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher11).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher12).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher13).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher14).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher15).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher16).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher17).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher18).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher19).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher20).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher21).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher22).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher23).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher24).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher25).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher26).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher27).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher28).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher29).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher30).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher31).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher32).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher33).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher34).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher35).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher36).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher37).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher38).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher39).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher40).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher41).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher42).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher43).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher44).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher45).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher46).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher47).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher48).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher49).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher50).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher51).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher52).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher53).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher54).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher55).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher56).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher57).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher58).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher59).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher60).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher61).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher62).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher63).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher64).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher65).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher66).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher67).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher68).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher69).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher70).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher71).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher72).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher73).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher74).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher75).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher76).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher77).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher78).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher79).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher80).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher81).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher82).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher83).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher84).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher85).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher86).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebateVoucher87).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher04Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher04Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher05Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher05Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher06Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher06Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher07Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher07Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher08Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher08Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher09Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher09Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher10Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher10Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher11Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher11Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher12Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher12Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher13Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher13Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher14Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher14Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher15Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher15Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher16Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher16Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher17Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher17Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher18Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher18Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher19Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher19Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher20Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher20Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher21Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher21Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher22Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher22Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher23Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher23Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher24Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher24Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher25Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher25Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher26Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher26Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher27Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher27Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher28Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher28Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher29Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher29Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher30Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher30Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher31Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher31Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher32Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher32Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher33Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher33Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher34Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher34Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher35Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher35Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher36Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher36Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher37Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher37Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher38Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher38Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher39Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher39Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher40Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher40Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher41Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher41Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher42Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher42Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher43Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher43Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher44Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher44Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher45Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher45Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher46Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher46Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher47Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher47Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher48Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher48Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher49Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher49Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher50Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher50Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher51Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher51Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher52Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher52Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher53Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher53Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher54Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher54Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher55Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher55Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher56Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher56Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher57Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher57Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher58Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher58Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher59Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher59Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher60Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher60Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher61Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher61Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher62Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher62Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher63Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher63Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher64Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher64Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher65Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher65Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher66Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher66Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher67Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher67Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher68Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher68Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher69Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher69Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher70Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher70Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher71Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher71Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher72Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher72Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher73Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher73Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher74Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher74Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher75Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher75Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher76Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher76Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher77Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher77Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher78Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher78Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher79Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher79Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher80Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher80Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher81Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher81Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher82Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher82Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher83Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher83Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher84Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher84Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher85Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher85Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher86Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher86Code).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVoucher87Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryHeaderVoucher87Code).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryHeaderVoucher_IsActive");
        });

        modelBuilder.Entity<InventoryLineTransaction>(entity =>
        {
            entity.ToTable("InventoryLineTransaction");

            entity.HasIndex(e => new { e.InventoryLineType, e.InventoryLineDocumentNumber, e.InventoryLineLineNumber, e.InventoryLineDate, e.InventoryLineOperationCode, e.TerminalNumber }, "UQ_InventoryLineTransaction_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryLineTransaction_CreatedOn");
            entity.Property(e => e.InventoryLineAccount).HasMaxLength(10);
            entity.Property(e => e.InventoryLineAmountdiscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryLineAMOUNTDISCOUNT");
            entity.Property(e => e.InventoryLineCashDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineCashDiscountRate)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("InventoryLineCashDiscountRATE");
            entity.Property(e => e.InventoryLineCategory).HasMaxLength(5);
            entity.Property(e => e.InventoryLineCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineCustomer).HasMaxLength(6);
            entity.Property(e => e.InventoryLineDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryLineDescriptioncription).HasMaxLength(40);
            entity.Property(e => e.InventoryLineDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineDiscountPc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryLineDiscountPC");
            entity.Property(e => e.InventoryLineDocumentNumber).HasMaxLength(15);
            entity.Property(e => e.InventoryLineEntryUserCode).HasMaxLength(30);
            entity.Property(e => e.InventoryLineGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineItemDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineItemDiscountRate)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("InventoryLineItemDiscountRATE");
            entity.Property(e => e.InventoryLineLocation).HasMaxLength(6);
            entity.Property(e => e.InventoryLineLocationTf)
                .HasMaxLength(6)
                .HasColumnName("InventoryLineLocationTF");
            entity.Property(e => e.InventoryLineNetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineOperationCode).HasMaxLength(5);
            entity.Property(e => e.InventoryLineQuantity).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity00).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity01).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity02).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity03).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity04).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity05).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity06).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity07).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity08).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity09).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity10).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity11).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity12).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity13).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineQuantity14).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.InventoryLineReturn).HasMaxLength(1);
            entity.Property(e => e.InventoryLineSalesRepresentativeresentativeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryLineSalesRepresentativeresentativeCODE");
            entity.Property(e => e.InventoryLineSaveflg)
                .HasMaxLength(1)
                .HasColumnName("InventoryLineSAVEFLG");
            entity.Property(e => e.InventoryLineSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineSmn)
                .HasMaxLength(10)
                .HasColumnName("InventoryLineSMN");
            entity.Property(e => e.InventoryLineSpecialDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryLineSpecialDiscountRate)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("InventoryLineSpecialDiscountRATE");
            entity.Property(e => e.InventoryLineStockP).HasMaxLength(7);
            entity.Property(e => e.InventoryLineType).HasMaxLength(2);
            entity.Property(e => e.InventoryLineValueUediscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryLineValueUEDISCOUNT");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryLineTransaction_IsActive");

            entity.HasOne(d => d.InventoryLineSizeNumberNavigation).WithMany(p => p.InventoryLineTransactions)
                .HasPrincipalKey(p => p.SizeNumber)
                .HasForeignKey(d => d.InventoryLineSizeNumber)
                .HasConstraintName("FK_InventoryLineTransaction_Size");

            entity.HasOne(d => d.InventoryLineStockPNavigation).WithMany(p => p.InventoryLineTransactions)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.InventoryLineStockP)
                .HasConstraintName("FK_InventoryLineTransaction_Item");

            entity.HasOne(d => d.InventoryHeaderTransaction).WithMany(p => p.InventoryLineTransactions)
                .HasPrincipalKey(p => new { p.InventoryHeaderType, p.InventoryHeaderDocumentNumber, p.InventoryHeaderDate, p.InventoryHeaderOperationCode, p.TerminalNumber })
                .HasForeignKey(d => new { d.InventoryLineType, d.InventoryLineDocumentNumber, d.InventoryLineDate, d.InventoryLineOperationCode, d.TerminalNumber })
                .HasConstraintName("FK_InventoryLineTransaction_InventoryHeaderTransaction");
        });

        modelBuilder.Entity<InventoryWarehouseTransaction>(entity =>
        {
            entity.ToTable("InventoryWarehouseTransaction");

            entity.HasIndex(e => new { e.InventoryWarehouseType, e.InventoryWarehouseLineNumber, e.TerminalNumber }, "UQ_InventoryWarehouseTransaction_BusinessKey").IsUnique();

            entity.Property(e => e.CashDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_CashDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_CashDiscountRate")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryWarehouseTransaction_CreatedOn");
            entity.Property(e => e.InventoryWarehouseAmountdiscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseAMOUNTDISCOUNT");
            entity.Property(e => e.InventoryWarehouseCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseDescriptioncription).HasMaxLength(40);
            entity.Property(e => e.InventoryWarehouseDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseDiscountPc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseDiscountPC");
            entity.Property(e => e.InventoryWarehouseGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseNetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity00).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity01).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity02).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity03).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity04).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity05).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity06).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity07).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity08).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity09).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity10).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity11).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity12).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity13).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity14).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseSalesRepresentativeresentativeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryWarehouseSalesRepresentativeresentativeCODE");
            entity.Property(e => e.InventoryWarehouseSaveflg)
                .HasMaxLength(1)
                .HasColumnName("InventoryWarehouseSAVEFLG");
            entity.Property(e => e.InventoryWarehouseSdr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseSDR");
            entity.Property(e => e.InventoryWarehouseSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseStockP).HasMaxLength(7);
            entity.Property(e => e.InventoryWarehouseType).HasMaxLength(2);
            entity.Property(e => e.InventoryWarehouseUno).HasColumnName("InventoryWarehouseUNO");
            entity.Property(e => e.InventoryWarehouseValueUediscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseValueUEDISCOUNT");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryWarehouseTransaction_IsActive");
            entity.Property(e => e.ItemDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_ItemDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_ItemDiscountRate")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.SpecialDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_SpecialDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SpecialDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_SpecialDiscountRate")
                .HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.InventoryWarehouseSizeNumberNavigation).WithMany(p => p.InventoryWarehouseTransactions)
                .HasPrincipalKey(p => p.SizeNumber)
                .HasForeignKey(d => d.InventoryWarehouseSizeNumber)
                .HasConstraintName("FK_InventoryWarehouseTransaction_Size");

            entity.HasOne(d => d.InventoryWarehouseStockPNavigation).WithMany(p => p.InventoryWarehouseTransactions)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.InventoryWarehouseStockP)
                .HasConstraintName("FK_InventoryWarehouseTransaction_Item");
        });

        modelBuilder.Entity<InventoryWarehouseTransactionReturn>(entity =>
        {
            entity.ToTable("InventoryWarehouseTransactionReturn");

            entity.HasIndex(e => new { e.InventoryWarehouseDocumentNumber, e.InventoryWarehouseType, e.InventoryWarehouseLineNumber, e.TerminalNumber }, "UQ_InventoryWarehouseTransactionReturn_BusinessKey").IsUnique();

            entity.Property(e => e.CashDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_CashDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_CashDiscountRate")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryWarehouseTransactionReturn_CreatedOn");
            entity.Property(e => e.InventoryWarehouseAmountdiscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseAMOUNTDISCOUNT");
            entity.Property(e => e.InventoryWarehouseCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseDescriptioncription).HasMaxLength(40);
            entity.Property(e => e.InventoryWarehouseDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseDiscountPc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseDiscountPC");
            entity.Property(e => e.InventoryWarehouseDocumentNumber).HasMaxLength(15);
            entity.Property(e => e.InventoryWarehouseGrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseNetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity00).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity01).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity02).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity03).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity04).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity05).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity06).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity07).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity08).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity09).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity10).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity11).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity12).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity13).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseQuantity14).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryWarehouseSalesRepresentativeresentativeCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryWarehouseSalesRepresentativeresentativeCODE");
            entity.Property(e => e.InventoryWarehouseSaveflg)
                .HasMaxLength(1)
                .HasColumnName("InventoryWarehouseSAVEFLG");
            entity.Property(e => e.InventoryWarehouseSdr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseSDR");
            entity.Property(e => e.InventoryWarehouseSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InventoryWarehouseStockP).HasMaxLength(7);
            entity.Property(e => e.InventoryWarehouseType).HasMaxLength(2);
            entity.Property(e => e.InventoryWarehouseUno).HasColumnName("InventoryWarehouseUNO");
            entity.Property(e => e.InventoryWarehouseValueUediscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InventoryWarehouseValueUEDISCOUNT");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryWarehouseTransactionReturn_IsActive");
            entity.Property(e => e.ItemDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_ItemDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_ItemDiscountRate")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.SpecialDiscountAmount)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_SpecialDiscountAmount")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SpecialDiscountRate)
                .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_SpecialDiscountRate")
                .HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.InventoryWarehouseSizeNumberNavigation).WithMany(p => p.InventoryWarehouseTransactionReturns)
                .HasPrincipalKey(p => p.SizeNumber)
                .HasForeignKey(d => d.InventoryWarehouseSizeNumber)
                .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Size");

            entity.HasOne(d => d.InventoryWarehouseStockPNavigation).WithMany(p => p.InventoryWarehouseTransactionReturns)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.InventoryWarehouseStockP)
                .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Item");

            entity.HasOne(d => d.InventoryWarehouseTransaction).WithMany(p => p.InventoryWarehouseTransactionReturns)
                .HasPrincipalKey(p => new { p.InventoryWarehouseType, p.InventoryWarehouseLineNumber, p.TerminalNumber })
                .HasForeignKey(d => new { d.InventoryWarehouseType, d.InventoryWarehouseLineNumber, d.TerminalNumber })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Transaction");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.HasIndex(e => e.ItemCode, "UQ_Item_BusinessKey").IsUnique();

            entity.Property(e => e.AccountCode).HasMaxLength(10);
            entity.Property(e => e.ActiveFlag).HasMaxLength(30);
            entity.Property(e => e.AddedByUserCode).HasMaxLength(30);
            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.AddedTime).HasMaxLength(30);
            entity.Property(e => e.AmendedByUserCode).HasMaxLength(30);
            entity.Property(e => e.AmendedDate).HasMaxLength(30);
            entity.Property(e => e.AmendedTime).HasMaxLength(30);
            entity.Property(e => e.AmendedTimeValue).HasMaxLength(30);
            entity.Property(e => e.CashDiscount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CategoryCode).HasMaxLength(10);
            entity.Property(e => e.ClosingQuantity).HasMaxLength(30);
            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Item_CreatedOn");
            entity.Property(e => e.CustomerNumber).HasMaxLength(30);
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.DiscountFlag).HasMaxLength(1);
            entity.Property(e => e.EntryDate).HasMaxLength(30);
            entity.Property(e => e.EntryTime).HasMaxLength(30);
            entity.Property(e => e.EntryUserCode).HasMaxLength(30);
            entity.Property(e => e.ExecutedQuantity).HasMaxLength(30);
            entity.Property(e => e.FamilyCode).HasMaxLength(10);
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.GoodsReceiptQuantity).HasMaxLength(30);
            entity.Property(e => e.GrossProfit).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.GroupWithPriceFlag).HasMaxLength(1);
            entity.Property(e => e.Ifscode)
                .HasMaxLength(30)
                .HasColumnName("IFSCode");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Item_IsActive");
            entity.Property(e => e.IssueQuantity).HasMaxLength(30);
            entity.Property(e => e.ItemCode).HasMaxLength(7);
            entity.Property(e => e.ItemDescription).HasMaxLength(40);
            entity.Property(e => e.ItemDiscount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ItemImage).HasMaxLength(50);
            entity.Property(e => e.ItemMetadata).HasMaxLength(30);
            entity.Property(e => e.MultiItemFlag).HasMaxLength(1);
            entity.Property(e => e.OpeningQuantity).HasMaxLength(30);
            entity.Property(e => e.OrderNumber).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PriceListFlag).HasMaxLength(30);
            entity.Property(e => e.RelatedItemCode).HasMaxLength(30);
            entity.Property(e => e.ReturnQuantity).HasMaxLength(30);
            entity.Property(e => e.SalesCommission)
                .HasDefaultValue(0m, "DF_Item_SalesCommission")
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.SalesDiscountRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalesQuantity).HasMaxLength(30);
            entity.Property(e => e.SalesReturnQuantity).HasMaxLength(30);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SellingPrice1).HasMaxLength(30);
            entity.Property(e => e.SellingPrice2).HasMaxLength(30);
            entity.Property(e => e.ShortQuantity).HasMaxLength(30);
            entity.Property(e => e.SlowStockCommission).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.SlowStockFlag).HasMaxLength(1);
            entity.Property(e => e.SpecialDiscount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.StockOnHandText).HasMaxLength(30);
            entity.Property(e => e.SupplierCode).HasMaxLength(10);
            entity.Property(e => e.SupplierReturnQuantity).HasMaxLength(30);
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Items)
                .HasPrincipalKey(p => p.DepartmentCode)
                .HasForeignKey(d => d.DepartmentCode)
                .HasConstraintName("FK_Item_Department");

            entity.HasOne(d => d.SizeNumberNavigation).WithMany(p => p.Items)
                .HasPrincipalKey(p => p.SizeNumber)
                .HasForeignKey(d => d.SizeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Size");

            entity.HasOne(d => d.SupplierCodeNavigation).WithMany(p => p.Items)
                .HasPrincipalKey(p => p.SupplierCode)
                .HasForeignKey(d => d.SupplierCode)
                .HasConstraintName("FK_Item_Supplier");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasPrincipalKey(p => new { p.CategoryCode, p.DepartmentCode })
                .HasForeignKey(d => new { d.CategoryCode, d.DepartmentCode })
                .HasConstraintName("FK_Item_Category");

            entity.HasOne(d => d.Family).WithMany(p => p.Items)
                .HasPrincipalKey(p => new { p.FamilyCode, p.CategoryCode, p.DepartmentCode })
                .HasForeignKey(d => new { d.FamilyCode, d.CategoryCode, d.DepartmentCode })
                .HasConstraintName("FK_Item_Family");
        });

        modelBuilder.Entity<MainLocation>(entity =>
        {
            entity.ToTable("MainLocation");

            entity.HasIndex(e => e.MainLocCode, "UQ_MainLocation_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_MainLocation_CreatedOn");
            entity.Property(e => e.DatabaseName).HasMaxLength(50);
            entity.Property(e => e.DatabasePassword).HasMaxLength(50);
            entity.Property(e => e.DatabaseServer).HasMaxLength(50);
            entity.Property(e => e.DatabaseUser).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_MainLocation_IsActive");
            entity.Property(e => e.LinkedToCpu).HasColumnName("LinkedToCPU");
            entity.Property(e => e.LocType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.MainLocCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MultiItem>(entity =>
        {
            entity.ToTable("MultiItem");

            entity.HasIndex(e => new { e.StockCode, e.SellingPrice }, "UQ_MultiItem_BusinessKey").IsUnique();

            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedByUser).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_MultiItem_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_MultiItem_IsActive");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockCode).HasMaxLength(8);
            entity.Property(e => e.StockDescriptioncription).HasMaxLength(40);
            entity.Property(e => e.StockOnHand)
                .HasDefaultValue(0m, "DF_MultiItem_StockOnHand")
                .HasColumnType("decimal(18, 3)");
            entity.Property(e => e.StockSizeCode).HasMaxLength(6);
            entity.Property(e => e.StockTypeCode).HasMaxLength(7);

            entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.MultiItems)
                .HasPrincipalKey(p => p.StockCode)
                .HasForeignKey(d => d.StockCode)
                .HasConstraintName("FK_MultiItem_Stock");

            entity.HasOne(d => d.StockTypeCodeNavigation).WithMany(p => p.MultiItems)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.StockTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MultiItem_Item");
        });

        modelBuilder.Entity<OperationHeader>(entity =>
        {
            entity.ToTable("OperationHeader");

            entity.HasIndex(e => new { e.OperationCode, e.OnDate, e.TerminalNumber }, "UQ_OperationHeader_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_OperationHeader_CreatedOn");
            entity.Property(e => e.EinvNo).HasColumnName("EInvNo");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_OperationHeader_IsActive");
            entity.Property(e => e.OnDate).HasColumnType("datetime");
            entity.Property(e => e.OnTimee).HasColumnType("datetime");
            entity.Property(e => e.OperationCode).HasMaxLength(5);
            entity.Property(e => e.Shift).HasMaxLength(25);
            entity.Property(e => e.SinvNo).HasColumnName("SInvNo");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.ToTable("PaymentDetail");

            entity.Property(e => e.AccountCode).HasMaxLength(10);
            entity.Property(e => e.AccountDescriptioncription).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_PaymentDetail_CreatedOn");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_PaymentDetail_IsActive");
            entity.Property(e => e.OnDate).HasColumnType("datetime");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Units).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.ToTable("PriceList");

            entity.HasIndex(e => e.PriceListPrl, "UQ_PriceList_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_PriceList_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_PriceList_IsActive");
            entity.Property(e => e.PriceListPrl).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp1).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp2).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp3).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp4).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp5).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp6).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp7).HasMaxLength(7);
            entity.Property(e => e.PriceListPrlItp8).HasMaxLength(7);
        });

        modelBuilder.Entity<Programs>(entity =>
        {
            entity.ToTable("Program");

            entity.HasIndex(e => e.ProgramCode, "UQ_Program_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Program_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Program_IsActive");
            entity.Property(e => e.Menu).HasMaxLength(30);
            entity.Property(e => e.ProgramCode).HasMaxLength(30);
            entity.Property(e => e.ProgramDescriptioncription).HasMaxLength(30);
            entity.Property(e => e.ProgramType).HasMaxLength(30);
        });

        modelBuilder.Entity<SalaryDetail>(entity =>
        {
            entity.ToTable("SalaryDetail");

            entity.Property(e => e.AccountNumber).HasMaxLength(10);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalaryDetail_CreatedOn");
            entity.Property(e => e.EmployeeNumber).HasMaxLength(5);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalaryDetail_IsActive");
            entity.Property(e => e.Month).HasMaxLength(2);
            entity.Property(e => e.OnDate).HasColumnType("datetime");

            entity.HasOne(d => d.EmployeeNumberNavigation).WithMany(p => p.SalaryDetails)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.EmployeeNumber)
                .HasConstraintName("FK_SalaryDetail_Employee");
        });

        modelBuilder.Entity<SalesCheque>(entity =>
        {
            entity.ToTable("SalesCheque");

            entity.HasIndex(e => new { e.CustomerCode, e.CustomerInvoiceNumber, e.InvoiceDate, e.TerminalCode, e.OperationCode, e.ChequeNumber }, "UQ_SalesCheque_BusinessKey").IsUnique();

            entity.Property(e => e.ChequeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeBranch).HasMaxLength(10);
            entity.Property(e => e.ChequeDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeNumber).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalesCheque_CreatedOn");
            entity.Property(e => e.CustomerCode).HasMaxLength(10);
            entity.Property(e => e.CustomerInvoiceNumber).HasMaxLength(15);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalesCheque_IsActive");
            entity.Property(e => e.OperationCode).HasMaxLength(5);
        });

        modelBuilder.Entity<SalesChequeTemporary>(entity =>
        {
            entity.ToTable("SalesChequeTemporary");

            entity.HasIndex(e => new { e.CustomerCode, e.CustomerInvoiceNumber, e.InvoiceDate, e.TerminalCode, e.OperationCode, e.ChequeNumber }, "UQ_SalesChequeTemporary_BusinessKey").IsUnique();

            entity.Property(e => e.ChequeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeBranch).HasMaxLength(10);
            entity.Property(e => e.ChequeDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeNumber).HasMaxLength(10);
            entity.Property(e => e.CustomerCode).HasMaxLength(10);
            entity.Property(e => e.CustomerInvoiceNumber).HasMaxLength(15);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.OperationCode).HasMaxLength(5);
        });

        modelBuilder.Entity<SalesRepresentativeMaster>(entity =>
        {
            entity.ToTable("SalesRepresentativeMaster");

            entity.HasIndex(e => e.SalesRepresentativeresentativeCode, "UQ_SalesRepresentativeMaster_BusinessKey").IsUnique();

            entity.Property(e => e.AreaCode).HasMaxLength(10);
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalesRepresentativeMaster_CreatedOn");
            entity.Property(e => e.CreditLimit).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalesRepresentativeMaster_IsActive");
            entity.Property(e => e.MainLocationCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.RunsWithStock).HasMaxLength(1);
            entity.Property(e => e.SalesRepresentativeresentativeAddress).HasMaxLength(80);
            entity.Property(e => e.SalesRepresentativeresentativeCode).HasMaxLength(10);
            entity.Property(e => e.SalesRepresentativeresentativeEmail).HasMaxLength(100);
            entity.Property(e => e.SalesRepresentativeresentativeFax)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesRepresentativeresentativeMobile)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesRepresentativeresentativeName).HasMaxLength(80);
            entity.Property(e => e.SalesRepresentativeresentativeTelephone).HasMaxLength(12);
        });

        modelBuilder.Entity<SalesRepresentativeStockOnHand>(entity =>
        {
            entity.ToTable("SalesRepresentativeStockOnHand");

            entity.HasIndex(e => new { e.SalesRepresentativeresentativeCode, e.StockCode }, "UQ_SalesRepresentativeStockOnHand_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalesRepresentativeStockOnHand_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalesRepresentativeStockOnHand_IsActive");
            entity.Property(e => e.ItemCode).HasMaxLength(7);
            entity.Property(e => e.ItemCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalesRepresentativeresentativeCode).HasMaxLength(10);
            entity.Property(e => e.StockCode).HasMaxLength(8);
            entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockSizeCode).HasMaxLength(6);

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.SalesRepresentativeStockOnHands)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.ItemCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesRepresentativeStockOnHand_Item");

            entity.HasOne(d => d.SalesRepresentativeresentativeCodeNavigation).WithMany(p => p.SalesRepresentativeStockOnHands)
                .HasPrincipalKey(p => p.SalesRepresentativeresentativeCode)
                .HasForeignKey(d => d.SalesRepresentativeresentativeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesRepresentativeStockOnHand_SalesRepresentativeMaster");

            entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.SalesRepresentativeStockOnHands)
                .HasPrincipalKey(p => p.StockCode)
                .HasForeignKey(d => d.StockCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesRepresentativeStockOnHand_Stock");
        });

        modelBuilder.Entity<SalesTemporary>(entity =>
        {
            entity.ToTable("SalesTemporary");

            entity.Property(e => e.CategoryCode).HasMaxLength(2);
        });

        modelBuilder.Entity<SignOn>(entity =>
        {
            entity.ToTable("SignOn");

            entity.HasIndex(e => new { e.OnDate, e.OperationCode, e.TerminalNumber }, "UQ_SignOn_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SignOn_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SignOn_IsActive");
            entity.Property(e => e.OnDate).HasColumnType("datetime");
            entity.Property(e => e.OperationCode).HasMaxLength(5);
            entity.Property(e => e.Status).HasMaxLength(1);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.HasIndex(e => e.SizeNumber, "UQ_Size_BusinessKey").IsUnique();

            entity.Property(e => e.AmendedByUserCode).HasMaxLength(10);
            entity.Property(e => e.AmendedDate).HasColumnType("datetime");
            entity.Property(e => e.AmendedTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Size_CreatedOn");
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.EntryTime).HasColumnType("datetime");
            entity.Property(e => e.EntryUserCode).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Size_IsActive");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.HasIndex(e => e.StockCode, "UQ_Stock_BusinessKey").IsUnique();

            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Stock_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Stock_IsActive");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockActiveFlag).HasMaxLength(1);
            entity.Property(e => e.StockAmendedByUserCode).HasMaxLength(20);
            entity.Property(e => e.StockAmendedDate).HasColumnType("datetime");
            entity.Property(e => e.StockAmendedTimee).HasMaxLength(12);
            entity.Property(e => e.StockCategory).HasMaxLength(4);
            entity.Property(e => e.StockCode).HasMaxLength(8);
            entity.Property(e => e.StockDepartment).HasMaxLength(10);
            entity.Property(e => e.StockDescriptioncription).HasMaxLength(40);
            entity.Property(e => e.StockDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockDiscountEnabledFlag).HasMaxLength(1);
            entity.Property(e => e.StockEntryDate).HasColumnType("datetime");
            entity.Property(e => e.StockEntryTimee).HasMaxLength(12);
            entity.Property(e => e.StockEntryUserCode).HasMaxLength(20);
            entity.Property(e => e.StockExcessQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockFamily).HasMaxLength(10);
            entity.Property(e => e.StockFlag).HasMaxLength(1);
            entity.Property(e => e.StockFreeQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockFromDate).HasColumnType("datetime");
            entity.Property(e => e.StockGoodsReceiptQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockGroupWithPriceFlag).HasMaxLength(1);
            entity.Property(e => e.StockIfscode)
                .HasMaxLength(30)
                .HasColumnName("StockIFSCode");
            entity.Property(e => e.StockIssueQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockLtd).HasColumnType("datetime");
            entity.Property(e => e.StockMaximumQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockMinimumQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockMultiItemFlag).HasMaxLength(1);
            entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockOrderedQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockPurchaseQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockReservedQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockReturnQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockSalesAdjustmentQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockSalesQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockSalesReturnQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StockSizeCode).HasMaxLength(6);
            entity.Property(e => e.StockSupplier).HasMaxLength(10);
            entity.Property(e => e.StockToDate).HasColumnType("datetime");
            entity.Property(e => e.StockTypeCode).HasMaxLength(7);
            entity.Property(e => e.StockVat).HasMaxLength(1);

            entity.HasOne(d => d.StockDepartmentNavigation).WithMany(p => p.Stocks)
                .HasPrincipalKey(p => p.DepartmentCode)
                .HasForeignKey(d => d.StockDepartment)
                .HasConstraintName("FK_Stock_Department");

            entity.HasOne(d => d.StockSupplierNavigation).WithMany(p => p.Stocks)
                .HasPrincipalKey(p => p.SupplierCode)
                .HasForeignKey(d => d.StockSupplier)
                .HasConstraintName("FK_Stock_Supplier");

            entity.HasOne(d => d.StockTypeCodeNavigation).WithMany(p => p.Stocks)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.StockTypeCode)
                .HasConstraintName("FK_Stock_Item");
        });

        modelBuilder.Entity<StockAnalysis>(entity =>
        {
            entity.ToTable("StockAnalysis");

            entity.HasIndex(e => e.ItemType, "UQ_StockAnalysis_BusinessKey").IsUnique();

            entity.Property(e => e.CloseStk).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockAnalysis_CreatedOn");
            entity.Property(e => e.Gin).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.GoodsInNoteAsDate).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.GoodsReceiptAsDate).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Grn).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockAnalysis_IsActive");
            entity.Property(e => e.ItemType).HasMaxLength(13);
            entity.Property(e => e.OpenStk).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.RetAsDate).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.SaleEsAsDate).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.SaleReturn).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.Sales).HasColumnType("numeric(10, 0)");
            entity.Property(e => e.StockAsDate).HasColumnType("numeric(10, 0)");
        });

        modelBuilder.Entity<StockDetail>(entity =>
        {
            entity.ToTable("StockDetail");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockDetail_CreatedOn");
            entity.Property(e => e.EnDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockDetail_IsActive");
            entity.Property(e => e.OpDate).HasColumnType("datetime");
            entity.Property(e => e.PhyQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PhyValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<StockOnHandSummary>(entity =>
        {
            entity.ToTable("StockOnHandSummary");

            entity.HasIndex(e => e.ItemNumber, "UQ_StockOnHandSummary_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockOnHandSummary_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockOnHandSummary_IsActive");
            entity.Property(e => e.ItemNumber).HasMaxLength(8);
        });

        modelBuilder.Entity<StockVariance>(entity =>
        {
            entity.ToTable("StockVariance");

            entity.HasIndex(e => new { e.StockCode, e.AdjDate }, "UQ_StockVariance_BusinessKey").IsUnique();

            entity.Property(e => e.AdjDate).HasColumnType("datetime");
            entity.Property(e => e.AfterStockAdjustment)
                .HasDefaultValue(0m, "DF_StockVariance_AfterStockAdjustment")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BeforeStockAdjustment)
                .HasDefaultValue(0m, "DF_StockVariance_BeforeStockAdjustment")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CostPrice)
                .HasDefaultValue(0m, "DF_StockVariance_CostPrice")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockVariance_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockVariance_IsActive");
            entity.Property(e => e.SalePrice)
                .HasDefaultValue(0m, "DF_StockVariance_SalePrice")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockCode).HasMaxLength(8);
            entity.Property(e => e.StockTypeCode).HasMaxLength(7);
            entity.Property(e => e.StockVarianceQuantity)
                .HasDefaultValue(0m, "DF_StockVariance_StockVarianceQuantity")
                .HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.StockVariances)
                .HasPrincipalKey(p => p.StockCode)
                .HasForeignKey(d => d.StockCode)
                .HasConstraintName("FK_StockVariance_Stock");
        });

        modelBuilder.Entity<StoreTransferTransaction>(entity =>
        {
            entity.ToTable("StoreTransferTransaction");

            entity.HasIndex(e => new { e.GoodsOutNoteNumber, e.GoodsInNoteNumber, e.ItemCode, e.StockCode }, "UQ_StoreTransferTransaction_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StoreTransferTransaction_CreatedOn");
            entity.Property(e => e.GoodsInNoteDate).HasColumnType("datetime");
            entity.Property(e => e.GoodsInNoteNumber).HasMaxLength(15);
            entity.Property(e => e.GoodsInNoteQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GoodsInNoteValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GoodsOutNoteDate).HasColumnType("datetime");
            entity.Property(e => e.GoodsOutNoteNumber).HasMaxLength(15);
            entity.Property(e => e.GoodsOutNoteQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GoodsOutNoteValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StoreTransferTransaction_IsActive");
            entity.Property(e => e.ItemCode).HasMaxLength(7);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockCode).HasMaxLength(8);

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.StoreTransferTransactions)
                .HasPrincipalKey(p => p.ItemCode)
                .HasForeignKey(d => d.ItemCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreTransferTransaction_Item");

            entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.StoreTransferTransactions)
                .HasPrincipalKey(p => p.StockCode)
                .HasForeignKey(d => d.StockCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreTransferTransaction_Stock");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.HasIndex(e => e.SupplierCode, "UQ_Supplier_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Supplier_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Supplier_IsActive");
            entity.Property(e => e.SupplierAdd).HasMaxLength(80);
            entity.Property(e => e.SupplierCode).HasMaxLength(10);
            entity.Property(e => e.SupplierEmail).HasMaxLength(100);
            entity.Property(e => e.SupplierFax)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SupplierMb)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SupplierMB");
            entity.Property(e => e.SupplierName).HasMaxLength(80);
            entity.Property(e => e.SupplierTp).HasMaxLength(12);
        });

        modelBuilder.Entity<Systems>(entity =>
        {
            entity.ToTable("Systems");

            entity.HasIndex(e => new { e.SystemRecordType, e.SystemRecordNumber }, "UQ_System_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_System_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_System_IsActive");
            entity.Property(e => e.SystemAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SystemAmount2).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SystemDescription).HasMaxLength(40);
            entity.Property(e => e.SystemDescription2).HasMaxLength(50);
            entity.Property(e => e.SystemFromDate).HasColumnType("datetime");
            entity.Property(e => e.SystemRecordNumber).HasMaxLength(10);
            entity.Property(e => e.SystemToDate).HasColumnType("datetime");
            entity.Property(e => e.ValueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValueAmount2).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserCode, "UQ_User_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_User_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_User_IsActive");
            entity.Property(e => e.Password).HasMaxLength(12);
            entity.Property(e => e.UserCode).HasMaxLength(20);
            entity.Property(e => e.UserGroup).HasMaxLength(30);
            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.UserStatus).HasMaxLength(1);
        });

        modelBuilder.Entity<UserGroupPermission>(entity =>
        {
            entity.ToTable("UserGroupPermission");

            entity.HasIndex(e => new { e.UserGroupName, e.FormName }, "UQ_UserGroupPermission_BusinessKey").IsUnique();

            entity.Property(e => e.Access).HasMaxLength(3);
            entity.Property(e => e.CanAdd).HasMaxLength(3);
            entity.Property(e => e.CanAmend).HasMaxLength(3);
            entity.Property(e => e.CanDelete).HasMaxLength(3);
            entity.Property(e => e.CanDisplay).HasMaxLength(3);
            entity.Property(e => e.CanEmail).HasMaxLength(3);
            entity.Property(e => e.CanPrint).HasMaxLength(3);
            entity.Property(e => e.CanSave).HasMaxLength(3);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_UserGroupPermission_CreatedOn");
            entity.Property(e => e.FormName).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_UserGroupPermission_IsActive");
            entity.Property(e => e.UserGroupName).HasMaxLength(30);
        });

        modelBuilder.Entity<VersionHeader>(entity =>
        {
            entity.ToTable("VersionHeader");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VersionHeader_CreatedOn");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VersionHeader_IsActive");
            entity.Property(e => e.Receipt).HasMaxLength(1);
            entity.Property(e => e.Sales).HasMaxLength(1);
            entity.Property(e => e.SalesReturn).HasMaxLength(1);
            entity.Property(e => e.Stock).HasMaxLength(1);
            entity.Property(e => e.Version).HasMaxLength(10);
        });

        modelBuilder.Entity<VoucherHeader>(entity =>
        {
            entity.ToTable("VoucherHeader");

            entity.HasIndex(e => e.VoucherCode, "UQ_VoucherHeader_BusinessKey").IsUnique();

            entity.Property(e => e.AccountCode).HasMaxLength(10);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherHeader_CreatedOn");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherHeader_IsActive");
            entity.Property(e => e.VoucherCode).HasMaxLength(10);
            entity.Property(e => e.VoucherDescriptioncription).HasMaxLength(35);
            entity.Property(e => e.VoucherFlag).HasMaxLength(1);
        });

        modelBuilder.Entity<VoucherInventoryHeader>(entity =>
        {
            entity.ToTable("VoucherInventoryHeader");

            entity.HasIndex(e => new { e.InventoryHeaderLocation, e.InventoryHeaderType, e.InventoryHeaderDocumentNumber, e.InventoryHeaderDate, e.InventoryHeaderOperationCode, e.TerminalNumber }, "UQ_VoucherInventoryHeader_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherInventoryHeader_CreatedOn");
            entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreditReceivedAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderAddedByUserCode).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderAddedDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderAmendedByUserCode).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderAmendedDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderCash).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderCashDisamount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderCashDISAmount");
            entity.Property(e => e.InventoryHeaderCashDiscountPc)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderCashDiscountPC");
            entity.Property(e => e.InventoryHeaderCashHt).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderCrAccNo).HasMaxLength(20);
            entity.Property(e => e.InventoryHeaderCrBnk).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderCreditBilno)
                .HasMaxLength(8)
                .HasColumnName("InventoryHeaderCreditBILNO");
            entity.Property(e => e.InventoryHeaderCreditDis).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderCreditNumber).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderCreditflg).HasMaxLength(1);
            entity.Property(e => e.InventoryHeaderCustomer).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderCustomerNam)
                .HasMaxLength(40)
                .HasColumnName("InventoryHeaderCustomerNAM");
            entity.Property(e => e.InventoryHeaderD01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderD02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderD1amt1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderD1Amt1");
            entity.Property(e => e.InventoryHeaderD2amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderD2Amount");
            entity.Property(e => e.InventoryHeaderD2amt2)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderD2Amt2");
            entity.Property(e => e.InventoryHeaderDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryHeaderDiscount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderDiscount1).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderDiscountAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderDiscountPc)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderDiscountPC");
            entity.Property(e => e.InventoryHeaderDocumentNumber).HasMaxLength(14);
            entity.Property(e => e.InventoryHeaderEtimee)
                .HasColumnType("datetime")
                .HasColumnName("InventoryHeaderETimee");
            entity.Property(e => e.InventoryHeaderExcAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderFrstk)
                .HasMaxLength(7)
                .HasColumnName("InventoryHeaderFRSTK");
            entity.Property(e => e.InventoryHeaderGrossAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderLocation).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderLocationTf)
                .HasMaxLength(6)
                .HasColumnName("InventoryHeaderLocationTF");
            entity.Property(e => e.InventoryHeaderNetAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderNetVatAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderNumberAme).HasColumnName("InventoryHeaderNumberAME");
            entity.Property(e => e.InventoryHeaderOperationCode).HasMaxLength(5);
            entity.Property(e => e.InventoryHeaderPayAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderPaytyp)
                .HasMaxLength(3)
                .HasColumnName("InventoryHeaderPAYTYP");
            entity.Property(e => e.InventoryHeaderPost)
                .HasMaxLength(1)
                .HasColumnName("InventoryHeaderPOST");
            entity.Property(e => e.InventoryHeaderPrintFlg)
                .HasMaxLength(1)
                .HasColumnName("InventoryHeaderPrintFLG");
            entity.Property(e => e.InventoryHeaderRebVou01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebVou02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderRebVou03).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderReference).HasMaxLength(20);
            entity.Property(e => e.InventoryHeaderReferenceAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderReturnQuantityAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderRgrsAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderRGrsAmount");
            entity.Property(e => e.InventoryHeaderSaleTyp)
                .HasMaxLength(6)
                .HasColumnName("InventoryHeaderSaleTYP");
            entity.Property(e => e.InventoryHeaderStimee)
                .HasColumnType("datetime")
                .HasColumnName("InventoryHeaderSTimee");
            entity.Property(e => e.InventoryHeaderSupplier).HasMaxLength(6);
            entity.Property(e => e.InventoryHeaderTostk)
                .HasMaxLength(7)
                .HasColumnName("InventoryHeaderTOSTK");
            entity.Property(e => e.InventoryHeaderTotalQuantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderTotalVod).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderTotalVodAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderType).HasMaxLength(2);
            entity.Property(e => e.InventoryHeaderV01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderV01amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderV01Amount");
            entity.Property(e => e.InventoryHeaderV02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderV02amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderV02Amount");
            entity.Property(e => e.InventoryHeaderV03).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderV03amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InventoryHeaderV03Amount");
            entity.Property(e => e.InventoryHeaderVatAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InventoryHeaderVouCat01).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVouCat02).HasMaxLength(10);
            entity.Property(e => e.InventoryHeaderVouCat03).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherInventoryHeader_IsActive");
        });

        modelBuilder.Entity<VoucherInventoryLine>(entity =>
        {
            entity.ToTable("VoucherInventoryLine");

            entity.HasIndex(e => new { e.InventoryLineLocation, e.InventoryLineType, e.InventoryLineDocumentNumber, e.InventoryLineLineNumber, e.InventoryLineDate, e.InventoryLineOperationCode, e.TerminalNumber }, "UQ_VoucherInventoryLine_BusinessKey").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherInventoryLine_CreatedOn");
            entity.Property(e => e.InventoryLineAccount).HasMaxLength(10);
            entity.Property(e => e.InventoryLineCustomer).HasMaxLength(6);
            entity.Property(e => e.InventoryLineDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryLineDescriptioncription).HasMaxLength(35);
            entity.Property(e => e.InventoryLineDiscountPc).HasColumnName("InventoryLineDiscountPC");
            entity.Property(e => e.InventoryLineDocumentNumber).HasMaxLength(14);
            entity.Property(e => e.InventoryLineLocation).HasMaxLength(6);
            entity.Property(e => e.InventoryLineLocationTf)
                .HasMaxLength(6)
                .HasColumnName("InventoryLineLocationTF");
            entity.Property(e => e.InventoryLineOperationCode).HasMaxLength(5);
            entity.Property(e => e.InventoryLineReturn).HasMaxLength(1);
            entity.Property(e => e.InventoryLineSaveflg)
                .HasMaxLength(1)
                .HasColumnName("InventoryLineSAVEFLG");
            entity.Property(e => e.InventoryLineSmn)
                .HasMaxLength(10)
                .HasColumnName("InventoryLineSMN");
            entity.Property(e => e.InventoryLineStockP).HasMaxLength(10);
            entity.Property(e => e.InventoryLineType).HasMaxLength(2);
            entity.Property(e => e.InventoryLineVno)
                .HasMaxLength(10)
                .HasColumnName("InventoryLineVNO");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherInventoryLine_IsActive");

            entity.HasOne(d => d.InventoryLineSizeNumberNavigation).WithMany(p => p.VoucherInventoryLines)
                .HasPrincipalKey(p => p.SizeNumber)
                .HasForeignKey(d => d.InventoryLineSizeNumber)
                .HasConstraintName("FK_VoucherInventoryLine_Size");

            entity.HasOne(d => d.VoucherInventoryHeader).WithMany(p => p.VoucherInventoryLines)
                .HasPrincipalKey(p => new { p.InventoryHeaderLocation, p.InventoryHeaderType, p.InventoryHeaderDocumentNumber, p.InventoryHeaderDate, p.InventoryHeaderOperationCode, p.TerminalNumber })
                .HasForeignKey(d => new { d.InventoryLineLocation, d.InventoryLineType, d.InventoryLineDocumentNumber, d.InventoryLineDate, d.InventoryLineOperationCode, d.TerminalNumber })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoucherInventoryLine_Header");
        });

        modelBuilder.Entity<VoucherTemporary>(entity =>
        {
            entity.ToTable("VoucherTemporary");

            entity.HasIndex(e => e.LineNumber, "UQ_VoucherTemporary_BusinessKey").IsUnique();

            entity.Property(e => e.AccountNumber).HasMaxLength(10);
            entity.Property(e => e.VoucherCode).HasMaxLength(10);
            entity.Property(e => e.VoucherDescriptioncription).HasMaxLength(35);
            entity.Property(e => e.VoucherItem).HasMaxLength(10);
            entity.Property(e => e.VoucherTypee).HasMaxLength(20);

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.VoucherTemporaries)
                .HasPrincipalKey(p => p.VoucherCode)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK_VoucherTemporary_VoucherHeader");
        });

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
