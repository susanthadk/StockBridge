using AutoMapper;
using StockBridge.Application.DTOs.VoucherInventories;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class VoucherInventoryProfile : Profile
{
    public VoucherInventoryProfile()
    {
        CreateMap<VoucherInventoryHeader, VoucherInventoryHeaderDto>();
        CreateMap<VoucherInventoryLine, VoucherInventoryLineDto>();
        CreateMap<CreateVoucherInventoryHeaderDto, VoucherInventoryHeader>();
        CreateMap<CreateVoucherInventoryLineDto, VoucherInventoryLine>();
        CreateMap<UpdateVoucherInventoryHeaderDto, VoucherInventoryHeader>()
            .ForMember(dest => dest.VoucherInventoryLines, opt => opt.Ignore());
        CreateMap<UpdateVoucherInventoryLineDto, VoucherInventoryLine>();
    }
}