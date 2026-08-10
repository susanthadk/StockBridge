using Microsoft.EntityFrameworkCore;
using StockBridge.API.Data;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;

namespace StockBridge.Infrastructure.Repositories;

public class InventoryTransactionRepository(StockBridgeDbContext context) : Repository<InventoryHeaderTransaction>(context), IInventoryTransactionRepository
{
    private readonly StockBridgeDbContext _context = context;

    public async Task<IEnumerable<InventoryHeaderTransaction>> GetAllWithLinesAsync()
    {
        return await _context.InventoryHeaderTransactions
            .Where(h => h.IsActive)
            .Include(h => h.InventoryLineTransactions.Where(l => l.IsActive))
            .ToListAsync();
    }

    public async Task<InventoryHeaderTransaction?> GetByIdWithLinesAsync(long headerId)
    {
        return await _context.InventoryHeaderTransactions
            .Where(h => h.IsActive)
            .Include(h => h.InventoryLineTransactions.Where(l => l.IsActive))
            .FirstOrDefaultAsync(h => h.InventoryHeaderTransactionId == headerId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteWithLinesAsync(long headerId)
    {
        var entity = await GetByIdWithLinesAsync(headerId);
        if (entity == null)
            return;

        entity.IsActive = false;

        foreach (var line in entity.InventoryLineTransactions)
        {
            line.IsActive = false;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistByBusinessKeyAsync(string type, string documentNumber, DateTime date, string operationCode, int terminalNumber)
    {
        return await _context.InventoryHeaderTransactions
            .Where(h => h.IsActive)
            .AnyAsync(h => h.InventoryHeaderType == type
                && h.InventoryHeaderDocumentNumber == documentNumber
                && h.InventoryHeaderDate == date
                && h.InventoryHeaderOperationCode == operationCode
                && h.TerminalNumber == terminalNumber);
    }
}