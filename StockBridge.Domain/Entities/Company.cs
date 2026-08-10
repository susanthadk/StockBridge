using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Company
{
    public int CompanyId { get; set; }

    public string LocationNumber { get; set; } = null!;

    public string? Logo1 { get; set; }

    public string? Logo2 { get; set; }

    public string? Logo3 { get; set; }

    public string? Logo4 { get; set; }

    public string? Logo5 { get; set; }

    public string? Mess1 { get; set; }

    public string? Mess2 { get; set; }

    public string? Mess3 { get; set; }

    public string? ConditionMsg { get; set; }

    public double? Vat { get; set; }

    public string? Email { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
