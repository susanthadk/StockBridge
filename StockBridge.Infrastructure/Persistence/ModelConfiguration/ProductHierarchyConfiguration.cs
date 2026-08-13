using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ProductHierarchyConfiguration : IEntityTypeConfiguration<ProductHierarchy>
{
    public void Configure(EntityTypeBuilder<ProductHierarchy> entity)
    {
        entity.ToTable("ProductHierarchy");

        entity.HasIndex(e => new { e.ProductHierarchyLevelId, e.ProductHierarchyCode }, "UQ_ProductHierarchy_Level_Code").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ProductHierarchy_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(500);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ProductHierarchy_IsActive");
        entity.Property(e => e.ProductHierarchyCode).HasMaxLength(30);
        entity.Property(e => e.ProductHierarchyName).HasMaxLength(150);

        entity.HasOne(d => d.ParentProductHierarchy).WithMany(p => p.InverseParentProductHierarchy)
            .HasForeignKey(d => d.ParentProductHierarchyId)
            .HasConstraintName("FK_ProductHierarchy_Parent");

        entity.HasOne(d => d.ProductHierarchyLevel).WithMany(p => p.ProductHierarchies)
            .HasForeignKey(d => d.ProductHierarchyLevelId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductHierarchy_Level");
    }
}
