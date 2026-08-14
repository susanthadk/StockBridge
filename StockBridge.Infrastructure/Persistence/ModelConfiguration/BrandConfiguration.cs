using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> entity)
    {
        entity.ToTable("Brand");

        entity.HasIndex(e => e.BrandCode, "UQ_Brand_BrandCode").IsUnique();

        entity.HasIndex(e => e.BrandName, "UQ_Brand_BrandName").IsUnique();

        entity.Property(e => e.BrandCode).HasMaxLength(30);
        entity.Property(e => e.BrandName).HasMaxLength(100);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Brand_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Brand_IsActive");
    }
}
