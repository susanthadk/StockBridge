using AutoMapper;
using StockBridge.Application.DTOs.SalesRepresentatives;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class SalesRepresentativeProfile : Profile
{
    public SalesRepresentativeProfile()
    {
        CreateMap<SalesRepresentative, SalesRepresentativeDto>();
        CreateMap<SalesRepresentativeDto, SalesRepresentative>();
    }
}