using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class SignOn
{
    public int SignOnId { get; set; }

    public DateTime OnDate { get; set; }

    public double? InvNo { get; set; }

    public string OperationCode { get; set; } = null!;

    public int TerminalNumber { get; set; }

    public double? SignOff { get; set; }

    public string? Status { get; set; }

    public double? VoucherInv { get; set; }

    public double? RebNo { get; set; }

    public double? SalesRepresentativeresentativeNumber { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
