using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> entity)
    {
        entity.ToTable("Size");

        entity.HasIndex(e => e.SizeNumber, "UQ_Size_BusinessKey").IsUnique();

        entity.Property(e => e.AmendedByUserCode).HasMaxLength(10);
        entity.Property(e => e.AmendedDate).HasColumnType("datetime");
        entity.Property(e => e.AmendedTime).HasColumnType("datetime");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Size_CreatedOn");
        entity.Property(e => e.EntryDate).HasColumnType("datetime");
        entity.Property(e => e.EntryTime).HasColumnType("datetime");
        entity.Property(e => e.EntryUserCode).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Size_IsActive");
    }
}
