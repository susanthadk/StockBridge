using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class BarcodeLabelDatumConfiguration : IEntityTypeConfiguration<BarcodeLabelDatum>
{
    public void Configure(EntityTypeBuilder<BarcodeLabelDatum> entity)
    {
        entity.HasKey(e => e.BarcodeLabelDataId);

        entity.Property(e => e.InitialCreate).HasMaxLength(10);
        entity.Property(e => e.ItemCode)
            .HasMaxLength(13)
            .IsUnicode(false);
        entity.Property(e => e.SellingPrice).HasColumnType("numeric(18, 2)");
        entity.Property(e => e.SellingPriceDescriptioncription)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.StockCode)
            .HasMaxLength(14)
            .IsUnicode(false);
        entity.Property(e => e.StockDescriptioncription)
            .HasMaxLength(45)
            .IsUnicode(false);
    }
}
