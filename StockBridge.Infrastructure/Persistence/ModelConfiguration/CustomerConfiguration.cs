using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("Customer");

        entity.HasIndex(e => e.CustomerCode, "UQ_Customer_BusinessKey").IsUnique();

        entity.Property(e => e.AreaCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Customer_CreatedOn");
        entity.Property(e => e.CreditLimit).HasColumnType("numeric(18, 2)");
        entity.Property(e => e.CustomerAddress)
            .HasMaxLength(80)
            .IsUnicode(false);
        entity.Property(e => e.CustomerCity)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.CustomerCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.CustomerEmail)
            .HasMaxLength(100)
            .IsUnicode(false);
        entity.Property(e => e.CustomerFax)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.CustomerMobile)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.CustomerName)
            .HasMaxLength(30)
            .IsUnicode(false);
        entity.Property(e => e.CustomerTelephone)
            .HasMaxLength(12)
            .IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Customer_IsActive");
        entity.Property(e => e.MainLocationCode)
            .HasMaxLength(3)
            .IsUnicode(false);

        entity.HasOne(d => d.AreaCodeNavigation).WithMany(p => p.Customers)
            .HasPrincipalKey(p => p.AreaCode)
            .HasForeignKey(d => d.AreaCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Customer_AreaRoute");
    }
}
