using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class FormulaHeaderConfiguration : IEntityTypeConfiguration<FormulaHeader>
{
    public void Configure(EntityTypeBuilder<FormulaHeader> entity)
    {
        entity.ToTable("FormulaHeader");

        entity.HasIndex(e => e.FormulaNumber, "UQ_FormulaHeader_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_FormulaHeader_CreatedOn");
        entity.Property(e => e.FormulaDate).HasColumnType("datetime");
        entity.Property(e => e.FormulaNumber).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_FormulaHeader_IsActive");
    }
}
