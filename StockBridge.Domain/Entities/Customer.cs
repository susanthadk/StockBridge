using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string AreaCode { get; set; } = null!;

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public string? CustomerCity { get; set; }

    public string? CustomerTelephone { get; set; }

    public string? CustomerFax { get; set; }

    public string? CustomerMobile { get; set; }

    public string? CustomerEmail { get; set; }

    public string? MainLocationCode { get; set; }

    public int? CreditPeriod { get; set; }

    public decimal? CreditLimit { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual AreaRoute AreaCodeNavigation { get; set; } = null!;
}
