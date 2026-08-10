using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalaryDetailConfiguration : IEntityTypeConfiguration<SalaryDetail>
{
    public void Configure(EntityTypeBuilder<SalaryDetail> entity)
    {
        entity.ToTable("SalaryDetail");

        entity.Property(e => e.AccountNumber).HasMaxLength(10);
        entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalaryDetail_CreatedOn");
        entity.Property(e => e.EmployeeNumber).HasMaxLength(5);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalaryDetail_IsActive");
        entity.Property(e => e.Month).HasMaxLength(2);
        entity.Property(e => e.OnDate).HasColumnType("datetime");

        entity.HasOne(d => d.EmployeeNumberNavigation).WithMany(p => p.SalaryDetails)
            .HasPrincipalKey(p => p.EmployeeCode)
            .HasForeignKey(d => d.EmployeeNumber)
            .HasConstraintName("FK_SalaryDetail_Employee");
    }
}
