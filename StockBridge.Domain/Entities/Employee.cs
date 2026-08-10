using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? IdentificationNumber { get; set; }

    public string? EmployeeStatus { get; set; }

    public string? EmployeeProvidentFundNumber { get; set; }

    public decimal? CommissionRate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<SalaryDetail> SalaryDetails { get; set; } = new List<SalaryDetail>();
}
