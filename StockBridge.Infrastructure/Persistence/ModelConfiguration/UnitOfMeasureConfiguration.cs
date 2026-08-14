using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> entity)
    {
        entity.ToTable("UnitOfMeasure");

        entity.HasIndex(e => e.UnitCode, "UQ_UnitOfMeasure_Code").IsUnique();

        entity.HasIndex(e => e.UnitName, "UQ_UnitOfMeasure_Name").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_UnitOfMeasure_CreatedOn");
        entity.Property(e => e.DecimalPlaces).HasDefaultValue((byte)3, "DF_UnitOfMeasure_DecimalPlaces");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_UnitOfMeasure_IsActive");
        entity.Property(e => e.UnitCategory).HasMaxLength(30);
        entity.Property(e => e.UnitCode).HasMaxLength(20);
        entity.Property(e => e.UnitName).HasMaxLength(50);
    }
}
