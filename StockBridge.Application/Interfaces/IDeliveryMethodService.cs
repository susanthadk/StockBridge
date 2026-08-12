using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DeliveryMethods;

namespace StockBridge.Application.Interfaces;

public interface IDeliveryMethodService
{
    Task<ResponseInfo<List<DeliveryMethodDto>?>> GetAllDeliveryMethods(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DeliveryMethodDto>?>> GetAllDeliveryMethods(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DeliveryMethodDto?>> GetDeliveryMethodById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DeliveryMethodDto>?>> SearchDeliveryMethod(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DeliveryMethodDto?>> AddDeliveryMethod(DeliveryMethodDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateDeliveryMethod(DeliveryMethodDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteDeliveryMethod(int id, CancellationToken cancellationToken = default);
}
