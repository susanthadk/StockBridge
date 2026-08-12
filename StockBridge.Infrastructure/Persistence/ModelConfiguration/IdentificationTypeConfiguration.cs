using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
{
    public void Configure(EntityTypeBuilder<IdentificationType> entity)
    {
        entity.ToTable("IdentificationType");

        entity.HasIndex(e => e.IdentificationTypeCode, "UQ_IdentificationType_Code").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IdentificationTypeCode).HasMaxLength(20);
        entity.Property(e => e.IdentificationTypeName).HasMaxLength(50);
        entity.Property(e => e.IsActive).HasDefaultValue(true);
    }
}
