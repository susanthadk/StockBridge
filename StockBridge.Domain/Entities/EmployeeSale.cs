using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class EmployeeSale
{
    public long EmployeeSalesId { get; set; }

    public DateTime SaleDate { get; set; }

    public string EmployeeProvidentFundNumber { get; set; } = null!;

    public decimal? SaleQuantity { get; set; }

    public decimal? SaleValue { get; set; }

    public decimal? SaleAmendedQuantity { get; set; }

    public decimal? SaleAmendedValue { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
