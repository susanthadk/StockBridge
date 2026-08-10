using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("Category");

        entity.HasIndex(e => new { e.CategoryCode, e.DepartmentCode }, "UQ_Category_BusinessKey").IsUnique();

        entity.Property(e => e.CategoryCode).HasMaxLength(10);
        entity.Property(e => e.CategoryName).HasMaxLength(40);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Category_CreatedOn");
        entity.Property(e => e.DepartmentCode).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Category_IsActive");

        entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Categories)
            .HasPrincipalKey(p => p.DepartmentCode)
            .HasForeignKey(d => d.DepartmentCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Category_Department");
    }
}
