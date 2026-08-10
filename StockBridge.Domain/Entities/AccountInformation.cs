using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class AccountInformation
{
    public int AccountInformationId { get; set; }

    public string? AccountNumber { get; set; }

    public string? SubCode { get; set; }

    public string? AccountDescriptioncription { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Account? Account { get; set; }
}
