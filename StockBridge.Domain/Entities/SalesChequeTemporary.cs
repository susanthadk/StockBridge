using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class SalesChequeTemporary
{
    public long SalesChequeTemporaryId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerInvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public int TerminalCode { get; set; }

    public string OperationCode { get; set; } = null!;

    public string ChequeNumber { get; set; } = null!;

    public DateTime? ChequeDate { get; set; }

    public string? ChequeBranch { get; set; }

    public decimal? ChequeAmount { get; set; }
}
