using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class PaymentDetail
{
    public long PaymentDetailId { get; set; }

    public string? AccountCode { get; set; }

    public string? AccountDescriptioncription { get; set; }

    public DateTime? OnDate { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? Units { get; set; }

    public decimal? Amount { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
