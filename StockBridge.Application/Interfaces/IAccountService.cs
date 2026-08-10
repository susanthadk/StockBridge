using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Accounts;

namespace StockBridge.Application.Interfaces;

public interface IAccountService
{
    Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(CancellationToken cancellation = default);
    Task<ResponseInfo<List<AccountDto>?>> GetAllAccounts(int pageNo, int pageSize, CancellationToken cancellation = default);
    Task<ResponseInfo<AccountDto?>> GetAccountById(int id, CancellationToken cancellation = default);
    Task<ResponseInfo<List<AccountDto>?>> SearchAccount(string fieldName, string searchString, CancellationToken cancellation = default);
    Task<ResponseInfo<AccountDto?>> AddAccount(AccountDto dto, CancellationToken cancellation = default);
    Task<ResponseInfo<bool>> UpdateAccount(AccountDto dto, CancellationToken cancellation = default);
    Task<ResponseInfo<bool>> DeleteAccount(int id, CancellationToken cancellation = default);
}