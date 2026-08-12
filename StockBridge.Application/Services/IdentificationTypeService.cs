using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.IdentificationTypes;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class IdentificationTypeService : IIdentificationTypeService
{
    private readonly IRepository<IdentificationType> _identificationTypeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<IdentificationTypeService> _logger;

    public IdentificationTypeService(
        IRepository<IdentificationType> identificationTypeRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<IdentificationTypeService> logger)
    {
        _identificationTypeRepository = identificationTypeRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<IdentificationTypeDto>?>> GetAllIdentificationTypes(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all IdentificationTypes.");

        var result = await _identificationTypeRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No IdentificationTypes found.", nameof(IdentificationTypeService), nameof(GetAllIdentificationTypes));
            return ResponseInfo<List<IdentificationTypeDto>?>.Success(new List<IdentificationTypeDto>(), HttpStatusCode.NoContent, "No IdentificationTypes found.");
        }

        var dtos = _mapper.Map<List<IdentificationTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} IdentificationTypes.", nameof(IdentificationTypeService), nameof(GetAllIdentificationTypes), dtos.Count);

        return ResponseInfo<List<IdentificationTypeDto>?>.Success(dtos, HttpStatusCode.OK, "IdentificationTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<List<IdentificationTypeDto>?>> GetAllIdentificationTypes(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all IdentificationTypes. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _identificationTypeRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No IdentificationTypes found.", nameof(IdentificationTypeService), nameof(GetAllIdentificationTypes));
            return ResponseInfo<List<IdentificationTypeDto>?>.Success(new List<IdentificationTypeDto>(), HttpStatusCode.NoContent, "No IdentificationTypes found.");
        }

        var dtos = _mapper.Map<List<IdentificationTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} IdentificationTypes.", nameof(IdentificationTypeService), nameof(GetAllIdentificationTypes), dtos.Count);

        return ResponseInfo<List<IdentificationTypeDto>?>.Success(dtos, HttpStatusCode.OK, "IdentificationTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<IdentificationTypeDto?>> GetIdentificationTypeById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching IdentificationType Id: {IdentificationTypeId}", id);

        var result = await _identificationTypeRepository.GetByIdAsync(id, nameof(IdentificationType.IdentificationTypeId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType not found Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(GetIdentificationTypeById), id);
            return ResponseInfo<IdentificationTypeDto?>.Success(null, HttpStatusCode.NoContent, "IdentificationType not found.");
        }

        var dto = _mapper.Map<IdentificationTypeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved IdentificationType Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(GetIdentificationTypeById), id);

        return ResponseInfo<IdentificationTypeDto?>.Success(dto, HttpStatusCode.OK, "IdentificationType retrieved successfully.");
    }

    public async Task<ResponseInfo<List<IdentificationTypeDto>?>> SearchIdentificationType(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching IdentificationTypes by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _identificationTypeRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No IdentificationTypes found.", nameof(IdentificationTypeService), nameof(SearchIdentificationType));
            return ResponseInfo<List<IdentificationTypeDto>?>.Success(new List<IdentificationTypeDto>(), HttpStatusCode.NoContent, "No IdentificationTypes found.");
        }

        var dtos = _mapper.Map<List<IdentificationTypeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} IdentificationTypes.", nameof(IdentificationTypeService), nameof(SearchIdentificationType), dtos.Count);

        return ResponseInfo<List<IdentificationTypeDto>?>.Success(dtos, HttpStatusCode.OK, "IdentificationTypes retrieved successfully.");
    }

    public async Task<ResponseInfo<IdentificationTypeDto?>> AddIdentificationType(IdentificationTypeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding IdentificationType.");

        var existing = await _identificationTypeRepository.GetByFieldAsync("IdentificationTypeCode", dto.IdentificationTypeCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType already exists with the same IdentificationType Code.", nameof(IdentificationTypeService), nameof(AddIdentificationType));
            return ResponseInfo<IdentificationTypeDto?>.Failure("IdentificationType already exists with the same IdentificationType Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<IdentificationType>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _identificationTypeRepository.AddAsync(entity);

        var resultDto = _mapper.Map<IdentificationTypeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType added successfully IdentificationTypeId: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(AddIdentificationType), result.IdentificationTypeId);

        return ResponseInfo<IdentificationTypeDto?>.Success(resultDto, HttpStatusCode.Created, "IdentificationType added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateIdentificationType(IdentificationTypeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating IdentificationType Id: {IdentificationTypeId}", dto.IdentificationTypeId);

        var isExists = await _identificationTypeRepository.IsExistByIdAsync(dto.IdentificationTypeId, nameof(IdentificationType.IdentificationTypeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType not found Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(UpdateIdentificationType), dto.IdentificationTypeId);
            return ResponseInfo<bool>.Failure("IdentificationType not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<IdentificationType>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _identificationTypeRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType updated Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(UpdateIdentificationType), dto.IdentificationTypeId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "IdentificationType updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteIdentificationType(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting IdentificationType Id: {IdentificationTypeId}", id);

        var isExists = await _identificationTypeRepository.IsExistByIdAsync(id, nameof(IdentificationType.IdentificationTypeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType not found Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(DeleteIdentificationType), id);
            return ResponseInfo<bool>.Failure("IdentificationType not found.", HttpStatusCode.NotFound);
        }

        await _identificationTypeRepository.DeleteAsync(id, nameof(IdentificationType.IdentificationTypeId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: IdentificationType deleted Id: {IdentificationTypeId}.", nameof(IdentificationTypeService), nameof(DeleteIdentificationType), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "IdentificationType deleted successfully.");
    }
}