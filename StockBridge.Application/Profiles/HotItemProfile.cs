using AutoMapper;
using StockBridge.Application.DTOs.HotItems;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class HotItemProfile : Profile
{
    public HotItemProfile()
    {
        CreateMap<HotItem, HotItemDto>();
        CreateMap<HotItemDto, HotItem>();
    }
}