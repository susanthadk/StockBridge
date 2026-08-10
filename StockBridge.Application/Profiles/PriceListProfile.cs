using AutoMapper;
using StockBridge.Application.DTOs.PriceLists;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class PriceListProfile : Profile
{
    public PriceListProfile()
    {
        CreateMap<PriceList, PriceListDto>();
        CreateMap<PriceListDto, PriceList>();
    }
}