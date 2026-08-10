using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> entity)
    {
        entity.ToTable("Company");

        entity.HasIndex(e => e.LocationNumber, "UQ_Company_BusinessKey").IsUnique();

        entity.Property(e => e.ConditionMsg)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Company_CreatedOn");
        entity.Property(e => e.Email)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Company_IsActive");
        entity.Property(e => e.LocationNumber).HasMaxLength(5);
        entity.Property(e => e.Logo1).HasMaxLength(20);
        entity.Property(e => e.Logo2).HasMaxLength(20);
        entity.Property(e => e.Logo3).HasMaxLength(40);
        entity.Property(e => e.Logo4).HasMaxLength(40);
        entity.Property(e => e.Logo5).HasMaxLength(40);
        entity.Property(e => e.Mess1).HasMaxLength(16);
        entity.Property(e => e.Mess2).HasMaxLength(16);
        entity.Property(e => e.Mess3).HasColumnType("ntext");
    }
}
