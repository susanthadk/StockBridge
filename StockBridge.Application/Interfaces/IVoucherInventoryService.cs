using StockBridge.Application.Common;
using StockBridge.Application.DTOs.VoucherInventories;

namespace StockBridge.Application.Interfaces;

public interface IVoucherInventoryService
{
    Task<ResponseInfo<List<VoucherInventoryHeaderDto>?>> GetAllVoucherInventories(CancellationToken cancellationToken = default);
    Task<ResponseInfo<VoucherInventoryHeaderDto?>> GetVoucherInventoryById(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<VoucherInventoryHeaderDto?>> AddVoucherInventory(CreateVoucherInventoryHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<VoucherInventoryHeaderDto?>> UpdateVoucherInventory(UpdateVoucherInventoryHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteVoucherInventory(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default);
}