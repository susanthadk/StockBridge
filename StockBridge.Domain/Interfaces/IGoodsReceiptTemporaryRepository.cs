using StockBridge.Domain.Entities;

namespace StockBridge.Domain.Interfaces;

public interface IGoodsReceiptTemporaryRepository : IRepository<GoodsReceiptTemporaryHeader>
{
    Task<IEnumerable<GoodsReceiptTemporaryHeader>> GetAllWithDetailsAsync();
    Task<GoodsReceiptTemporaryHeader?> GetByIdWithDetailsAsync(long headerId);
    Task SaveAsync();
    Task DeleteWithDetailsAsync(long headerId);
}