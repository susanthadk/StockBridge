using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class FormulaLineConfiguration : IEntityTypeConfiguration<FormulaLine>
{
    public void Configure(EntityTypeBuilder<FormulaLine> entity)
    {
        entity.ToTable("FormulaLine");

        entity.HasIndex(e => new { e.FormulaNumber, e.ItemNumber }, "UQ_FormulaLine_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_FormulaLine_CreatedOn");
        entity.Property(e => e.FormulaNumber).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_FormulaLine_IsActive");
        entity.Property(e => e.ItemNumber).HasMaxLength(7);

        entity.HasOne(d => d.FormulaNumberNavigation).WithMany(p => p.FormulaLines)
            .HasPrincipalKey(p => p.FormulaNumber)
            .HasForeignKey(d => d.FormulaNumber)
            .HasConstraintName("FK_FormulaLine_FormulaHeader");

        entity.HasOne(d => d.ItemNumberNavigation).WithMany(p => p.FormulaLines)
            .HasPrincipalKey(p => p.ItemCode)
            .HasForeignKey(d => d.ItemNumber)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_FormulaLine_Item");
    }
}
