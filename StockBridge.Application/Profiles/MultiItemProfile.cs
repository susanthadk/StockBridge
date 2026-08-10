using AutoMapper;
using StockBridge.Application.DTOs.MultiItems;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class MultiItemProfile : Profile
{
    public MultiItemProfile()
    {
        CreateMap<MultiItem, MultiItemDto>();
        CreateMap<MultiItemDto, MultiItem>();
    }
}