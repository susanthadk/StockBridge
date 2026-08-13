using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class OrganizationalUnitConfiguration : IEntityTypeConfiguration<OrganizationalUnit>
{
    public void Configure(EntityTypeBuilder<OrganizationalUnit> entity)
    {
        entity.HasKey(e => e.OrganizationalUnitId).HasName("PK_OrganizationalUnit_OrganizationalUnitId");

        entity.ToTable("OrganizationalUnit");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        entity.Property(e => e.OrganizationalUnitCode).HasMaxLength(10);
        entity.Property(e => e.OrganizationalUnitName).HasMaxLength(100);

        entity.HasOne(d => d.Level).WithMany(p => p.OrganizationalUnits)
            .HasForeignKey(d => d.LevelId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ParentUnit).WithMany(p => p.InverseParentUnit).HasForeignKey(d => d.ParentUnitId);
    }
}
