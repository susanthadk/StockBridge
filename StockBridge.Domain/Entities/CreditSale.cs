using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class CreditSale
{
    public long CreditSaleId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerInvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public int TerminalCode { get; set; }

    public string OperationCode { get; set; } = null!;

    public string CreditCode { get; set; } = null!;

    public DateTime? CreditDate { get; set; }

    public decimal? CreditPeriod { get; set; }

    public decimal? CreditAmount { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual CreditHeader CreditCodeNavigation { get; set; } = null!;
}
