using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.VoucherInventories;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class VoucherInventoryService : IVoucherInventoryService
{
    private readonly IVoucherInventoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<VoucherInventoryService> _logger;

    public VoucherInventoryService(
        IVoucherInventoryRepository repository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<VoucherInventoryService> logger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<VoucherInventoryHeaderDto>?>> GetAllVoucherInventories(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all VoucherInventories.");

        var result = await _repository.GetAllWithLinesAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No VoucherInventories found.", nameof(VoucherInventoryService), nameof(GetAllVoucherInventories));
            return ResponseInfo<List<VoucherInventoryHeaderDto>?>.Success(new List<VoucherInventoryHeaderDto>(), HttpStatusCode.NoContent, "No VoucherInventories found.");
        }

        var dtos = _mapper.Map<List<VoucherInventoryHeaderDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} VoucherInventories.", nameof(VoucherInventoryService), nameof(GetAllVoucherInventories), dtos.Count);

        return ResponseInfo<List<VoucherInventoryHeaderDto>?>.Success(dtos, HttpStatusCode.OK, "VoucherInventories retrieved successfully.");
    }

    public async Task<ResponseInfo<VoucherInventoryHeaderDto?>> GetVoucherInventoryById(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching VoucherInventory Id: {HeaderId}", headerId);

        var result = await _repository.GetByIdWithLinesAsync(headerId);

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory not found Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(GetVoucherInventoryById), headerId);
            return ResponseInfo<VoucherInventoryHeaderDto?>.Success(null, HttpStatusCode.NoContent, "VoucherInventory not found.");
        }

        var dto = _mapper.Map<VoucherInventoryHeaderDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved VoucherInventory Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(GetVoucherInventoryById), headerId);

        return ResponseInfo<VoucherInventoryHeaderDto?>.Success(dto, HttpStatusCode.OK, "VoucherInventory retrieved successfully.");
    }

    public async Task<ResponseInfo<VoucherInventoryHeaderDto?>> AddVoucherInventory(CreateVoucherInventoryHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding VoucherInventory.");

        if (dto.VoucherInventoryLines == null || !dto.VoucherInventoryLines.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory cannot be added without lines.", nameof(VoucherInventoryService), nameof(AddVoucherInventory));
            return ResponseInfo<VoucherInventoryHeaderDto?>.Failure("VoucherInventory must contain at least one line.", HttpStatusCode.BadRequest);
        }

        var isExists = await _repository.IsExistByBusinessKeyAsync(dto.InventoryHeaderLocation, dto.InventoryHeaderType, dto.InventoryHeaderDocumentNumber, dto.InventoryHeaderDate, dto.InventoryHeaderOperationCode, dto.TerminalNumber);
        if (isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory already exists with the same business key.", nameof(VoucherInventoryService), nameof(AddVoucherInventory));
            return ResponseInfo<VoucherInventoryHeaderDto?>.Failure("VoucherInventory already exists with the same Location, Type, Document Number, Date, Operation Code and Terminal Number.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<VoucherInventoryHeader>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        foreach (var line in entity.VoucherInventoryLines)
        {
            line.CreatedBy = entity.CreatedBy;
            line.CreatedOn = entity.CreatedOn;
            line.IsActive = true;
        }

        await _repository.AddAsync(entity);

        var result = _mapper.Map<VoucherInventoryHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory added successfully HeaderId: {HeaderId}.", nameof(VoucherInventoryService), nameof(AddVoucherInventory), entity.VoucherInventoryHeaderId);

        return ResponseInfo<VoucherInventoryHeaderDto?>.Success(result, HttpStatusCode.OK, "VoucherInventory added successfully.");
    }

    public async Task<ResponseInfo<VoucherInventoryHeaderDto?>> UpdateVoucherInventory(UpdateVoucherInventoryHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating VoucherInventory Id: {HeaderId}", dto.VoucherInventoryHeaderId);

        var entity = await _repository.GetByIdWithLinesAsync(dto.VoucherInventoryHeaderId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory not found Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(UpdateVoucherInventory), dto.VoucherInventoryHeaderId);
            return ResponseInfo<VoucherInventoryHeaderDto?>.Failure("VoucherInventory not found.", HttpStatusCode.NotFound);
        }

        _mapper.Map(dto, entity);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        var dtoLineIds = new HashSet<long>(dto.VoucherInventoryLines.Where(l => l.VoucherInventoryLineId > 0).Select(l => l.VoucherInventoryLineId));

        foreach (var lineDto in dto.VoucherInventoryLines)
        {
            if (lineDto.VoucherInventoryLineId > 0)
            {
                var existingLine = entity.VoucherInventoryLines.FirstOrDefault(l => l.VoucherInventoryLineId == lineDto.VoucherInventoryLineId);

                if (existingLine != null)
                {
                    _mapper.Map(lineDto, existingLine);
                    existingLine.IsActive = true;
                    continue;
                }
            }

            var newLine = _mapper.Map<VoucherInventoryLine>(lineDto);
            newLine.CreatedBy = _currentUserService.UserId ?? 0;
            newLine.CreatedOn = DateTime.UtcNow;
            newLine.IsActive = true;
            entity.VoucherInventoryLines.Add(newLine);
        }

        foreach (var removedLine in entity.VoucherInventoryLines.Where(l => !dtoLineIds.Contains(l.VoucherInventoryLineId)).ToList())
        {
            removedLine.IsActive = false;
        }

        await _repository.SaveAsync();

        var result = _mapper.Map<VoucherInventoryHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory updated Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(UpdateVoucherInventory), dto.VoucherInventoryHeaderId);

        return ResponseInfo<VoucherInventoryHeaderDto?>.Success(result, HttpStatusCode.OK, "VoucherInventory updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteVoucherInventory(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting VoucherInventory Id: {HeaderId}", headerId);

        var entity = await _repository.GetByIdWithLinesAsync(headerId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory not found Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(DeleteVoucherInventory), headerId);
            return ResponseInfo<bool>.Failure("VoucherInventory not found.", HttpStatusCode.NotFound);
        }

        await _repository.SoftDeleteWithLinesAsync(headerId);

        _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory deleted Id: {HeaderId}.", nameof(VoucherInventoryService), nameof(DeleteVoucherInventory), headerId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "VoucherInventory deleted successfully.");
    }

    public async Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking VoucherInventory existence HeaderId: {HeaderId}", headerId);

        var result = await _repository.IsExistByIdAsync(headerId, nameof(VoucherInventoryHeader.VoucherInventoryHeaderId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: VoucherInventory exists: {Result} HeaderId: {HeaderId}.", nameof(VoucherInventoryService), nameof(IsExist), result, headerId);

        return ResponseInfo<bool>.Success(result, HttpStatusCode.OK, result ? "VoucherInventory exists." : "VoucherInventory does not exist.");
    }
}