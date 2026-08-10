using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class BalanceTemporaryConfiguration : IEntityTypeConfiguration<BalanceTemporary>
{
    public void Configure(EntityTypeBuilder<BalanceTemporary> entity)
    {
        entity.ToTable("BalanceTemporary");

        entity.Property(e => e.Date).HasColumnType("datetime");
        entity.Property(e => e.Flag).HasMaxLength(50);
        entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.Text).HasMaxLength(50);
        entity.Property(e => e.Type).HasMaxLength(6);
        entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
    }
}
