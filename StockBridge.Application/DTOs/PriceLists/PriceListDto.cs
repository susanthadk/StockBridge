using System;

namespace StockBridge.Application.DTOs.PriceLists;

public class PriceListDto
{
    public int PriceListId { get; set; }

    public string PriceListPrl { get; set; } = null!;

    public string? PriceListPrlItp1 { get; set; }

    public string? PriceListPrlItp2 { get; set; }

    public string? PriceListPrlItp3 { get; set; }

    public string? PriceListPrlItp4 { get; set; }

    public string? PriceListPrlItp5 { get; set; }

    public string? PriceListPrlItp6 { get; set; }

    public string? PriceListPrlItp7 { get; set; }

    public string? PriceListPrlItp8 { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}