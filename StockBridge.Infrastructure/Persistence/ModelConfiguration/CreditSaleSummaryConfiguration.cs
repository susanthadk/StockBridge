using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CreditSaleSummaryConfiguration : IEntityTypeConfiguration<CreditSaleSummary>
{
    public void Configure(EntityTypeBuilder<CreditSaleSummary> entity)
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
    }
}
