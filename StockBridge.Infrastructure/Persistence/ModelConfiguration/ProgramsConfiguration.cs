using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ProgramsConfiguration : IEntityTypeConfiguration<Programs>
{
    public void Configure(EntityTypeBuilder<Programs> entity)
    {
        entity.ToTable("Program");

        entity.HasKey(e => e.ProgramId);

        entity.HasIndex(e => e.ProgramCode, "UQ_Program_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Program_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Program_IsActive");
        entity.Property(e => e.Menu).HasMaxLength(30);
        entity.Property(e => e.ProgramCode).HasMaxLength(30);
        entity.Property(e => e.ProgramDescriptioncription).HasMaxLength(30);
        entity.Property(e => e.ProgramType).HasMaxLength(30);
    }
}
