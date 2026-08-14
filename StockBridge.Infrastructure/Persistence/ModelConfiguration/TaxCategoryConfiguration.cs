using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class TaxCategoryConfiguration : IEntityTypeConfiguration<TaxCategory>
{
    public void Configure(EntityTypeBuilder<TaxCategory> entity)
    {
        entity.ToTable("TaxCategory");

        entity.HasIndex(e => e.TaxCategoryCode, "UQ_TaxCategory_Code").IsUnique();

        entity.HasIndex(e => e.TaxCategoryName, "UQ_TaxCategory_Name").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_TaxCategory_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_TaxCategory_IsActive");
        entity.Property(e => e.IsTaxable).HasDefaultValue(true, "DF_TaxCategory_IsTaxable");
        entity.Property(e => e.TaxCategoryCode).HasMaxLength(30);
        entity.Property(e => e.TaxCategoryName).HasMaxLength(100);
    }
}
