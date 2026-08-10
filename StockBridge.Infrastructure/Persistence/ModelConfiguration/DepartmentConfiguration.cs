using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> entity)
    {
        entity.ToTable("Department");

        entity.HasIndex(e => e.DepartmentCode, "UQ_Department_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Department_CreatedOn");
        entity.Property(e => e.DepartmentCode).HasMaxLength(10);
        entity.Property(e => e.DepartmentName).HasMaxLength(30);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Department_IsActive");
    }
}
