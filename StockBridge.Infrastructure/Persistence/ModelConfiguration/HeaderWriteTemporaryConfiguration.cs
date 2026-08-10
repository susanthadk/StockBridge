using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class HeaderWriteTemporaryConfiguration : IEntityTypeConfiguration<HeaderWriteTemporary>
{
    public void Configure(EntityTypeBuilder<HeaderWriteTemporary> entity)
    {
        entity.ToTable("HeaderWriteTemporary");

        entity.HasIndex(e => new { e.LineNumber, e.TerminalNumber }, "UQ_HeaderWriteTemporary_BusinessKey").IsUnique();

        entity.Property(e => e.CatCode).HasMaxLength(50);
        entity.Property(e => e.DiscountGroup).HasMaxLength(50);
        entity.Property(e => e.DiscountRate).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.GpDisRate).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.GpDisTyp).HasMaxLength(255);
        entity.Property(e => e.GpPrint).HasMaxLength(255);
        entity.Property(e => e.GpPrintQuantity).HasMaxLength(255);
        entity.Property(e => e.IsDeleted).HasMaxLength(1);
        entity.Property(e => e.IsReturn).HasMaxLength(1);
        entity.Property(e => e.ItemDescriptioncription).HasMaxLength(30);
        entity.Property(e => e.ItemNumber).HasMaxLength(10);
        entity.Property(e => e.ItemType).HasMaxLength(7);
        entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SalesmanCode).HasMaxLength(5);
        entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
    }
}
