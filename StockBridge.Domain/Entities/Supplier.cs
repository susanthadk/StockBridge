using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierCode { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string? SupplierShortName { get; set; }

    public string? ContactPersonName { get; set; }

    public string? SupplierAddress { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryCode { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? FaxNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? Website { get; set; }

    public string? BusinessRegistrationNumber { get; set; }

    public string? TaxRegistrationNumber { get; set; }

    public int? SupplierTypeId { get; set; }

    public int? PaymentTermDays { get; set; }

    public decimal? CreditLimit { get; set; }

    public string? CurrencyCode { get; set; }

    public decimal? MinimumOrderValue { get; set; }

    public decimal? MinimumOrderQuantity { get; set; }

    public int? LeadTimeDays { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? DeliveryTerms { get; set; }

    public decimal? SupplierRating { get; set; }

    public bool IsPreferredSupplier { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual DeliveryMethod? DeliveryMethod { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual SupplierType? SupplierType { get; set; }
}
