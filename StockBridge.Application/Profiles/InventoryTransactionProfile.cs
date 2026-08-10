using AutoMapper;
using StockBridge.Application.DTOs.InventoryTransactions;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class InventoryTransactionProfile : Profile
{
    public InventoryTransactionProfile()
    {
        CreateMap<InventoryHeaderTransaction, InventoryHeaderTransactionDto>();
        CreateMap<InventoryLineTransaction, InventoryLineTransactionDto>();
        CreateMap<CreateInventoryHeaderTransactionDto, InventoryHeaderTransaction>();
        CreateMap<CreateInventoryLineTransactionDto, InventoryLineTransaction>();
        CreateMap<UpdateInventoryHeaderTransactionDto, InventoryHeaderTransaction>()
            .ForMember(dest => dest.InventoryLineTransactions, opt => opt.Ignore());
        CreateMap<UpdateInventoryLineTransactionDto, InventoryLineTransaction>();
    }
}