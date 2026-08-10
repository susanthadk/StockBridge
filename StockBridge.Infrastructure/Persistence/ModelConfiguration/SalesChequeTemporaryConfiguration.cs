using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalesChequeTemporaryConfiguration : IEntityTypeConfiguration<SalesChequeTemporary>
{
    public void Configure(EntityTypeBuilder<SalesChequeTemporary> entity)
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
    }
}
