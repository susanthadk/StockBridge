using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Accounts;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountService> _logger;

    public AccountService(
        IRepository<Account> accountRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<AccountService> logger)
    {
        _accountRepository = accountRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Accounts.");

        var result = await _accountRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Accounts found.", nameof(AccountService), nameof(GetAllAccounts));
            return ResponseInfo<List<AccountDto>?>.Success(new List<AccountDto>(), HttpStatusCode.NoContent, "No Accounts found.");
        }

        var dtos = _mapper.Map<List<AccountDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Accounts.", nameof(AccountService), nameof(GetAllAccounts), dtos.Count);

        return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Accounts retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Accounts. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _accountRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Accounts found.", nameof(AccountService), nameof(GetAllAccounts));
            return ResponseInfo<List<AccountDto>?>.Success(new List<AccountDto>(), HttpStatusCode.NoContent, "No Accounts found.");
        }

        var dtos = _mapper.Map<List<AccountDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Accounts.", nameof(AccountService), nameof(GetAllAccounts), dtos.Count);

        return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Accounts retrieved successfully.");
    }

    public async Task<ResponseInfo<AccountDto?>> GetAccountById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Account Id: {AccountId}", id);

        var result = await _accountRepository.GetByIdAsync(id, nameof(Account.AccountId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Account not found Id: {AccountId}.", nameof(AccountService), nameof(GetAccountById), id);
            return ResponseInfo<AccountDto?>.Success(null, HttpStatusCode.NoContent, "Account not found.");
        }

        var dto = _mapper.Map<AccountDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Account Id: {AccountId}.", nameof(AccountService), nameof(GetAccountById), id);

        return ResponseInfo<AccountDto?>.Success(dto, HttpStatusCode.OK, "Account retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AccountDto>?>> SearchAccount(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Accounts by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _accountRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Accounts found.", nameof(AccountService), nameof(SearchAccount));
            return ResponseInfo<List<AccountDto>?>.Success(new List<AccountDto>(), HttpStatusCode.NoContent, "No Accounts found.");
        }

        var dtos = _mapper.Map<List<AccountDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Accounts.", nameof(AccountService), nameof(SearchAccount), dtos.Count);

        return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Accounts retrieved successfully.");
    }

    public async Task<ResponseInfo<AccountDto?>> AddAccount(AccountDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Account.");

        var existing = await _accountRepository.GetByFieldAsync("AccountNumber", dto.AccountNumber);
        if (existing?.Any(x => x.SubCode == dto.SubCode) == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Account already exists with the same Account Number and Sub Code.", nameof(AccountService), nameof(AddAccount));
            return ResponseInfo<AccountDto?>.Failure("Account already exists with the same Account Number and Sub Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Account>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _accountRepository.AddAsync(entity);

        var resultDto = _mapper.Map<AccountDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Account added successfully AccountId: {AccountId}.", nameof(AccountService), nameof(AddAccount), result.AccountId);

        return ResponseInfo<AccountDto?>.Success(resultDto, HttpStatusCode.Created, "Account added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateAccount(AccountDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Account Id: {AccountId}", dto.AccountId);

        var isExists = await _accountRepository.IsExistByIdAsync(dto.AccountId, nameof(Account.AccountId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Account not found Id: {AccountId}.", nameof(AccountService), nameof(UpdateAccount), dto.AccountId);
            return ResponseInfo<bool>.Failure("Account not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Account>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _accountRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Account updated Id: {AccountId}.", nameof(AccountService), nameof(UpdateAccount), dto.AccountId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Account updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteAccount(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Account Id: {AccountId}", id);

        var isExists = await _accountRepository.IsExistByIdAsync(id, nameof(Account.AccountId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Account not found Id: {AccountId}.", nameof(AccountService), nameof(DeleteAccount), id);
            return ResponseInfo<bool>.Failure("Account not found.", HttpStatusCode.NotFound);
        }

        await _accountRepository.DeleteAsync(id, nameof(Account.AccountId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Account deleted Id: {AccountId}.", nameof(AccountService), nameof(DeleteAccount), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Account deleted successfully.");
    }
}