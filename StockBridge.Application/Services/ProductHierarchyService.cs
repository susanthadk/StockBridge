using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.ProductHierarchies;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class ProductHierarchyService : IProductHierarchyService
{
    private readonly IRepository<ProductHierarchy> _productHierarchyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductHierarchyService> _logger;

    public ProductHierarchyService(
        IRepository<ProductHierarchy> productHierarchyRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<ProductHierarchyService> logger)
    {
        _productHierarchyRepository = productHierarchyRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<ProductHierarchyDto>?>> GetAllProductHierarchies(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all ProductHierarchies.");

        var result = await _productHierarchyRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchies found.", nameof(ProductHierarchyService), nameof(GetAllProductHierarchies));
            return ResponseInfo<List<ProductHierarchyDto>?>.Success(new List<ProductHierarchyDto>(), HttpStatusCode.NoContent, "No ProductHierarchies found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchies.", nameof(ProductHierarchyService), nameof(GetAllProductHierarchies), dtos.Count);

        return ResponseInfo<List<ProductHierarchyDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchies retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ProductHierarchyDto>?>> GetAllProductHierarchies(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all ProductHierarchies. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _productHierarchyRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchies found.", nameof(ProductHierarchyService), nameof(GetAllProductHierarchies));
            return ResponseInfo<List<ProductHierarchyDto>?>.Success(new List<ProductHierarchyDto>(), HttpStatusCode.NoContent, "No ProductHierarchies found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchies.", nameof(ProductHierarchyService), nameof(GetAllProductHierarchies), dtos.Count);

        return ResponseInfo<List<ProductHierarchyDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchies retrieved successfully.");
    }

    public async Task<ResponseInfo<ProductHierarchyDto?>> GetProductHierarchyById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching ProductHierarchy Id: {ProductHierarchyId}", id);

        var result = await _productHierarchyRepository.GetByIdAsync(id, nameof(ProductHierarchy.ProductHierarchyId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy not found Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(GetProductHierarchyById), id);
            return ResponseInfo<ProductHierarchyDto?>.Success(null, HttpStatusCode.NoContent, "ProductHierarchy not found.");
        }

        var dto = _mapper.Map<ProductHierarchyDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved ProductHierarchy Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(GetProductHierarchyById), id);

        return ResponseInfo<ProductHierarchyDto?>.Success(dto, HttpStatusCode.OK, "ProductHierarchy retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ProductHierarchyDto>?>> SearchProductHierarchy(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching ProductHierarchies by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _productHierarchyRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No ProductHierarchies found.", nameof(ProductHierarchyService), nameof(SearchProductHierarchy));
            return ResponseInfo<List<ProductHierarchyDto>?>.Success(new List<ProductHierarchyDto>(), HttpStatusCode.NoContent, "No ProductHierarchies found.");
        }

        var dtos = _mapper.Map<List<ProductHierarchyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} ProductHierarchies.", nameof(ProductHierarchyService), nameof(SearchProductHierarchy), dtos.Count);

        return ResponseInfo<List<ProductHierarchyDto>?>.Success(dtos, HttpStatusCode.OK, "ProductHierarchies retrieved successfully.");
    }

    public async Task<ResponseInfo<ProductHierarchyDto?>> AddProductHierarchy(ProductHierarchyDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding ProductHierarchy.");

        var existing = await _productHierarchyRepository.GetByFieldAsync("ProductHierarchyCode", dto.ProductHierarchyCode);
        if (existing?.Any(x => x.ProductHierarchyLevelId == dto.ProductHierarchyLevelId) == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy already exists with the same ProductHierarchyCode and ProductHierarchyLevelId.", nameof(ProductHierarchyService), nameof(AddProductHierarchy));
            return ResponseInfo<ProductHierarchyDto?>.Failure("ProductHierarchy already exists with the same ProductHierarchyCode and ProductHierarchyLevelId.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<ProductHierarchy>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _productHierarchyRepository.AddAsync(entity);

        var resultDto = _mapper.Map<ProductHierarchyDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy added successfully ProductHierarchyId: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(AddProductHierarchy), result.ProductHierarchyId);

        return ResponseInfo<ProductHierarchyDto?>.Success(resultDto, HttpStatusCode.Created, "ProductHierarchy added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateProductHierarchy(ProductHierarchyDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating ProductHierarchy Id: {ProductHierarchyId}", dto.ProductHierarchyId);

        var isExists = await _productHierarchyRepository.IsExistByIdAsync(dto.ProductHierarchyId, nameof(ProductHierarchy.ProductHierarchyId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy not found Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(UpdateProductHierarchy), dto.ProductHierarchyId);
            return ResponseInfo<bool>.Failure("ProductHierarchy not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<ProductHierarchy>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _productHierarchyRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy updated Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(UpdateProductHierarchy), dto.ProductHierarchyId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "ProductHierarchy updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteProductHierarchy(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting ProductHierarchy Id: {ProductHierarchyId}", id);

        var isExists = await _productHierarchyRepository.IsExistByIdAsync(id, nameof(ProductHierarchy.ProductHierarchyId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy not found Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(DeleteProductHierarchy), id);
            return ResponseInfo<bool>.Failure("ProductHierarchy not found.", HttpStatusCode.NotFound);
        }

        await _productHierarchyRepository.DeleteAsync(id, nameof(ProductHierarchy.ProductHierarchyId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: ProductHierarchy deleted Id: {ProductHierarchyId}.", nameof(ProductHierarchyService), nameof(DeleteProductHierarchy), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "ProductHierarchy deleted successfully.");
    }
}