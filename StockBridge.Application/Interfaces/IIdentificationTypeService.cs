using StockBridge.Application.Common;
using StockBridge.Application.DTOs.IdentificationTypes;

namespace StockBridge.Application.Interfaces;

public interface IIdentificationTypeService
{
    Task<ResponseInfo<List<IdentificationTypeDto>?>> GetAllIdentificationTypes(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<IdentificationTypeDto>?>> GetAllIdentificationTypes(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<IdentificationTypeDto?>> GetIdentificationTypeById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<IdentificationTypeDto>?>> SearchIdentificationType(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<IdentificationTypeDto?>> AddIdentificationType(IdentificationTypeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateIdentificationType(IdentificationTypeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteIdentificationType(int id, CancellationToken cancellationToken = default);
}