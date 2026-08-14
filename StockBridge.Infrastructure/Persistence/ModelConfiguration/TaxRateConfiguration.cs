using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class TaxRateConfiguration : IEntityTypeConfiguration<TaxRate>
{
    public void Configure(EntityTypeBuilder<TaxRate> entity)
    {
        entity.ToTable("TaxRate");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_TaxRate_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_TaxRate_IsActive");
        entity.Property(e => e.TaxRatePercentage).HasColumnType("decimal(9, 4)");

        entity.HasOne(d => d.TaxCategory).WithMany(p => p.TaxRates)
            .HasForeignKey(d => d.TaxCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TaxRate_TaxCategory");
    }
}
