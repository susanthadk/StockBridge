using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class AccountInformationConfiguration : IEntityTypeConfiguration<AccountInformation>
{
    public void Configure(EntityTypeBuilder<AccountInformation> entity)
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
    }
}
