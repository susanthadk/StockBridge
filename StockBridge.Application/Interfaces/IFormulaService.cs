using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Formulas;

namespace StockBridge.Application.Interfaces;

public interface IFormulaService
{
    Task<ResponseInfo<List<FormulaHeaderDto>?>> GetAllFormulas(CancellationToken cancellationToken = default);
    Task<ResponseInfo<FormulaHeaderDto?>> GetFormulaById(int headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<FormulaHeaderDto?>> AddFormula(CreateFormulaHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<FormulaHeaderDto?>> UpdateFormula(UpdateFormulaHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteFormula(int headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> IsExist(int headerId, CancellationToken cancellationToken = default);
}