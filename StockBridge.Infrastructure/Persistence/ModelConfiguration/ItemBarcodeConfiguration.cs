using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemBarcodeConfiguration : IEntityTypeConfiguration<ItemBarcode>
{
    public void Configure(EntityTypeBuilder<ItemBarcode> entity)
    {
        entity.ToTable("ItemBarcode");

        entity.HasIndex(e => e.Barcode, "UQ_ItemBarcode_Barcode").IsUnique();

        entity.Property(e => e.Barcode).HasMaxLength(50);
        entity.Property(e => e.BarcodeType).HasMaxLength(30);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemBarcode_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemBarcode_IsActive");

        entity.HasOne(d => d.ItemUnitOfMeasure).WithMany(p => p.ItemBarcodes)
            .HasForeignKey(d => d.ItemUnitOfMeasureId)
            .HasConstraintName("FK_ItemBarcode_ItemUOM");
    }
}