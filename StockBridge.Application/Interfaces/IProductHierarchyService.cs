using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchies;

namespace StockBridge.Application.Interfaces;

public interface IProductHierarchyService
{
    Task<ResponseInfo<List<ProductHierarchyDto>?>> GetAllProductHierarchies(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ProductHierarchyDto>?>> GetAllProductHierarchies(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ProductHierarchyDto?>> GetProductHierarchyById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ProductHierarchyDto>?>> SearchProductHierarchy(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ProductHierarchyDto?>> AddProductHierarchy(ProductHierarchyDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateProductHierarchy(ProductHierarchyDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteProductHierarchy(int id, CancellationToken cancellationToken = default);
}