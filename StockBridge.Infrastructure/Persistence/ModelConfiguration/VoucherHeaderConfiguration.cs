using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class VoucherHeaderConfiguration : IEntityTypeConfiguration<VoucherHeader>
{
    public void Configure(EntityTypeBuilder<VoucherHeader> entity)
    {
        entity.ToTable("VoucherHeader");

        entity.HasIndex(e => e.VoucherCode, "UQ_VoucherHeader_BusinessKey").IsUnique();

        entity.Property(e => e.AccountCode).HasMaxLength(10);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherHeader_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherHeader_IsActive");
        entity.Property(e => e.VoucherCode).HasMaxLength(10);
        entity.Property(e => e.VoucherDescriptioncription).HasMaxLength(35);
        entity.Property(e => e.VoucherFlag).HasMaxLength(1);
    }
}
