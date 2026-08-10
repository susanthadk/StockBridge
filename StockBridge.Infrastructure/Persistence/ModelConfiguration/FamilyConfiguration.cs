using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> entity)
    {
        entity.ToTable("Family");

        entity.HasIndex(e => new { e.FamilyCode, e.CategoryCode, e.DepartmentCode }, "UQ_Family_BusinessKey").IsUnique();

        entity.Property(e => e.CategoryCode).HasMaxLength(10);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Family_CreatedOn");
        entity.Property(e => e.DepartmentCode).HasMaxLength(10);
        entity.Property(e => e.FamilyCode).HasMaxLength(10);
        entity.Property(e => e.FamilyName).HasMaxLength(40);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Family_IsActive");

        entity.HasOne(d => d.Category).WithMany(p => p.Families)
            .HasPrincipalKey(p => new { p.CategoryCode, p.DepartmentCode })
            .HasForeignKey(d => new { d.CategoryCode, d.DepartmentCode })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Family_Category");
    }
}
