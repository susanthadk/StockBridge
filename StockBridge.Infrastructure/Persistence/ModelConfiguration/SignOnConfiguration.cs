using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SignOnConfiguration : IEntityTypeConfiguration<SignOn>
{
    public void Configure(EntityTypeBuilder<SignOn> entity)
    {
        entity.ToTable("SignOn");

        entity.HasIndex(e => new { e.OnDate, e.OperationCode, e.TerminalNumber }, "UQ_SignOn_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SignOn_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SignOn_IsActive");
        entity.Property(e => e.OnDate).HasColumnType("datetime");
        entity.Property(e => e.OperationCode).HasMaxLength(5);
        entity.Property(e => e.Status).HasMaxLength(1);
    }
}
