using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PreferredName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? Gender { get; set; }

    public string? MobileNumber { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryCode { get; set; }

    public int? IdentificationTypeId { get; set; }

    public string? IdentificationNumber { get; set; }

    public int? DesignationId { get; set; }

    public int? ManagerEmployeeId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual IdentificationType? IdentificationType { get; set; }

    public virtual ICollection<Employee> InverseManagerEmployee { get; set; } = new List<Employee>();

    public virtual Employee? ManagerEmployee { get; set; }

    public virtual ICollection<SalaryDetail> SalaryDetails { get; set; } = new List<SalaryDetail>();
}
