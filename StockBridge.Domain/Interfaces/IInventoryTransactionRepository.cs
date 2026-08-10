using StockBridge.Domain.Entities;

namespace StockBridge.Domain.Interfaces;

public interface IInventoryTransactionRepository : IRepository<InventoryHeaderTransaction>
{
    Task<IEnumerable<InventoryHeaderTransaction>> GetAllWithLinesAsync();
    Task<InventoryHeaderTransaction?> GetByIdWithLinesAsync(long headerId);
    Task SaveAsync();
    Task SoftDeleteWithLinesAsync(long headerId);
    Task<bool> IsExistByBusinessKeyAsync(string type, string documentNumber, DateTime date, string operationCode, int terminalNumber);
}