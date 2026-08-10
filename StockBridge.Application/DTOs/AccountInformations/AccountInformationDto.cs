using System;

namespace StockBridge.Application.DTOs.AccountInformations;

public class AccountInformationDto
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
}