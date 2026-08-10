using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalesRepresentativeMasterConfiguration : IEntityTypeConfiguration<SalesRepresentativeMaster>
{
    public void Configure(EntityTypeBuilder<SalesRepresentativeMaster> entity)
    {
        entity.ToTable("SalesRepresentativeMaster");

        entity.HasIndex(e => e.SalesRepresentativeresentativeCode, "UQ_SalesRepresentativeMaster_BusinessKey").IsUnique();

        entity.Property(e => e.AreaCode).HasMaxLength(10);
        entity.Property(e => e.CompanyCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalesRepresentativeMaster_CreatedOn");
        entity.Property(e => e.CreditLimit).HasColumnType("numeric(18, 2)");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalesRepresentativeMaster_IsActive");
        entity.Property(e => e.MainLocationCode)
            .HasMaxLength(3)
            .IsUnicode(false);
        entity.Property(e => e.RunsWithStock).HasMaxLength(1);
        entity.Property(e => e.SalesRepresentativeresentativeAddress).HasMaxLength(80);
        entity.Property(e => e.SalesRepresentativeresentativeCode).HasMaxLength(10);
        entity.Property(e => e.SalesRepresentativeresentativeEmail).HasMaxLength(100);
        entity.Property(e => e.SalesRepresentativeresentativeFax)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.SalesRepresentativeresentativeMobile)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.SalesRepresentativeresentativeName).HasMaxLength(80);
        entity.Property(e => e.SalesRepresentativeresentativeTelephone).HasMaxLength(12);
    }
}
