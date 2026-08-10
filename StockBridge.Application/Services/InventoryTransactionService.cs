using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.InventoryTransactions;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class InventoryTransactionService : IInventoryTransactionService
{
    private readonly IInventoryTransactionRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<InventoryTransactionService> _logger;

    public InventoryTransactionService(
        IInventoryTransactionRepository repository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<InventoryTransactionService> logger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<InventoryHeaderTransactionDto>?>> GetAllInventoryTransactions(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all InventoryTransactions.");

        var result = await _repository.GetAllWithLinesAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No InventoryTransactions found.", nameof(InventoryTransactionService), nameof(GetAllInventoryTransactions));
            return ResponseInfo<List<InventoryHeaderTransactionDto>?>.Success(new List<InventoryHeaderTransactionDto>(), HttpStatusCode.NoContent, "No InventoryTransactions found.");
        }

        var dtos = _mapper.Map<List<InventoryHeaderTransactionDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} InventoryTransactions.", nameof(InventoryTransactionService), nameof(GetAllInventoryTransactions), dtos.Count);

        return ResponseInfo<List<InventoryHeaderTransactionDto>?>.Success(dtos, HttpStatusCode.OK, "InventoryTransactions retrieved successfully.");
    }

    public async Task<ResponseInfo<InventoryHeaderTransactionDto?>> GetInventoryTransactionById(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching InventoryTransaction Id: {HeaderId}", headerId);

        var result = await _repository.GetByIdWithLinesAsync(headerId);

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction not found Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(GetInventoryTransactionById), headerId);
            return ResponseInfo<InventoryHeaderTransactionDto?>.Success(null, HttpStatusCode.NoContent, "InventoryTransaction not found.");
        }

        var dto = _mapper.Map<InventoryHeaderTransactionDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved InventoryTransaction Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(GetInventoryTransactionById), headerId);

        return ResponseInfo<InventoryHeaderTransactionDto?>.Success(dto, HttpStatusCode.OK, "InventoryTransaction retrieved successfully.");
    }

    public async Task<ResponseInfo<InventoryHeaderTransactionDto?>> AddInventoryTransaction(CreateInventoryHeaderTransactionDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding InventoryTransaction.");

        if (dto.InventoryLineTransactions == null || !dto.InventoryLineTransactions.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction cannot be added without lines.", nameof(InventoryTransactionService), nameof(AddInventoryTransaction));
            return ResponseInfo<InventoryHeaderTransactionDto?>.Failure("InventoryTransaction must contain at least one line.", HttpStatusCode.BadRequest);
        }

        var isExists = await _repository.IsExistByBusinessKeyAsync(dto.InventoryHeaderType, dto.InventoryHeaderDocumentNumber, dto.InventoryHeaderDate, dto.InventoryHeaderOperationCode, dto.TerminalNumber);
        if (isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction already exists with the same business key.", nameof(InventoryTransactionService), nameof(AddInventoryTransaction));
            return ResponseInfo<InventoryHeaderTransactionDto?>.Failure("InventoryTransaction already exists with the same Type, Document Number, Date, Operation Code and Terminal Number.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<InventoryHeaderTransaction>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        foreach (var line in entity.InventoryLineTransactions)
        {
            line.CreatedBy = entity.CreatedBy;
            line.CreatedOn = entity.CreatedOn;
            line.IsActive = true;
        }

        await _repository.AddAsync(entity);

        var result = _mapper.Map<InventoryHeaderTransactionDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction added successfully HeaderId: {HeaderId}.", nameof(InventoryTransactionService), nameof(AddInventoryTransaction), entity.InventoryHeaderTransactionId);

        return ResponseInfo<InventoryHeaderTransactionDto?>.Success(result, HttpStatusCode.OK, "InventoryTransaction added successfully.");
    }

    public async Task<ResponseInfo<InventoryHeaderTransactionDto?>> UpdateInventoryTransaction(UpdateInventoryHeaderTransactionDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating InventoryTransaction Id: {HeaderId}", dto.InventoryHeaderTransactionId);

        var entity = await _repository.GetByIdWithLinesAsync(dto.InventoryHeaderTransactionId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction not found Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(UpdateInventoryTransaction), dto.InventoryHeaderTransactionId);
            return ResponseInfo<InventoryHeaderTransactionDto?>.Failure("InventoryTransaction not found.", HttpStatusCode.NotFound);
        }

        _mapper.Map(dto, entity);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        var dtoLineIds = new HashSet<long>(dto.InventoryLineTransactions.Where(l => l.InventoryLineTransactionId > 0).Select(l => l.InventoryLineTransactionId));

        foreach (var lineDto in dto.InventoryLineTransactions)
        {
            if (lineDto.InventoryLineTransactionId > 0)
            {
                var existingLine = entity.InventoryLineTransactions.FirstOrDefault(l => l.InventoryLineTransactionId == lineDto.InventoryLineTransactionId);

                if (existingLine != null)
                {
                    _mapper.Map(lineDto, existingLine);
                    existingLine.IsActive = true;
                    continue;
                }
            }

            var newLine = _mapper.Map<InventoryLineTransaction>(lineDto);
            newLine.CreatedBy = _currentUserService.UserId ?? 0;
            newLine.CreatedOn = DateTime.UtcNow;
            newLine.IsActive = true;
            entity.InventoryLineTransactions.Add(newLine);
        }

        foreach (var removedLine in entity.InventoryLineTransactions.Where(l => !dtoLineIds.Contains(l.InventoryLineTransactionId)).ToList())
        {
            removedLine.IsActive = false;
        }

        await _repository.SaveAsync();

        var result = _mapper.Map<InventoryHeaderTransactionDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction updated Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(UpdateInventoryTransaction), dto.InventoryHeaderTransactionId);

        return ResponseInfo<InventoryHeaderTransactionDto?>.Success(result, HttpStatusCode.OK, "InventoryTransaction updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteInventoryTransaction(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting InventoryTransaction Id: {HeaderId}", headerId);

        var entity = await _repository.GetByIdWithLinesAsync(headerId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction not found Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(DeleteInventoryTransaction), headerId);
            return ResponseInfo<bool>.Failure("InventoryTransaction not found.", HttpStatusCode.NotFound);
        }

        await _repository.SoftDeleteWithLinesAsync(headerId);

        _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction deleted Id: {HeaderId}.", nameof(InventoryTransactionService), nameof(DeleteInventoryTransaction), headerId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "InventoryTransaction deleted successfully.");
    }

    public async Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking InventoryTransaction existence HeaderId: {HeaderId}", headerId);

        var result = await _repository.IsExistByIdAsync(headerId, nameof(InventoryHeaderTransaction.InventoryHeaderTransactionId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: InventoryTransaction exists: {Result} HeaderId: {HeaderId}.", nameof(InventoryTransactionService), nameof(IsExist), result, headerId);

        return ResponseInfo<bool>.Success(result, HttpStatusCode.OK, result ? "InventoryTransaction exists." : "InventoryTransaction does not exist.");
    }
}