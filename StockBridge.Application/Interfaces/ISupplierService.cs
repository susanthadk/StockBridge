using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Suppliers;

namespace StockBridge.Application.Interfaces;

public interface ISupplierService
{
    Task<ResponseInfo<List<SupplierDto>?>> GetAllSuppliers(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SupplierDto>?>> GetAllSuppliers(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SupplierDto?>> GetSupplierById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SupplierDto>?>> SearchSupplier(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SupplierDto?>> AddSupplier(SupplierDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateSupplier(SupplierDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteSupplier(int id, CancellationToken cancellationToken = default);
}