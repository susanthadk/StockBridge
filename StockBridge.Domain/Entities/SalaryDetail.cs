using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class SalaryDetail
{
    public int SalaryDetailId { get; set; }

    public string? EmployeeNumber { get; set; }

    public string? AccountNumber { get; set; }

    public DateTime? OnDate { get; set; }

    public string? Month { get; set; }

    public decimal? Amount { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Employee? EmployeeNumberNavigation { get; set; }
}
