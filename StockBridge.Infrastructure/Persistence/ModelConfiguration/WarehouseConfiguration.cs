using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> entity)
    {
        entity.ToTable("Warehouse");

        entity.HasIndex(e => e.WarehouseCode, "UQ_Warehouse_WarehouseCode").IsUnique();

        entity.Property(e => e.AddressLine1).HasMaxLength(150);
        entity.Property(e => e.AddressLine2).HasMaxLength(150);
        entity.Property(e => e.City).HasMaxLength(80);
        entity.Property(e => e.CountryCode).HasMaxLength(10);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Warehouse_CreatedOn");
        entity.Property(e => e.EmailAddress).HasMaxLength(150);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Warehouse_IsActive");
        entity.Property(e => e.MobileNumber).HasMaxLength(30);
        entity.Property(e => e.PostalCode).HasMaxLength(20);
        entity.Property(e => e.TelephoneNumber).HasMaxLength(30);
        entity.Property(e => e.WarehouseCode).HasMaxLength(20);
        entity.Property(e => e.WarehouseName).HasMaxLength(100);

        entity.HasOne(d => d.WarehouseType).WithMany(p => p.Warehouses)
            .HasForeignKey(d => d.WarehouseTypeId)
            .HasConstraintName("FK_Warehouse_WarehouseType");
    }
}