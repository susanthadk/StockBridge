using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> entity)
    {
        entity.ToTable("Account");

        entity.HasIndex(e => new { e.AccountNumber, e.SubCode }, "UQ_Account_BusinessKey").IsUnique();

        entity.Property(e => e.AccountNumber).HasMaxLength(10);
        entity.Property(e => e.BankDescriptioncription).HasMaxLength(30);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Account_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Account_IsActive");
        entity.Property(e => e.SubCode).HasMaxLength(10);
    }
}
