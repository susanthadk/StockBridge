using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Categories;

namespace StockBridge.Application.Interfaces;

public interface ICategoryService
{
    Task<ResponseInfo<List<CategoryDto>?>> GetAllCategories(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CategoryDto>?>> GetAllCategories(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CategoryDto?>> GetCategoryById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CategoryDto>?>> SearchCategory(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CategoryDto?>> AddCategory(CategoryDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateCategory(CategoryDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteCategory(int id, CancellationToken cancellationToken = default);
}