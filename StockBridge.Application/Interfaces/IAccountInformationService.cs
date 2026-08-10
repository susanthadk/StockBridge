using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AccountInformations;

namespace StockBridge.Application.Interfaces;

public interface IAccountInformationService
{
    Task<ResponseInfo<List<AccountInformationDto>?>> GetAllAccountInformations(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<AccountInformationDto>?>> GetAllAccountInformations(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<AccountInformationDto?>> GetAccountInformationById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<AccountInformationDto>?>> SearchAccountInformation(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<AccountInformationDto?>> AddAccountInformation(AccountInformationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateAccountInformation(AccountInformationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteAccountInformation(int id, CancellationToken cancellationToken = default);
}