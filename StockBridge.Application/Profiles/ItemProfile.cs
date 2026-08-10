using AutoMapper;
using StockBridge.Application.DTOs.Items;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<Item, ItemDto>();
        CreateMap<ItemDto, Item>();
    }
}