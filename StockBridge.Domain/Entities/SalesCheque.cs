using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class SalesCheque
{
    public long SalesChequeId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerInvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public int TerminalCode { get; set; }

    public string OperationCode { get; set; } = null!;

    public string ChequeNumber { get; set; } = null!;

    public DateTime? ChequeDate { get; set; }

    public string? ChequeBranch { get; set; }

    public decimal? ChequeAmount { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
