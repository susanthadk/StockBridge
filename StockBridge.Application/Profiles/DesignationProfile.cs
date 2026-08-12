using AutoMapper;
using StockBridge.Application.DTOs.Designations;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class DesignationProfile : Profile
{
    public DesignationProfile()
    {
        CreateMap<Designation, DesignationDto>();
        CreateMap<DesignationDto, Designation>();
    }
}