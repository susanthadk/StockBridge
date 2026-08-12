using AutoMapper;
using StockBridge.Application.DTOs.DeliveryMethods;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class DeliveryMethodProfile : Profile
{
    public DeliveryMethodProfile()
    {
        CreateMap<DeliveryMethod, DeliveryMethodDto>();
        CreateMap<DeliveryMethodDto, DeliveryMethod>();
    }
}
