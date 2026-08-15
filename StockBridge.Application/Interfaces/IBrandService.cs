using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Brands;

namespace StockBridge.Application.Interfaces;

public interface IBrandService
{
    Task<ResponseInfo<List<BrandDto>?>> GetAllBrands(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<BrandDto>?>> GetAllBrands(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<BrandDto?>> GetBrandById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<BrandDto>?>> SearchBrand(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<BrandDto?>> AddBrand(BrandDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateBrand(BrandDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteBrand(int id, CancellationToken cancellationToken = default);
}