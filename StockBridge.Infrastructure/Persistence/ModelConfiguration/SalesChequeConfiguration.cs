using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalesChequeConfiguration : IEntityTypeConfiguration<SalesCheque>
{
    public void Configure(EntityTypeBuilder<SalesCheque> entity)
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
    }
}
