using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class OperationHeader
{
    public int OperationHeaderId { get; set; }

    public string OperationCode { get; set; } = null!;

    public DateTime OnDate { get; set; }

    public int TerminalNumber { get; set; }

    public DateTime? OnTimee { get; set; }

    public double? SignOff { get; set; }

    public string? Shift { get; set; }

    public double? SinvNo { get; set; }

    public double? EinvNo { get; set; }

    public double? CashAc { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
