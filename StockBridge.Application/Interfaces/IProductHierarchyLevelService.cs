using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchyLevels;

namespace StockBridge.Application.Interfaces;

public interface IProductHierarchyLevelService
{
    Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> GetAllProductHierarchyLevels(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> GetAllProductHierarchyLevels(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ProductHierarchyLevelDto?>> GetProductHierarchyLevelById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> SearchProductHierarchyLevel(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ProductHierarchyLevelDto?>> AddProductHierarchyLevel(ProductHierarchyLevelDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateProductHierarchyLevel(ProductHierarchyLevelDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteProductHierarchyLevel(int id, CancellationToken cancellationToken = default);
}