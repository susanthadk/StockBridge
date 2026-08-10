using AutoMapper;
using StockBridge.Application.DTOs.MainLocations;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class MainLocationProfile : Profile
{
    public MainLocationProfile()
    {
        CreateMap<MainLocation, MainLocationDto>();
        CreateMap<MainLocationDto, MainLocation>();
    }
}