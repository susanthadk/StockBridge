using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CashBankTransactionConfiguration : IEntityTypeConfiguration<CashBankTransaction>
{
    public void Configure(EntityTypeBuilder<CashBankTransaction> entity)
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
    }
}
