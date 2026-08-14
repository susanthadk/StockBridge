using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class WarehouseTypeConfiguration : IEntityTypeConfiguration<WarehouseType>
{
    public void Configure(EntityTypeBuilder<WarehouseType> entity)
    {
        entity.ToTable("WarehouseType");

        entity.HasIndex(e => e.WarehouseTypeCode, "UQ_WarehouseType_Code").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_WarehouseType_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_WarehouseType_IsActive");
        entity.Property(e => e.WarehouseTypeCode).HasMaxLength(20);
        entity.Property(e => e.WarehouseTypeName).HasMaxLength(100);
    }
}
