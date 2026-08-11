using Microsoft.EntityFrameworkCore;
using StockBridge.API.Data;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;

namespace StockBridge.Infrastructure.Repositories;

public class FormulaRepository(StockBridgeDbContext context) : Repository<FormulaHeader>(context), IFormulaRepository
{
    private readonly StockBridgeDbContext _context = context;

    public async Task<IEnumerable<FormulaHeader>> GetAllWithLinesAsync()
    {
        return await _context.FormulaHeaders
            .Where(h => h.IsActive)
            .Include(h => h.FormulaLines.Where(l => l.IsActive))
            .ToListAsync();
    }

    public async Task<FormulaHeader?> GetByIdWithLinesAsync(int headerId)
    {
        return await _context.FormulaHeaders
            .Where(h => h.IsActive)
            .Include(h => h.FormulaLines.Where(l => l.IsActive))
            .FirstOrDefaultAsync(h => h.FormulaHeaderId == headerId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteWithLinesAsync(int headerId)
    {
        var entity = await GetByIdWithLinesAsync(headerId);
        if (entity == null)
            return;

        entity.IsActive = false;

        foreach (var line in entity.FormulaLines)
        {
            line.IsActive = false;
        }

        await _context.SaveChangesAsync();
    }
}