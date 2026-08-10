using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CreditSaleConfiguration : IEntityTypeConfiguration<CreditSale>
{
    public void Configure(EntityTypeBuilder<CreditSale> entity)
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
    }
}
