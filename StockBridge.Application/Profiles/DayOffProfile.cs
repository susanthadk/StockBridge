using AutoMapper;
using StockBridge.Application.DTOs.DayOffs;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class DayOffProfile : Profile
{
    public DayOffProfile()
    {
        CreateMap<DayOff, DayOffDto>();
        CreateMap<DayOffDto, DayOff>();
    }
}