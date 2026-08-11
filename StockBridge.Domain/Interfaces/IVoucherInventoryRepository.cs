using StockBridge.Domain.Entities;

namespace StockBridge.Domain.Interfaces;

public interface IVoucherInventoryRepository : IRepository<VoucherInventoryHeader>
{
    Task<IEnumerable<VoucherInventoryHeader>> GetAllWithLinesAsync();
    Task<VoucherInventoryHeader?> GetByIdWithLinesAsync(long headerId);
    Task SaveAsync();
    Task SoftDeleteWithLinesAsync(long headerId);
    Task<bool> IsExistByBusinessKeyAsync(string location, string type, string documentNumber, DateTime date, string operationCode, int terminalNumber);
}