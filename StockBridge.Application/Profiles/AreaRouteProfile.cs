using AutoMapper;
using StockBridge.Application.DTOs.AreaRoutes;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class AreaRouteProfile : Profile
{
    public AreaRouteProfile()
    {
        CreateMap<AreaRoute, AreaRouteDto>();
        CreateMap<AreaRouteDto, AreaRoute>();
    }
}