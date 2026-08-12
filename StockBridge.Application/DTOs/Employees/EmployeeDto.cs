using System;

namespace StockBridge.Application.DTOs.Employees;

public class EmployeeDto
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

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

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}