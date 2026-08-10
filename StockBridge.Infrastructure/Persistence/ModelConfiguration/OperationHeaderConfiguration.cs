using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class OperationHeaderConfiguration : IEntityTypeConfiguration<OperationHeader>
{
    public void Configure(EntityTypeBuilder<OperationHeader> entity)
    {
        entity.ToTable("OperationHeader");

        entity.HasIndex(e => new { e.OperationCode, e.OnDate, e.TerminalNumber }, "UQ_OperationHeader_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_OperationHeader_CreatedOn");
        entity.Property(e => e.EinvNo).HasColumnName("EInvNo");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_OperationHeader_IsActive");
        entity.Property(e => e.OnDate).HasColumnType("datetime");
        entity.Property(e => e.OnTimee).HasColumnType("datetime");
        entity.Property(e => e.OperationCode).HasMaxLength(5);
        entity.Property(e => e.Shift).HasMaxLength(25);
        entity.Property(e => e.SinvNo).HasColumnName("SInvNo");
    }
}
