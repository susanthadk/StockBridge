using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SupplierTypeConfiguration : IEntityTypeConfiguration<SupplierType>
{
    public void Configure(EntityTypeBuilder<SupplierType> entity)
    {
        entity.ToTable("SupplierType");

        entity.HasIndex(e => e.SupplierTypeCode, "UQ_SupplierType_SupplierTypeCode").IsUnique();

        entity.HasIndex(e => e.SupplierTypeName, "UQ_SupplierType_SupplierTypeName").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SupplierType_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SupplierType_IsActive");
        entity.Property(e => e.SupplierTypeCode).HasMaxLength(10);
        entity.Property(e => e.SupplierTypeName).HasMaxLength(50);
    }
}
