using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ProductHierarchyLevelConfiguration : IEntityTypeConfiguration<ProductHierarchyLevel>
{
    public void Configure(EntityTypeBuilder<ProductHierarchyLevel> entity)
    {
        entity.ToTable("ProductHierarchyLevel");

        entity.HasIndex(e => e.LevelCode, "UQ_ProductHierarchyLevel_LevelCode").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ProductHierarchyLevel_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ProductHierarchyLevel_IsActive");
        entity.Property(e => e.LevelCode).HasMaxLength(30);
        entity.Property(e => e.LevelName).HasMaxLength(100);
    }
}
