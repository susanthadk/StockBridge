using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SupplierTypes;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class SupplierTypeService : ISupplierTypeService
{
    private readonly IRepository<SupplierType> _supplierTypeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<SupplierTypeService> _logger;

    public SupplierTypeService(
        IRepository<SupplierType> supplierTypeRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<SupplierTypeService> logger)
    {
        _supplierTypeRepository = supplierTypeRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<SupplierTypeDto>?>> GetAllSupplierTypes(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all SupplierTypes.");

        var result = await _supplierTypeRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SupplierTypes found.", nameof(SupplierTypeService), nameof(GetAllSupplierTypes));
            return ResponseInfo<List<SupplierTypeDto>?>.Success(new List<SupplierTypeDto>(), HttpStatusCode.NoContent, "No SupplierTypes found.");
        }

        var dtos = _mapper.Map<List<SupplierTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SupplierTypes.", nameof(SupplierTypeService), nameof(GetAllSupplierTypes), dtos.Count);

        return ResponseInfo<List<SupplierTypeDto>?>.Success(dtos, HttpStatusCode.OK, "SupplierTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SupplierTypeDto>?>> GetAllSupplierTypes(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all SupplierTypes. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _supplierTypeRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SupplierTypes found.", nameof(SupplierTypeService), nameof(GetAllSupplierTypes));
            return ResponseInfo<List<SupplierTypeDto>?>.Success(new List<SupplierTypeDto>(), HttpStatusCode.NoContent, "No SupplierTypes found.");
        }

        var dtos = _mapper.Map<List<SupplierTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SupplierTypes.", nameof(SupplierTypeService), nameof(GetAllSupplierTypes), dtos.Count);

        return ResponseInfo<List<SupplierTypeDto>?>.Success(dtos, HttpStatusCode.OK, "SupplierTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<SupplierTypeDto?>> GetSupplierTypeById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching SupplierType Id: {SupplierTypeId}", id);

        var result = await _supplierTypeRepository.GetByIdAsync(id, nameof(SupplierType.SupplierTypeId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType not found Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(GetSupplierTypeById), id);
            return ResponseInfo<SupplierTypeDto?>.Success(null, HttpStatusCode.NoContent, "SupplierType not found.");
        }

        var dto = _mapper.Map<SupplierTypeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved SupplierType Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(GetSupplierTypeById), id);

        return ResponseInfo<SupplierTypeDto?>.Success(dto, HttpStatusCode.OK, "SupplierType retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SupplierTypeDto>?>> SearchSupplierType(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching SupplierTypes by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _supplierTypeRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SupplierTypes found.", nameof(SupplierTypeService), nameof(SearchSupplierType));
            return ResponseInfo<List<SupplierTypeDto>?>.Success(new List<SupplierTypeDto>(), HttpStatusCode.NoContent, "No SupplierTypes found.");
        }

        var dtos = _mapper.Map<List<SupplierTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SupplierTypes.", nameof(SupplierTypeService), nameof(SearchSupplierType), dtos.Count);

        return ResponseInfo<List<SupplierTypeDto>?>.Success(dtos, HttpStatusCode.OK, "SupplierTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<SupplierTypeDto?>> AddSupplierType(SupplierTypeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding SupplierType.");

        var existing = await _supplierTypeRepository.GetByFieldAsync("SupplierTypeCode", dto.SupplierTypeCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType already exists with the same SupplierType Code.", nameof(SupplierTypeService), nameof(AddSupplierType));
            return ResponseInfo<SupplierTypeDto?>.Failure("SupplierType already exists with the same SupplierType Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<SupplierType>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _supplierTypeRepository.AddAsync(entity);

        var resultDto = _mapper.Map<SupplierTypeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType added successfully SupplierTypeId: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(AddSupplierType), result.SupplierTypeId);

        return ResponseInfo<SupplierTypeDto?>.Success(resultDto, HttpStatusCode.Created, "SupplierType added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateSupplierType(SupplierTypeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating SupplierType Id: {SupplierTypeId}", dto.SupplierTypeId);

        var isExists = await _supplierTypeRepository.IsExistByIdAsync(dto.SupplierTypeId, nameof(SupplierType.SupplierTypeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType not found Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(UpdateSupplierType), dto.SupplierTypeId);
            return ResponseInfo<bool>.Failure("SupplierType not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<SupplierType>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _supplierTypeRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType updated Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(UpdateSupplierType), dto.SupplierTypeId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "SupplierType updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteSupplierType(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting SupplierType Id: {SupplierTypeId}", id);

        var isExists = await _supplierTypeRepository.IsExistByIdAsync(id, nameof(SupplierType.SupplierTypeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType not found Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(DeleteSupplierType), id);
            return ResponseInfo<bool>.Failure("SupplierType not found.", HttpStatusCode.NotFound);
        }

        await _supplierTypeRepository.DeleteAsync(id, nameof(SupplierType.SupplierTypeId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: SupplierType deleted Id: {SupplierTypeId}.", nameof(SupplierTypeService), nameof(DeleteSupplierType), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "SupplierType deleted successfully.");
    }
}
