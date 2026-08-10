using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CreditHeaderConfiguration : IEntityTypeConfiguration<CreditHeader>
{
    public void Configure(EntityTypeBuilder<CreditHeader> entity)
    {
        entity.ToTable("CreditHeader");

        entity.HasIndex(e => e.CreditCode, "UQ_CreditHeader_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_CreditHeader_CreatedOn");
        entity.Property(e => e.CreditCode).HasMaxLength(10);
        entity.Property(e => e.CreditDescriptioncription).HasMaxLength(35);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_CreditHeader_IsActive");
    }
}
