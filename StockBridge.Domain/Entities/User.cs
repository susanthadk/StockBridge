using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserCode { get; set; } = null!;

    public string? UserName { get; set; }

    public string? UserGroup { get; set; }

    public string? Password { get; set; }

    public string? UserStatus { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
