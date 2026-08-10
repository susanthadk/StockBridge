using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class CashBankTransaction
{
    public long CashBankTransactionId { get; set; }

    public string BankCode { get; set; } = null!;

    public string OperationCode { get; set; } = null!;

    public int TerminalNumber { get; set; }

    public string VisitCode { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? BalanceDate { get; set; }

    public decimal? DepositAmount { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
