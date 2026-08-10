using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class UserGroupPermission
{
    public int UserGroupPermissionId { get; set; }

    public string UserGroupName { get; set; } = null!;

    public string FormName { get; set; } = null!;

    public string? Access { get; set; }

    public string? CanAdd { get; set; }

    public string? CanAmend { get; set; }

    public string? CanSave { get; set; }

    public string? CanDelete { get; set; }

    public string? CanDisplay { get; set; }

    public string? CanPrint { get; set; }

    public string? CanEmail { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
