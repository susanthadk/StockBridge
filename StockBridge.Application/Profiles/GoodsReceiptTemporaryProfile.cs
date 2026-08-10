using AutoMapper;
using StockBridge.Application.DTOs.GoodsReceiptTemporaries;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class GoodsReceiptTemporaryProfile : Profile
{
    public GoodsReceiptTemporaryProfile()
    {
        CreateMap<GoodsReceiptTemporaryHeader, GoodsReceiptTemporaryHeaderDto>();
        CreateMap<GoodsReceiptTemporaryDetail, GoodsReceiptTemporaryDetailDto>();
        CreateMap<CreateGoodsReceiptTemporaryHeaderDto, GoodsReceiptTemporaryHeader>();
        CreateMap<CreateGoodsReceiptTemporaryDetailDto, GoodsReceiptTemporaryDetail>();
        CreateMap<UpdateGoodsReceiptTemporaryHeaderDto, GoodsReceiptTemporaryHeader>()
            .ForMember(dest => dest.GoodsReceiptTemporaryDetails, opt => opt.Ignore());
        CreateMap<UpdateGoodsReceiptTemporaryDetailDto, GoodsReceiptTemporaryDetail>();
    }
}