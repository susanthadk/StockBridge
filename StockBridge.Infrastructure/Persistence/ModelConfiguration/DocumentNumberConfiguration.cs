using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class DocumentNumberConfiguration : IEntityTypeConfiguration<DocumentNumber>
{
    public void Configure(EntityTypeBuilder<DocumentNumber> entity)
    {
        entity.ToTable("DocumentNumber");

        entity.HasIndex(e => new { e.MainLocationCode, e.StationId, e.DocumentType }, "UQ_DocumentNumber_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_DocumentNumber_CreatedOn");
        entity.Property(e => e.DocumentNumber1).HasColumnName("DocumentNumber");
        entity.Property(e => e.DocumentType)
            .HasMaxLength(3)
            .IsUnicode(false);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_DocumentNumber_IsActive");
        entity.Property(e => e.MainLocationCode)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.NumberId)
            .HasMaxLength(3)
            .IsUnicode(false);
        entity.Property(e => e.StationId)
            .HasMaxLength(3)
            .IsUnicode(false);

        entity.HasOne(d => d.MainLocationCodeNavigation).WithMany(p => p.DocumentNumbers)
            .HasPrincipalKey(p => p.MainLocCode)
            .HasForeignKey(d => d.MainLocationCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DocumentNumber_MainLocation");
    }
}
