using Microsoft.EntityFrameworkCore;
using StockBridge.API.Data;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;

namespace StockBridge.Infrastructure.Repositories;

public class VoucherInventoryRepository(StockBridgeDbContext context) : Repository<VoucherInventoryHeader>(context), IVoucherInventoryRepository
{
    private readonly StockBridgeDbContext _context = context;

    public async Task<IEnumerable<VoucherInventoryHeader>> GetAllWithLinesAsync()
    {
        return await _context.VoucherInventoryHeaders
            .Where(h => h.IsActive)
            .Include(h => h.VoucherInventoryLines.Where(l => l.IsActive))
            .ToListAsync();
    }

    public async Task<VoucherInventoryHeader?> GetByIdWithLinesAsync(long headerId)
    {
        return await _context.VoucherInventoryHeaders
            .Where(h => h.IsActive)
            .Include(h => h.VoucherInventoryLines.Where(l => l.IsActive))
            .FirstOrDefaultAsync(h => h.VoucherInventoryHeaderId == headerId);
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

        foreach (var line in entity.VoucherInventoryLines)
        {
            line.IsActive = false;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistByBusinessKeyAsync(string location, string type, string documentNumber, DateTime date, string operationCode, int terminalNumber)
    {
        return await _context.VoucherInventoryHeaders
            .Where(h => h.IsActive)
            .AnyAsync(h => h.InventoryHeaderLocation == location
                && h.InventoryHeaderType == type
                && h.InventoryHeaderDocumentNumber == documentNumber
                && h.InventoryHeaderDate == date
                && h.InventoryHeaderOperationCode == operationCode
                && h.TerminalNumber == terminalNumber);
    }
}