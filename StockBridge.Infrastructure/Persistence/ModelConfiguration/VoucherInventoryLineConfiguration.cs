using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class VoucherInventoryLineConfiguration : IEntityTypeConfiguration<VoucherInventoryLine>
{
    public void Configure(EntityTypeBuilder<VoucherInventoryLine> entity)
    {
        entity.ToTable("VoucherInventoryLine");

        entity.HasIndex(e => new { e.InventoryLineLocation, e.InventoryLineType, e.InventoryLineDocumentNumber, e.InventoryLineLineNumber, e.InventoryLineDate, e.InventoryLineOperationCode, e.TerminalNumber }, "UQ_VoucherInventoryLine_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherInventoryLine_CreatedOn");
        entity.Property(e => e.InventoryLineAccount).HasMaxLength(10);
        entity.Property(e => e.InventoryLineCustomer).HasMaxLength(6);
        entity.Property(e => e.InventoryLineDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryLineDescriptioncription).HasMaxLength(35);
        entity.Property(e => e.InventoryLineDiscountPc).HasColumnName("InventoryLineDiscountPC");
        entity.Property(e => e.InventoryLineDocumentNumber).HasMaxLength(14);
        entity.Property(e => e.InventoryLineLocation).HasMaxLength(6);
        entity.Property(e => e.InventoryLineLocationTf)
            .HasMaxLength(6)
            .HasColumnName("InventoryLineLocationTF");
        entity.Property(e => e.InventoryLineOperationCode).HasMaxLength(5);
        entity.Property(e => e.InventoryLineReturn).HasMaxLength(1);
        entity.Property(e => e.InventoryLineSaveflg)
            .HasMaxLength(1)
            .HasColumnName("InventoryLineSAVEFLG");
        entity.Property(e => e.InventoryLineSmn)
            .HasMaxLength(10)
            .HasColumnName("InventoryLineSMN");
        entity.Property(e => e.InventoryLineStockP).HasMaxLength(10);
        entity.Property(e => e.InventoryLineType).HasMaxLength(2);
        entity.Property(e => e.InventoryLineVno)
            .HasMaxLength(10)
            .HasColumnName("InventoryLineVNO");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherInventoryLine_IsActive");

        entity.HasOne(d => d.InventoryLineSizeNumberNavigation).WithMany(p => p.VoucherInventoryLines)
            .HasPrincipalKey(p => p.SizeNumber)
            .HasForeignKey(d => d.InventoryLineSizeNumber)
            .HasConstraintName("FK_VoucherInventoryLine_Size");

        entity.HasOne(d => d.VoucherInventoryHeader).WithMany(p => p.VoucherInventoryLines)
            .HasPrincipalKey(p => new { p.InventoryHeaderLocation, p.InventoryHeaderType, p.InventoryHeaderDocumentNumber, p.InventoryHeaderDate, p.InventoryHeaderOperationCode, p.TerminalNumber })
            .HasForeignKey(d => new { d.InventoryLineLocation, d.InventoryLineType, d.InventoryLineDocumentNumber, d.InventoryLineDate, d.InventoryLineOperationCode, d.TerminalNumber })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VoucherInventoryLine_Header");
    }
}
