using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class VoucherTemporaryConfiguration : IEntityTypeConfiguration<VoucherTemporary>
{
    public void Configure(EntityTypeBuilder<VoucherTemporary> entity)
    {
        entity.ToTable("VoucherTemporary");

        entity.HasIndex(e => e.LineNumber, "UQ_VoucherTemporary_BusinessKey").IsUnique();

        entity.Property(e => e.AccountNumber).HasMaxLength(10);
        entity.Property(e => e.VoucherCode).HasMaxLength(10);
        entity.Property(e => e.VoucherDescriptioncription).HasMaxLength(35);
        entity.Property(e => e.VoucherItem).HasMaxLength(10);
        entity.Property(e => e.VoucherTypee).HasMaxLength(20);

        entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.VoucherTemporaries)
            .HasPrincipalKey(p => p.VoucherCode)
            .HasForeignKey(d => d.VoucherCode)
            .HasConstraintName("FK_VoucherTemporary_VoucherHeader");
    }
}
