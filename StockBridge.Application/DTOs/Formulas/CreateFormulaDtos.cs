using System.ComponentModel.DataAnnotations;

namespace StockBridge.Application.DTOs.Formulas;

public class CreateFormulaLineDto
{
    [Required(ErrorMessage = "ItemNumber is required.")]
    [StringLength(7, ErrorMessage = "ItemNumber cannot exceed 7 characters.")]
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
}

public class CreateFormulaHeaderDto
{
    [Required(ErrorMessage = "FormulaNumber is required.")]
    [StringLength(10, ErrorMessage = "FormulaNumber cannot exceed 10 characters.")]
    public string FormulaNumber { get; set; } = null!;

    public DateTime? FormulaDate { get; set; }

    [Required(ErrorMessage = "At least one line is required.")]
    [MinLength(1, ErrorMessage = "At least one line is required.")]
    public List<CreateFormulaLineDto> FormulaLines { get; set; } = new List<CreateFormulaLineDto>();
}