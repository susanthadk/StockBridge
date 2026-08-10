using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class PaymentDetailConfiguration : IEntityTypeConfiguration<PaymentDetail>
{
    public void Configure(EntityTypeBuilder<PaymentDetail> entity)
    {
        entity.ToTable("PaymentDetail");

        entity.Property(e => e.AccountCode).HasMaxLength(10);
        entity.Property(e => e.AccountDescriptioncription).HasMaxLength(30);
        entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_PaymentDetail_CreatedOn");
        entity.Property(e => e.FromDate).HasColumnType("datetime");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_PaymentDetail_IsActive");
        entity.Property(e => e.OnDate).HasColumnType("datetime");
        entity.Property(e => e.ToDate).HasColumnType("datetime");
        entity.Property(e => e.Units).HasColumnType("decimal(18, 2)");
    }
}
