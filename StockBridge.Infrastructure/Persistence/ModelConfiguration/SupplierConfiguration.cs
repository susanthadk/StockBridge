using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> entity)
    {
        entity.ToTable("Supplier");

        entity.HasIndex(e => e.SupplierCode, "UQ_Supplier_SupplierCode").IsUnique();

        entity.Property(e => e.BusinessRegistrationNumber).HasMaxLength(50);
        entity.Property(e => e.City).HasMaxLength(80);
        entity.Property(e => e.ContactPersonName).HasMaxLength(100);
        entity.Property(e => e.CountryCode).HasMaxLength(10);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Supplier_CreatedOn");
        entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CurrencyCode)
            .HasMaxLength(3)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.DeliveryTerms).HasMaxLength(100);
        entity.Property(e => e.EmailAddress).HasMaxLength(150);
        entity.Property(e => e.FaxNumber).HasMaxLength(30);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Supplier_IsActive");
        entity.Property(e => e.MinimumOrderQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.MinimumOrderValue).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.MobileNumber).HasMaxLength(30);
        entity.Property(e => e.PostalCode).HasMaxLength(20);
        entity.Property(e => e.SupplierAddress).HasMaxLength(250);
        entity.Property(e => e.SupplierCode).HasMaxLength(20);
        entity.Property(e => e.SupplierName).HasMaxLength(150);
        entity.Property(e => e.SupplierRating).HasColumnType("decimal(5, 2)");
        entity.Property(e => e.SupplierShortName).HasMaxLength(50);
        entity.Property(e => e.TaxRegistrationNumber).HasMaxLength(50);
        entity.Property(e => e.TelephoneNumber).HasMaxLength(30);
        entity.Property(e => e.Website).HasMaxLength(200);

        entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Suppliers)
            .HasForeignKey(d => d.DeliveryMethodId)
            .HasConstraintName("FK_Supplier_DeliveryMethod");

        entity.HasOne(d => d.SupplierType).WithMany(p => p.Suppliers)
            .HasForeignKey(d => d.SupplierTypeId)
            .HasConstraintName("FK_Supplier_SupplierType");
    }
}
