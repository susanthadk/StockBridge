using System;

namespace StockBridge.Application.DTOs.SalesRepresentatives;

public class SalesRepresentativeDto
{
    public int SalesRepresentativeId { get; set; }

    public string SalesRepresentativeresentativeCode { get; set; } = string.Empty;

    public string CompanyCode { get; set; } = string.Empty;

    public string? MainLocationCode { get; set; }

    public string? SalesRepresentativeresentativeName { get; set; }

    public string? SalesRepresentativeresentativeAddress { get; set; }

    public string? AreaCode { get; set; }

    public string? SalesRepresentativeresentativeTelephone { get; set; }

    public string? SalesRepresentativeresentativeFax { get; set; }

    public string? SalesRepresentativeresentativeMobile { get; set; }

    public string? SalesRepresentativeresentativeEmail { get; set; }

    public int? CreditPeriod { get; set; }

    public decimal? CreditLimit { get; set; }

    public string? RunsWithStock { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}