using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.ToTable("Employee");

        entity.HasIndex(e => e.EmployeeCode, "UQ_Employee_EmployeeCode").IsUnique();

        entity.Property(e => e.AddressLine1).HasMaxLength(150);
        entity.Property(e => e.AddressLine2).HasMaxLength(150);
        entity.Property(e => e.City).HasMaxLength(80);
        entity.Property(e => e.CountryCode).HasMaxLength(10);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Employee_CreatedOn");
        entity.Property(e => e.EmailAddress).HasMaxLength(150);
        entity.Property(e => e.EmployeeCode).HasMaxLength(20);
        entity.Property(e => e.FirstName).HasMaxLength(80);
        entity.Property(e => e.IdentificationNumber).HasMaxLength(50);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Employee_IsActive");
        entity.Property(e => e.LastName).HasMaxLength(80);
        entity.Property(e => e.MiddleName).HasMaxLength(80);
        entity.Property(e => e.MobileNumber).HasMaxLength(30);
        entity.Property(e => e.PostalCode).HasMaxLength(20);
        entity.Property(e => e.PreferredName).HasMaxLength(80);
        entity.Property(e => e.TelephoneNumber).HasMaxLength(30);

        entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
            .HasForeignKey(d => d.DesignationId)
            .HasConstraintName("FK_Employee_Designation");

        entity.HasOne(d => d.IdentificationType).WithMany(p => p.Employees)
            .HasForeignKey(d => d.IdentificationTypeId)
            .HasConstraintName("FK_Employee_IdentificationType");

        entity.HasOne(d => d.ManagerEmployee).WithMany(p => p.InverseManagerEmployee)
            .HasForeignKey(d => d.ManagerEmployeeId)
            .HasConstraintName("FK_Employee_ManagerEmployee");
    }
}
