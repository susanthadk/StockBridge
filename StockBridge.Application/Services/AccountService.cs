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

    public async Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(CancellationToken cancellation = default)
    {
        try
        {
            _logger.LogInformation("Fetching all Accounts...");
            var result = await _accountRepository.GetAllAsync();
            var accounts = result?.ToList();
            if (accounts == null || accounts.Count == 0)
            {
                _logger.LogWarning("No Account found.");
                return ResponseInfo<List<AccountDto>?>.Failure("No Account found.", HttpStatusCode.NotFound);
            }
            var dtos = _mapper.Map<List<AccountDto>>(accounts);
            _logger.LogInformation($"{dtos.Count} Account(s) retrieved successfully.");
            return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Account retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Account");
            return ResponseInfo<List<AccountDto>?>.Failure("An error occurred while retrieving Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(int pageNo, int pageSize, CancellationToken cancellation = default)
    {
        try
        {
            _logger.LogInformation($"Fetching all Accounts... page: {pageNo}, size: {pageSize}");
            var result = await _accountRepository.GetPagedAsync(pageNo, pageSize);
            var accounts = result?.ToList();
            if (accounts == null || accounts.Count == 0)
            {
                _logger.LogWarning("No Account found.");
                return ResponseInfo<List<AccountDto>?>.Failure("No Account found.", HttpStatusCode.NotFound);
            }
            var dtos = _mapper.Map<List<AccountDto>>(accounts);
            _logger.LogInformation($"{dtos.Count} Account(s) retrieved successfully.");
            return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Account retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Account");
            return ResponseInfo<List<AccountDto>?>.Failure("An error occurred while retrieving Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<AccountDto?>> GetAccountById(int id, CancellationToken cancellation = default)
    {
        try
        {
            _logger.LogInformation("Fetching Account by Id...");
            var result = await _accountRepository.GetByIdAsync(id, "AccountId");
            if (result == null)
            {
                _logger.LogWarning("No Account found.");
                return ResponseInfo<AccountDto?>.Failure("No Account found.", HttpStatusCode.NotFound);
            }
            var dto = _mapper.Map<AccountDto>(result);
            _logger.LogInformation("Account retrieved successfully.");
            return ResponseInfo<AccountDto?>.Success(dto, HttpStatusCode.OK, "Account retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Account");
            return ResponseInfo<AccountDto?>.Failure("An error occurred while retrieving Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<List<AccountDto>?>> SearchAccount(string fieldName, string searchString, CancellationToken cancellation = default)
    {
        try
        {
            _logger.LogInformation($"Fetching Account by {fieldName}...");
            var result = await _accountRepository.GetByFieldAsync(fieldName, searchString);
            var accounts = result?.ToList();
            if (accounts == null || accounts.Count == 0)
            {
                _logger.LogWarning("No Account found.");
                return ResponseInfo<List<AccountDto>?>.Failure("No Account found.", HttpStatusCode.NotFound);
            }
            var dtos = _mapper.Map<List<AccountDto>>(accounts);
            _logger.LogInformation($"{dtos.Count} Account(s) retrieved successfully.");
            return ResponseInfo<List<AccountDto>?>.Success(dtos, HttpStatusCode.OK, "Account retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Account");
            return ResponseInfo<List<AccountDto>?>.Failure("An error occurred while searching Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<AccountDto?>> AddAccount(AccountDto dto, CancellationToken cancellation = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        try
        {
            _logger.LogInformation("Creating new Account...");

            var existing = await _accountRepository.GetByFieldAsync("AccountNumber", dto.AccountNumber);
            if (existing?.Any(x => x.SubCode == dto.SubCode) == true)
            {
                _logger.LogWarning("Account already exists with the same Account Number and Sub Code.");
                return ResponseInfo<AccountDto?>.Failure("Account already exists with the same Account Number and Sub Code.", HttpStatusCode.BadRequest);
            }

            var entity = _mapper.Map<Account>(dto);
            entity.CreatedBy = _currentUserService.UserId ?? 0;
            entity.CreatedOn = DateTime.UtcNow;
            entity.IsActive = true;

            var result = await _accountRepository.AddAsync(entity);
            if (result == null)
            {
                _logger.LogWarning("Account not created.");
                return ResponseInfo<AccountDto?>.Failure("Account not created.", HttpStatusCode.BadRequest);
            }

            var resultDto = _mapper.Map<AccountDto>(result);
            _logger.LogInformation("Account created successfully.");
            return ResponseInfo<AccountDto?>.Success(resultDto, HttpStatusCode.Created, "Account created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Account");
            return ResponseInfo<AccountDto?>.Failure("An error occurred while creating Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<bool>> UpdateAccount(AccountDto dto, CancellationToken cancellation = default)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        try
        {
            _logger.LogInformation("Updating Account...");
            var isExists = await _accountRepository.IsExistByIdAsync(dto.AccountId, "AccountId");
            if (!isExists)
            {
                _logger.LogWarning("Selected Account not found.");
                return ResponseInfo<bool>.Failure("Selected Account not found.", HttpStatusCode.NotFound);
            }

            var entity = _mapper.Map<Account>(dto);
            entity.ModifiedBy = _currentUserService.UserId;
            entity.ModifiedOn = DateTime.UtcNow;

            await _accountRepository.UpdateAsync(entity);
            _logger.LogInformation("Account updated successfully.");
            return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Account updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Account");
            return ResponseInfo<bool>.Failure("An error occurred while updating Account.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseInfo<bool>> DeleteAccount(int id, CancellationToken cancellation = default)
    {
        try
        {
            _logger.LogInformation("Deleting Account...");
            var isExists = await _accountRepository.IsExistByIdAsync(id, "AccountId");
            if (!isExists)
            {
                _logger.LogWarning("Selected Account not found.");
                return ResponseInfo<bool>.Failure("Selected Account not found.", HttpStatusCode.NotFound);
            }

            await _accountRepository.DeleteAsync(id, "AccountId");
            _logger.LogInformation("Account deleted successfully.");
            return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Account deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Account");
            return ResponseInfo<bool>.Failure("An error occurred while deleting Account.", HttpStatusCode.InternalServerError);
        }
    }
}