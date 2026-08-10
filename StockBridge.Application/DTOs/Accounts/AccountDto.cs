using System;

namespace StockBridge.Application.DTOs.Accounts;

public class AccountDto
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string SubCode { get; set; } = string.Empty;

    public string? BankDescriptioncription { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}