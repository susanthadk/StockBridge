using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Brands;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class BrandService : IBrandService
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<BrandService> _logger;

    public BrandService(
        IRepository<Brand> brandRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<BrandService> logger)
    {
        _brandRepository = brandRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<BrandDto>?>> GetAllBrands(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Brands.");

        var result = await _brandRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Brands found.", nameof(BrandService), nameof(GetAllBrands));
            return ResponseInfo<List<BrandDto>?>.Success(new List<BrandDto>(), HttpStatusCode.NoContent, "No Brands found.");
        }

        var dtos = _mapper.Map<List<BrandDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Brands.", nameof(BrandService), nameof(GetAllBrands), dtos.Count);

        return ResponseInfo<List<BrandDto>?>.Success(dtos, HttpStatusCode.OK, "Brands retrieved successfully.");
    }

    public async Task<ResponseInfo<List<BrandDto>?>> GetAllBrands(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Brands. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _brandRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Brands found.", nameof(BrandService), nameof(GetAllBrands));
            return ResponseInfo<List<BrandDto>?>.Success(new List<BrandDto>(), HttpStatusCode.NoContent, "No Brands found.");
        }

        var dtos = _mapper.Map<List<BrandDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Brands.", nameof(BrandService), nameof(GetAllBrands), dtos.Count);

        return ResponseInfo<List<BrandDto>?>.Success(dtos, HttpStatusCode.OK, "Brands retrieved successfully.");
    }

    public async Task<ResponseInfo<BrandDto?>> GetBrandById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Brand Id: {BrandId}", id);

        var result = await _brandRepository.GetByIdAsync(id, nameof(Brand.BrandId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Brand not found Id: {BrandId}.", nameof(BrandService), nameof(GetBrandById), id);
            return ResponseInfo<BrandDto?>.Success(null, HttpStatusCode.NoContent, "Brand not found.");
        }

        var dto = _mapper.Map<BrandDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Brand Id: {BrandId}.", nameof(BrandService), nameof(GetBrandById), id);

        return ResponseInfo<BrandDto?>.Success(dto, HttpStatusCode.OK, "Brand retrieved successfully.");
    }

    public async Task<ResponseInfo<List<BrandDto>?>> SearchBrand(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Brands by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _brandRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Brands found.", nameof(BrandService), nameof(SearchBrand));
            return ResponseInfo<List<BrandDto>?>.Success(new List<BrandDto>(), HttpStatusCode.NoContent, "No Brands found.");
        }

        var dtos = _mapper.Map<List<BrandDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Brands.", nameof(BrandService), nameof(SearchBrand), dtos.Count);

        return ResponseInfo<List<BrandDto>?>.Success(dtos, HttpStatusCode.OK, "Brands retrieved successfully.");
    }

    public async Task<ResponseInfo<BrandDto?>> AddBrand(BrandDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Brand.");

        var existingByCode = await _brandRepository.GetByFieldAsync("BrandCode", dto.BrandCode);
        if (existingByCode?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Brand already exists with the same Brand Code.", nameof(BrandService), nameof(AddBrand));
            return ResponseInfo<BrandDto?>.Failure("Brand already exists with the same Brand Code.", HttpStatusCode.BadRequest);
        }

        var existingByName = await _brandRepository.GetByFieldAsync("BrandName", dto.BrandName);
        if (existingByName?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Brand already exists with the same Brand Name.", nameof(BrandService), nameof(AddBrand));
            return ResponseInfo<BrandDto?>.Failure("Brand already exists with the same Brand Name.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Brand>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _brandRepository.AddAsync(entity);

        var resultDto = _mapper.Map<BrandDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Brand added successfully BrandId: {BrandId}.", nameof(BrandService), nameof(AddBrand), result.BrandId);

        return ResponseInfo<BrandDto?>.Success(resultDto, HttpStatusCode.Created, "Brand added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateBrand(BrandDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Brand Id: {BrandId}", dto.BrandId);

        var isExists = await _brandRepository.IsExistByIdAsync(dto.BrandId, nameof(Brand.BrandId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Brand not found Id: {BrandId}.", nameof(BrandService), nameof(UpdateBrand), dto.BrandId);
            return ResponseInfo<bool>.Failure("Brand not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Brand>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _brandRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Brand updated Id: {BrandId}.", nameof(BrandService), nameof(UpdateBrand), dto.BrandId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Brand updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteBrand(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Brand Id: {BrandId}", id);

        var isExists = await _brandRepository.IsExistByIdAsync(id, nameof(Brand.BrandId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Brand not found Id: {BrandId}.", nameof(BrandService), nameof(DeleteBrand), id);
            return ResponseInfo<bool>.Failure("Brand not found.", HttpStatusCode.NotFound);
        }

        await _brandRepository.DeleteAsync(id, nameof(Brand.BrandId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Brand deleted Id: {BrandId}.", nameof(BrandService), nameof(DeleteBrand), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Brand deleted successfully.");
    }
}