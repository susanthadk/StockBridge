using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("User");

        entity.HasIndex(e => e.UserCode, "UQ_User_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_User_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_User_IsActive");
        entity.Property(e => e.Password).HasMaxLength(12);
        entity.Property(e => e.UserCode).HasMaxLength(20);
        entity.Property(e => e.UserGroup).HasMaxLength(30);
        entity.Property(e => e.UserName).HasMaxLength(20);
        entity.Property(e => e.UserStatus).HasMaxLength(1);
    }
}
