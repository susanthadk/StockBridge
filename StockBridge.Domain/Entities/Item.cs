using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public string? ShortName { get; set; }

    public int? ProductHierarchyId { get; set; }

    public int? BrandId { get; set; }

    public int BaseUnitOfMeasureId { get; set; }

    public int ItemTypeId { get; set; }

    public int? TaxCategoryId { get; set; }

    public bool IsStockItem { get; set; }

    public bool IsPurchaseItem { get; set; }

    public bool IsSaleItem { get; set; }

    public bool IsBatchControlled { get; set; }

    public bool IsExpiryControlled { get; set; }

    public bool IsSerialControlled { get; set; }

    public decimal? NetWeight { get; set; }

    public decimal? GrossWeight { get; set; }

    public decimal? Length { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual UnitOfMeasure BaseUnitOfMeasure { get; set; } = null!;

    public virtual Brand? Brand { get; set; }

    public virtual ItemType ItemType { get; set; } = null!;

    public virtual ProductHierarchy? ProductHierarchy { get; set; }

    public virtual TaxCategory? TaxCategory { get; set; }
}
