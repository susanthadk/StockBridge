using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class EmployeeSaleConfiguration : IEntityTypeConfiguration<EmployeeSale>
{
    public void Configure(EntityTypeBuilder<EmployeeSale> entity)
    {
        entity.HasKey(e => e.EmployeeSalesId);

        entity.HasIndex(e => new { e.SaleDate, e.EmployeeProvidentFundNumber }, "UQ_EmployeeSales_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_EmployeeSales_CreatedOn");
        entity.Property(e => e.EmployeeProvidentFundNumber).HasMaxLength(50);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_EmployeeSales_IsActive");
        entity.Property(e => e.SaleAmendedQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SaleAmendedValue).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SaleDate).HasColumnType("datetime");
        entity.Property(e => e.SaleQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SaleValue).HasColumnType("decimal(18, 2)");
    }
}
