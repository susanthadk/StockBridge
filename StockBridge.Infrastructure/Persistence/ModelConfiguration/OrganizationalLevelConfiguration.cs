using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class OrganizationalLevelConfiguration : IEntityTypeConfiguration<OrganizationalLevel>
{
    public void Configure(EntityTypeBuilder<OrganizationalLevel> entity)
    {
        entity.HasKey(e => e.LevelId).HasName("PK_OrganizationalLevel_LevelId");

        entity.ToTable("OrganizationalLevel");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())", "DF_OrganizationalLevel_CreatedOn");
        entity.Property(e => e.OrganizationLevel).HasMaxLength(100);
    }
}
