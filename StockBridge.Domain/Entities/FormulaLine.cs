using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class FormulaLine
{
    public int FormulaLineId { get; set; }

    public string FormulaNumber { get; set; } = null!;

    public string ItemNumber { get; set; } = null!;

    public int? FormulaQuantity00 { get; set; }

    public int? FormulaQuantity01 { get; set; }

    public int? FormulaQuantity02 { get; set; }

    public int? FormulaQuantity03 { get; set; }

    public int? FormulaQuantity04 { get; set; }

    public int? FormulaQuantity05 { get; set; }

    public int? FormulaQuantity06 { get; set; }

    public int? FormulaQuantity07 { get; set; }

    public int? FormulaQuantity08 { get; set; }

    public int? FormulaQuantity09 { get; set; }

    public int? FormulaQuantity10 { get; set; }

    public int? FormulaQuantity11 { get; set; }

    public int? FormulaQuantity12 { get; set; }

    public int? FormulaQuantity13 { get; set; }

    public int? FormulaQuantity14 { get; set; }

    public short? FormulaSizeNumber00 { get; set; }

    public short? FormulaSizeNumber01 { get; set; }

    public short? FormulaSizeNumber02 { get; set; }

    public short? FormulaSizeNumber03 { get; set; }

    public short? FormulaSizeNumber04 { get; set; }

    public short? FormulaSizeNumber05 { get; set; }

    public short? FormulaSizeNumber06 { get; set; }

    public short? FormulaSizeNumber07 { get; set; }

    public short? FormulaSizeNumber08 { get; set; }

    public short? FormulaSizeNumber09 { get; set; }

    public short? FormulaSizeNumber10 { get; set; }

    public short? FormulaSizeNumber11 { get; set; }

    public short? FormulaSizeNumber12 { get; set; }

    public short? FormulaSizeNumber13 { get; set; }

    public short? FormulaSizeNumber14 { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual FormulaHeader FormulaNumberNavigation { get; set; } = null!;

    public virtual Item ItemNumberNavigation { get; set; } = null!;
}
