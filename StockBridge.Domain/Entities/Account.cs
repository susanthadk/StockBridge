using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string SubCode { get; set; } = null!;

    public string? BankDescriptioncription { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccountInformation> AccountInformations { get; set; } = new List<AccountInformation>();
}
