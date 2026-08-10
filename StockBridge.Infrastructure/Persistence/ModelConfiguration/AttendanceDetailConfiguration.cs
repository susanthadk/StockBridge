using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class AttendanceDetailConfiguration : IEntityTypeConfiguration<AttendanceDetail>
{
    public void Configure(EntityTypeBuilder<AttendanceDetail> entity)
    {
        entity.ToTable("AttendanceDetail");

        entity.HasIndex(e => new { e.EmployeeProvidentFundNumber, e.InDate }, "UQ_AttendanceDetail_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_AttendanceDetail_CreatedOn");
        entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(10);
        entity.Property(e => e.InDate).HasColumnType("datetime");
        entity.Property(e => e.InTimee).HasColumnType("datetime");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_AttendanceDetail_IsActive");
        entity.Property(e => e.LoginCode).HasMaxLength(10);
        entity.Property(e => e.OutDate).HasColumnType("datetime");
        entity.Property(e => e.OutTimee).HasColumnType("datetime");
    }
}
