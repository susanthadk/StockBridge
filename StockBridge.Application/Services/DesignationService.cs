using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Designations;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class DesignationService : IDesignationService
{
    private readonly IRepository<Designation> _designationRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<DesignationService> _logger;

    public DesignationService(
        IRepository<Designation> designationRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<DesignationService> logger)
    {
        _designationRepository = designationRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<DesignationDto>?>> GetAllDesignations(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Designations.");

        var result = await _designationRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Designations found.", nameof(DesignationService), nameof(GetAllDesignations));
            return ResponseInfo<List<DesignationDto>?>.Success(new List<DesignationDto>(), HttpStatusCode.NoContent, "No Designations found.");
        }

        var dtos = _mapper.Map<List<DesignationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Designations.", nameof(DesignationService), nameof(GetAllDesignations), dtos.Count);

        return ResponseInfo<List<DesignationDto>?>.Success(dtos, HttpStatusCode.OK, "Designations retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DesignationDto>?>> GetAllDesignations(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Designations. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _designationRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Designations found.", nameof(DesignationService), nameof(GetAllDesignations));
            return ResponseInfo<List<DesignationDto>?>.Success(new List<DesignationDto>(), HttpStatusCode.NoContent, "No Designations found.");
        }

        var dtos = _mapper.Map<List<DesignationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Designations.", nameof(DesignationService), nameof(GetAllDesignations), dtos.Count);

        return ResponseInfo<List<DesignationDto>?>.Success(dtos, HttpStatusCode.OK, "Designations retrieved successfully.");
    }

    public async Task<ResponseInfo<DesignationDto?>> GetDesignationById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Designation Id: {DesignationId}", id);

        var result = await _designationRepository.GetByIdAsync(id, nameof(Designation.DesignationId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Designation not found Id: {DesignationId}.", nameof(DesignationService), nameof(GetDesignationById), id);
            return ResponseInfo<DesignationDto?>.Success(null, HttpStatusCode.NoContent, "Designation not found.");
        }

        var dto = _mapper.Map<DesignationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Designation Id: {DesignationId}.", nameof(DesignationService), nameof(GetDesignationById), id);

        return ResponseInfo<DesignationDto?>.Success(dto, HttpStatusCode.OK, "Designation retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DesignationDto>?>> SearchDesignation(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Designations by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _designationRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Designations found.", nameof(DesignationService), nameof(SearchDesignation));
            return ResponseInfo<List<DesignationDto>?>.Success(new List<DesignationDto>(), HttpStatusCode.NoContent, "No Designations found.");
        }

        var dtos = _mapper.Map<List<DesignationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Designations.", nameof(DesignationService), nameof(SearchDesignation), dtos.Count);

        return ResponseInfo<List<DesignationDto>?>.Success(dtos, HttpStatusCode.OK, "Designations retrieved successfully.");
    }

    public async Task<ResponseInfo<DesignationDto?>> AddDesignation(DesignationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Designation.");

        var existing = await _designationRepository.GetByFieldAsync("DesignationCode", dto.DesignationCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Designation already exists with the same Designation Code.", nameof(DesignationService), nameof(AddDesignation));
            return ResponseInfo<DesignationDto?>.Failure("Designation already exists with the same Designation Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Designation>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _designationRepository.AddAsync(entity);

        var resultDto = _mapper.Map<DesignationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Designation added successfully DesignationId: {DesignationId}.", nameof(DesignationService), nameof(AddDesignation), result.DesignationId);

        return ResponseInfo<DesignationDto?>.Success(resultDto, HttpStatusCode.Created, "Designation added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateDesignation(DesignationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Designation Id: {DesignationId}", dto.DesignationId);

        var isExists = await _designationRepository.IsExistByIdAsync(dto.DesignationId, nameof(Designation.DesignationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Designation not found Id: {DesignationId}.", nameof(DesignationService), nameof(UpdateDesignation), dto.DesignationId);
            return ResponseInfo<bool>.Failure("Designation not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Designation>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _designationRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Designation updated Id: {DesignationId}.", nameof(DesignationService), nameof(UpdateDesignation), dto.DesignationId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Designation updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteDesignation(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Designation Id: {DesignationId}", id);

        var isExists = await _designationRepository.IsExistByIdAsync(id, nameof(Designation.DesignationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Designation not found Id: {DesignationId}.", nameof(DesignationService), nameof(DeleteDesignation), id);
            return ResponseInfo<bool>.Failure("Designation not found.", HttpStatusCode.NotFound);
        }

        await _designationRepository.DeleteAsync(id, nameof(Designation.DesignationId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Designation deleted Id: {DesignationId}.", nameof(DesignationService), nameof(DeleteDesignation), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Designation deleted successfully.");
    }
}