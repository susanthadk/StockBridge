using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalesTemporaryConfiguration : IEntityTypeConfiguration<SalesTemporary>
{
    public void Configure(EntityTypeBuilder<SalesTemporary> entity)
    {
        entity.ToTable("SalesTemporary");

        entity.Property(e => e.CategoryCode).HasMaxLength(2);
    }
}
