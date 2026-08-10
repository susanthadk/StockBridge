using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.ToTable("Employee");

        entity.HasIndex(e => e.EmployeeCode, "UQ_Employee_BusinessKey").IsUnique();

        entity.Property(e => e.AddressLine1).HasMaxLength(25);
        entity.Property(e => e.AddressLine2).HasMaxLength(25);
        entity.Property(e => e.CommissionRate)
            .HasDefaultValue(0m, "DF_Employee_CommissionRate")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Employee_CreatedOn");
        entity.Property(e => e.EmployeeCode).HasMaxLength(5);
        entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(10);
        entity.Property(e => e.EmployeeStatus).HasMaxLength(1);
        entity.Property(e => e.FirstName).HasMaxLength(30);
        entity.Property(e => e.IdentificationNumber).HasMaxLength(12);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Employee_IsActive");
        entity.Property(e => e.LastName).HasMaxLength(20);
        entity.Property(e => e.TelephoneNumber).HasMaxLength(12);
    }
}
