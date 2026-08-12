using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> entity)
    {
        entity.ToTable("Designation");

        entity.HasIndex(e => e.DesignationCode, "UQ_Designation_Code").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Designation_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.DesignationCode).HasMaxLength(20);
        entity.Property(e => e.DesignationName).HasMaxLength(100);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Designation_IsActive");

        entity.HasOne(d => d.Department).WithMany(p => p.Designations)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("FK_Designation_Department");
    }
}
