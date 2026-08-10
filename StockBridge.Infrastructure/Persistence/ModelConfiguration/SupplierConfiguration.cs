using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> entity)
    {
        entity.ToTable("Supplier");

        entity.HasIndex(e => e.SupplierCode, "UQ_Supplier_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Supplier_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Supplier_IsActive");
        entity.Property(e => e.SupplierAdd).HasMaxLength(80);
        entity.Property(e => e.SupplierCode).HasMaxLength(10);
        entity.Property(e => e.SupplierEmail).HasMaxLength(100);
        entity.Property(e => e.SupplierFax)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.SupplierMb)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("SupplierMB");
        entity.Property(e => e.SupplierName).HasMaxLength(80);
        entity.Property(e => e.SupplierTp).HasMaxLength(12);
    }
}
