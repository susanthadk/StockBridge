using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SupplierTypes;

namespace StockBridge.Application.Interfaces;

public interface ISupplierTypeService
{
    Task<ResponseInfo<List<SupplierTypeDto>?>> GetAllSupplierTypes(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SupplierTypeDto>?>> GetAllSupplierTypes(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SupplierTypeDto?>> GetSupplierTypeById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SupplierTypeDto>?>> SearchSupplierType(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SupplierTypeDto?>> AddSupplierType(SupplierTypeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateSupplierType(SupplierTypeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteSupplierType(int id, CancellationToken cancellationToken = default);
}
