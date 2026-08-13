using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchyLevels;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class ProductHierarchyLevelService : IProductHierarchyLevelService
{
    private readonly IRepository<ProductHierarchyLevel> _productHierarchyLevelRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductHierarchyLevelService> _logger;

    public ProductHierarchyLevelService(
        IRepository<ProductHierarchyLevel> productHierarchyLevelRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<ProductHierarchyLevelService> logger)
    {
        _productHierarchyLevelRepository = productHierarchyLevelRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> GetAllProductHierarchyLevels(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all ProductHierarchyLevels.");

        var result = await _productHierarchyLevelRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchyLevels found.", nameof(ProductHierarchyLevelService), nameof(GetAllProductHierarchyLevels));
            return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(new List<ProductHierarchyLevelDto>(), HttpStatusCode.NoContent, "No ProductHierarchyLevels found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchyLevels.", nameof(ProductHierarchyLevelService), nameof(GetAllProductHierarchyLevels), dtos.Count);

        return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchyLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> GetAllProductHierarchyLevels(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all ProductHierarchyLevels. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _productHierarchyLevelRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchyLevels found.", nameof(ProductHierarchyLevelService), nameof(GetAllProductHierarchyLevels));
            return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(new List<ProductHierarchyLevelDto>(), HttpStatusCode.NoContent, "No ProductHierarchyLevels found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchyLevels.", nameof(ProductHierarchyLevelService), nameof(GetAllProductHierarchyLevels), dtos.Count);

        return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchyLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<ProductHierarchyLevelDto?>> GetProductHierarchyLevelById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching ProductHierarchyLevel Id: {ProductHierarchyLevelId}", id);

        var result = await _productHierarchyLevelRepository.GetByIdAsync(id, nameof(ProductHierarchyLevel.ProductHierarchyLevelId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel not found Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(GetProductHierarchyLevelById), id);
            return ResponseInfo<ProductHierarchyLevelDto?>.Success(null, HttpStatusCode.NoContent, "ProductHierarchyLevel not found.");
        }

        var dto = _mapper.Map<ProductHierarchyLevelDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved ProductHierarchyLevel Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(GetProductHierarchyLevelById), id);

        return ResponseInfo<ProductHierarchyLevelDto?>.Success(dto, HttpStatusCode.OK, "ProductHierarchyLevel retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ProductHierarchyLevelDto>?>> SearchProductHierarchyLevel(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching ProductHierarchyLevels by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _productHierarchyLevelRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchyLevels found.", nameof(ProductHierarchyLevelService), nameof(SearchProductHierarchyLevel));
            return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(new List<ProductHierarchyLevelDto>(), HttpStatusCode.NoContent, "No ProductHierarchyLevels found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchyLevels.", nameof(ProductHierarchyLevelService), nameof(SearchProductHierarchyLevel), dtos.Count);

        return ResponseInfo<List<ProductHierarchyLevelDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchyLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<ProductHierarchyLevelDto?>> AddProductHierarchyLevel(ProductHierarchyLevelDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding ProductHierarchyLevel.");

        var existing = await _productHierarchyLevelRepository.GetByFieldAsync("LevelCode", dto.LevelCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel already exists with the same LevelCode.", nameof(ProductHierarchyLevelService), nameof(AddProductHierarchyLevel));
            return ResponseInfo<ProductHierarchyLevelDto?>.Failure("ProductHierarchyLevel already exists with the same LevelCode.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<ProductHierarchyLevel>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _productHierarchyLevelRepository.AddAsync(entity);

        var resultDto = _mapper.Map<ProductHierarchyLevelDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel added successfully ProductHierarchyLevelId: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(AddProductHierarchyLevel), result.ProductHierarchyLevelId);

        return ResponseInfo<ProductHierarchyLevelDto?>.Success(resultDto, HttpStatusCode.Created, "ProductHierarchyLevel added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateProductHierarchyLevel(ProductHierarchyLevelDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating ProductHierarchyLevel Id: {ProductHierarchyLevelId}", dto.ProductHierarchyLevelId);

        var isExists = await _productHierarchyLevelRepository.IsExistByIdAsync(dto.ProductHierarchyLevelId, nameof(ProductHierarchyLevel.ProductHierarchyLevelId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel not found Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(UpdateProductHierarchyLevel), dto.ProductHierarchyLevelId);
            return ResponseInfo<bool>.Failure("ProductHierarchyLevel not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<ProductHierarchyLevel>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _productHierarchyLevelRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel updated Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(UpdateProductHierarchyLevel), dto.ProductHierarchyLevelId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "ProductHierarchyLevel updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteProductHierarchyLevel(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting ProductHierarchyLevel Id: {ProductHierarchyLevelId}", id);

        var isExists = await _productHierarchyLevelRepository.IsExistByIdAsync(id, nameof(ProductHierarchyLevel.ProductHierarchyLevelId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel not found Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(DeleteProductHierarchyLevel), id);
            return ResponseInfo<bool>.Failure("ProductHierarchyLevel not found.", HttpStatusCode.NotFound);
        }

        await _productHierarchyLevelRepository.DeleteAsync(id, nameof(ProductHierarchyLevel.ProductHierarchyLevelId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchyLevel deleted Id: {ProductHierarchyLevelId}.", nameof(ProductHierarchyLevelService), nameof(DeleteProductHierarchyLevel), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "ProductHierarchyLevel deleted successfully.");
    }
}