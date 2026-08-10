using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class UserGroupPermissionConfiguration : IEntityTypeConfiguration<UserGroupPermission>
{
    public void Configure(EntityTypeBuilder<UserGroupPermission> entity)
    {
        entity.ToTable("UserGroupPermission");

        entity.HasIndex(e => new { e.UserGroupName, e.FormName }, "UQ_UserGroupPermission_BusinessKey").IsUnique();

        entity.Property(e => e.Access).HasMaxLength(3);
        entity.Property(e => e.CanAdd).HasMaxLength(3);
        entity.Property(e => e.CanAmend).HasMaxLength(3);
        entity.Property(e => e.CanDelete).HasMaxLength(3);
        entity.Property(e => e.CanDisplay).HasMaxLength(3);
        entity.Property(e => e.CanEmail).HasMaxLength(3);
        entity.Property(e => e.CanPrint).HasMaxLength(3);
        entity.Property(e => e.CanSave).HasMaxLength(3);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_UserGroupPermission_CreatedOn");
        entity.Property(e => e.FormName).HasMaxLength(30);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_UserGroupPermission_IsActive");
        entity.Property(e => e.UserGroupName).HasMaxLength(30);
    }
}
