using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Formulas;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class FormulaService : IFormulaService
{
    private readonly IFormulaRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<FormulaService> _logger;

    public FormulaService(
        IFormulaRepository repository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<FormulaService> logger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<FormulaHeaderDto>?>> GetAllFormulas(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Formulas.");

        var result = await _repository.GetAllWithLinesAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Formulas found.", nameof(FormulaService), nameof(GetAllFormulas));
            return ResponseInfo<List<FormulaHeaderDto>?>.Success(new List<FormulaHeaderDto>(), HttpStatusCode.NoContent, "No Formulas found.");
        }

        var dtos = _mapper.Map<List<FormulaHeaderDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Formulas.", nameof(FormulaService), nameof(GetAllFormulas), dtos.Count);

        return ResponseInfo<List<FormulaHeaderDto>?>.Success(dtos, HttpStatusCode.OK, "Formulas retrieved successfully.");
    }

    public async Task<ResponseInfo<FormulaHeaderDto?>> GetFormulaById(int headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Formula Id: {HeaderId}", headerId);

        var result = await _repository.GetByIdWithLinesAsync(headerId);

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Formula not found Id: {HeaderId}.", nameof(FormulaService), nameof(GetFormulaById), headerId);
            return ResponseInfo<FormulaHeaderDto?>.Success(null, HttpStatusCode.NoContent, "Formula not found.");
        }

        var dto = _mapper.Map<FormulaHeaderDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Formula Id: {HeaderId}.", nameof(FormulaService), nameof(GetFormulaById), headerId);

        return ResponseInfo<FormulaHeaderDto?>.Success(dto, HttpStatusCode.OK, "Formula retrieved successfully.");
    }

    public async Task<ResponseInfo<FormulaHeaderDto?>> AddFormula(CreateFormulaHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Formula.");

        if (dto.FormulaLines == null || !dto.FormulaLines.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Formula cannot be added without lines.", nameof(FormulaService), nameof(AddFormula));
            return ResponseInfo<FormulaHeaderDto?>.Failure("Formula must contain at least one line.", HttpStatusCode.BadRequest);
        }

        var existing = await _repository.GetByFieldAsync("FormulaNumber", dto.FormulaNumber);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Formula already exists with the same Formula Number.", nameof(FormulaService), nameof(AddFormula));
            return ResponseInfo<FormulaHeaderDto?>.Failure("Formula already exists with the same Formula Number.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<FormulaHeader>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        foreach (var line in entity.FormulaLines)
        {
            line.CreatedBy = entity.CreatedBy;
            line.CreatedOn = entity.CreatedOn;
            line.IsActive = true;
        }

        await _repository.AddAsync(entity);

        var result = _mapper.Map<FormulaHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Formula added successfully HeaderId: {HeaderId}.", nameof(FormulaService), nameof(AddFormula), entity.FormulaHeaderId);

        return ResponseInfo<FormulaHeaderDto?>.Success(result, HttpStatusCode.OK, "Formula added successfully.");
    }

    public async Task<ResponseInfo<FormulaHeaderDto?>> UpdateFormula(UpdateFormulaHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Formula Id: {HeaderId}", dto.FormulaHeaderId);

        var entity = await _repository.GetByIdWithLinesAsync(dto.FormulaHeaderId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Formula not found Id: {HeaderId}.", nameof(FormulaService), nameof(UpdateFormula), dto.FormulaHeaderId);
            return ResponseInfo<FormulaHeaderDto?>.Failure("Formula not found.", HttpStatusCode.NotFound);
        }

        _mapper.Map(dto, entity);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        var dtoLineIds = new HashSet<int>(dto.FormulaLines.Where(l => l.FormulaLineId > 0).Select(l => l.FormulaLineId));

        foreach (var lineDto in dto.FormulaLines)
        {
            if (lineDto.FormulaLineId > 0)
            {
                var existingLine = entity.FormulaLines.FirstOrDefault(l => l.FormulaLineId == lineDto.FormulaLineId);

                if (existingLine != null)
                {
                    _mapper.Map(lineDto, existingLine);
                    existingLine.IsActive = true;
                    continue;
                }
            }

            var newLine = _mapper.Map<FormulaLine>(lineDto);
            newLine.CreatedBy = _currentUserService.UserId ?? 0;
            newLine.CreatedOn = DateTime.UtcNow;
            newLine.IsActive = true;
            entity.FormulaLines.Add(newLine);
        }

        foreach (var removedLine in entity.FormulaLines.Where(l => !dtoLineIds.Contains(l.FormulaLineId)).ToList())
        {
            removedLine.IsActive = false;
        }

        await _repository.SaveAsync();

        var result = _mapper.Map<FormulaHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Formula updated Id: {HeaderId}.", nameof(FormulaService), nameof(UpdateFormula), dto.FormulaHeaderId);

        return ResponseInfo<FormulaHeaderDto?>.Success(result, HttpStatusCode.OK, "Formula updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteFormula(int headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Formula Id: {HeaderId}", headerId);

        var entity = await _repository.GetByIdWithLinesAsync(headerId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Formula not found Id: {HeaderId}.", nameof(FormulaService), nameof(DeleteFormula), headerId);
            return ResponseInfo<bool>.Failure("Formula not found.", HttpStatusCode.NotFound);
        }

        await _repository.SoftDeleteWithLinesAsync(headerId);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Formula deleted Id: {HeaderId}.", nameof(FormulaService), nameof(DeleteFormula), headerId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Formula deleted successfully.");
    }

    public async Task<ResponseInfo<bool>> IsExist(int headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking Formula existence HeaderId: {HeaderId}", headerId);

        var result = await _repository.IsExistByIdAsync(headerId, nameof(FormulaHeader.FormulaHeaderId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Formula exists: {Result} HeaderId: {HeaderId}.", nameof(FormulaService), nameof(IsExist), result, headerId);

        return ResponseInfo<bool>.Success(result, HttpStatusCode.OK, result ? "Formula exists." : "Formula does not exist.");
    }
}