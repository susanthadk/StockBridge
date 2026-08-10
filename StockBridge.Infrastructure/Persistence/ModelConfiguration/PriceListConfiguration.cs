using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
{
    public void Configure(EntityTypeBuilder<PriceList> entity)
    {
        entity.ToTable("PriceList");

        entity.HasIndex(e => e.PriceListPrl, "UQ_PriceList_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_PriceList_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_PriceList_IsActive");
        entity.Property(e => e.PriceListPrl).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp1).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp2).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp3).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp4).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp5).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp6).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp7).HasMaxLength(7);
        entity.Property(e => e.PriceListPrlItp8).HasMaxLength(7);
    }
}
