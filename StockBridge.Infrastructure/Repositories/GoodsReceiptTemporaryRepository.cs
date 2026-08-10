using Microsoft.EntityFrameworkCore;
using StockBridge.API.Data;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;

namespace StockBridge.Infrastructure.Repositories;

public class GoodsReceiptTemporaryRepository(StockBridgeDbContext context) : Repository<GoodsReceiptTemporaryHeader>(context), IGoodsReceiptTemporaryRepository
{
    private readonly StockBridgeDbContext _context = context;

    public async Task<IEnumerable<GoodsReceiptTemporaryHeader>> GetAllWithDetailsAsync()
    {
        return await _context.GoodsReceiptTemporaryHeaders
            .Include(h => h.GoodsReceiptTemporaryDetails)
            .ToListAsync();
    }

    public async Task<GoodsReceiptTemporaryHeader?> GetByIdWithDetailsAsync(long headerId)
    {
        return await _context.GoodsReceiptTemporaryHeaders
            .Include(h => h.GoodsReceiptTemporaryDetails)
            .FirstOrDefaultAsync(h => h.GoodsReceiptTemporaryHeaderId == headerId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWithDetailsAsync(long headerId)
    {
        var entity = await GetByIdWithDetailsAsync(headerId);
        if (entity == null)
            return;

        foreach (var detail in entity.GoodsReceiptTemporaryDetails.ToList())
        {
            entity.GoodsReceiptTemporaryDetails.Remove(detail);
        }

        _context.GoodsReceiptTemporaryHeaders.Remove(entity);
        await _context.SaveChangesAsync();
    }
}