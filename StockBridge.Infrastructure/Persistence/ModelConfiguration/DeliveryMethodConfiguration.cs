using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> entity)
    {
        entity.ToTable("DeliveryMethod");

        entity.HasIndex(e => e.DeliveryMethodCode, "UQ_DeliveryMethod_DeliveryMethodCode").IsUnique();

        entity.HasIndex(e => e.DeliveryMethodName, "UQ_DeliveryMethod_DeliveryMethodName").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_DeliveryMethod_CreatedOn");
        entity.Property(e => e.DeliveryMethodCode).HasMaxLength(10);
        entity.Property(e => e.DeliveryMethodName).HasMaxLength(50);
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_DeliveryMethod_IsActive");
    }
}
