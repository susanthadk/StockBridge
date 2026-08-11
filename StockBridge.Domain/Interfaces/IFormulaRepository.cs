using StockBridge.Domain.Entities;

namespace StockBridge.Domain.Interfaces;

public interface IFormulaRepository : IRepository<FormulaHeader>
{
    Task<IEnumerable<FormulaHeader>> GetAllWithLinesAsync();
    Task<FormulaHeader?> GetByIdWithLinesAsync(int headerId);
    Task SaveAsync();
    Task SoftDeleteWithLinesAsync(int headerId);
}