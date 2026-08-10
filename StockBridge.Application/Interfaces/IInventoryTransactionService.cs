using StockBridge.Application.Common;
using StockBridge.Application.DTOs.InventoryTransactions;

namespace StockBridge.Application.Interfaces;

public interface IInventoryTransactionService
{
    Task<ResponseInfo<List<InventoryHeaderTransactionDto>?>> GetAllInventoryTransactions(CancellationToken cancellationToken = default);
    Task<ResponseInfo<InventoryHeaderTransactionDto?>> GetInventoryTransactionById(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<InventoryHeaderTransactionDto?>> AddInventoryTransaction(CreateInventoryHeaderTransactionDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<InventoryHeaderTransactionDto?>> UpdateInventoryTransaction(UpdateInventoryHeaderTransactionDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteInventoryTransaction(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default);
}