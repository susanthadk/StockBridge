using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class CreditSaleSummary
{
    public long CreditSaleSummaryId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public string SalesRepresentativeresentativeCode { get; set; } = null!;

    public string CompanyCode { get; set; } = null!;

    public string CustomerCode { get; set; } = null!;

    public decimal? InvoiceGrossAmount { get; set; }

    public decimal? InvoiceNetAmount { get; set; }

    public decimal? InvoiceItemDiscount { get; set; }

    public decimal? InvoiceSpecialDiscount { get; set; }

    public decimal? InvoiceCashDiscount { get; set; }

    public decimal? InvoiceAmountDiscount { get; set; }

    public decimal? InvoiceCashReceived { get; set; }

    public decimal? InvoiceChequeAmount { get; set; }

    public decimal? InvoiceBalancePayment { get; set; }

    public decimal? InvoiceCashDiscountRate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual SalesRepresentativeMaster SalesRepresentativeresentativeCodeNavigation { get; set; } = null!;
}
