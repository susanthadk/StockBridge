using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AccountInformations;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class AccountInformationService : IAccountInformationService
{
    private readonly IRepository<AccountInformation> _accountInformationRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountInformationService> _logger;

    public AccountInformationService(
        IRepository<AccountInformation> accountInformationRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<AccountInformationService> logger)
    {
        _accountInformationRepository = accountInformationRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<AccountInformationDto>?>> GetAllAccountInformations(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all AccountInformations.");

        var result = await _accountInformationRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AccountInformations found.", nameof(AccountInformationService), nameof(GetAllAccountInformations));
            return ResponseInfo<List<AccountInformationDto>?>.Success(new List<AccountInformationDto>(), HttpStatusCode.NoContent, "No AccountInformations found.");
        }

        var dtos = _mapper.Map<List<AccountInformationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AccountInformations.", nameof(AccountInformationService), nameof(GetAllAccountInformations), dtos.Count);

        return ResponseInfo<List<AccountInformationDto>?>.Success(dtos, HttpStatusCode.OK, "AccountInformations retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AccountInformationDto>?>> GetAllAccountInformations(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all AccountInformations. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _accountInformationRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AccountInformations found.", nameof(AccountInformationService), nameof(GetAllAccountInformations));
            return ResponseInfo<List<AccountInformationDto>?>.Success(new List<AccountInformationDto>(), HttpStatusCode.NoContent, "No AccountInformations found.");
        }

        var dtos = _mapper.Map<List<AccountInformationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AccountInformations.", nameof(AccountInformationService), nameof(GetAllAccountInformations), dtos.Count);

        return ResponseInfo<List<AccountInformationDto>?>.Success(dtos, HttpStatusCode.OK, "AccountInformations retrieved successfully.");
    }

    public async Task<ResponseInfo<AccountInformationDto?>> GetAccountInformationById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching AccountInformation Id: {AccountInformationId}", id);

        var result = await _accountInformationRepository.GetByIdAsync(id, nameof(AccountInformation.AccountInformationId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation not found Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(GetAccountInformationById), id);
            return ResponseInfo<AccountInformationDto?>.Success(null, HttpStatusCode.NoContent, "AccountInformation not found.");
        }

        var dto = _mapper.Map<AccountInformationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved AccountInformation Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(GetAccountInformationById), id);

        return ResponseInfo<AccountInformationDto?>.Success(dto, HttpStatusCode.OK, "AccountInformation retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AccountInformationDto>?>> SearchAccountInformation(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching AccountInformations by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _accountInformationRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AccountInformations found.", nameof(AccountInformationService), nameof(SearchAccountInformation));
            return ResponseInfo<List<AccountInformationDto>?>.Success(new List<AccountInformationDto>(), HttpStatusCode.NoContent, "No AccountInformations found.");
        }

        var dtos = _mapper.Map<List<AccountInformationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AccountInformations.", nameof(AccountInformationService), nameof(SearchAccountInformation), dtos.Count);

        return ResponseInfo<List<AccountInformationDto>?>.Success(dtos, HttpStatusCode.OK, "AccountInformations retrieved successfully.");
    }

    public async Task<ResponseInfo<AccountInformationDto?>> AddAccountInformation(AccountInformationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding AccountInformation.");

        var entity = _mapper.Map<AccountInformation>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _accountInformationRepository.AddAsync(entity);

        var resultDto = _mapper.Map<AccountInformationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation added successfully AccountInformationId: {AccountInformationId}.", nameof(AccountInformationService), nameof(AddAccountInformation), result.AccountInformationId);

        return ResponseInfo<AccountInformationDto?>.Success(resultDto, HttpStatusCode.Created, "AccountInformation added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateAccountInformation(AccountInformationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating AccountInformation Id: {AccountInformationId}", dto.AccountInformationId);

        var isExists = await _accountInformationRepository.IsExistByIdAsync(dto.AccountInformationId, nameof(AccountInformation.AccountInformationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation not found Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(UpdateAccountInformation), dto.AccountInformationId);
            return ResponseInfo<bool>.Failure("AccountInformation not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<AccountInformation>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _accountInformationRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation updated Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(UpdateAccountInformation), dto.AccountInformationId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "AccountInformation updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteAccountInformation(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting AccountInformation Id: {AccountInformationId}", id);

        var isExists = await _accountInformationRepository.IsExistByIdAsync(id, nameof(AccountInformation.AccountInformationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation not found Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(DeleteAccountInformation), id);
            return ResponseInfo<bool>.Failure("AccountInformation not found.", HttpStatusCode.NotFound);
        }

        await _accountInformationRepository.DeleteAsync(id, nameof(AccountInformation.AccountInformationId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: AccountInformation deleted Id: {AccountInformationId}.", nameof(AccountInformationService), nameof(DeleteAccountInformation), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "AccountInformation deleted successfully.");
    }
}