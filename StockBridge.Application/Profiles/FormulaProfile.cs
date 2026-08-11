using AutoMapper;
using StockBridge.Application.DTOs.Formulas;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class FormulaProfile : Profile
{
    public FormulaProfile()
    {
        CreateMap<FormulaHeader, FormulaHeaderDto>();
        CreateMap<FormulaLine, FormulaLineDto>();
        CreateMap<CreateFormulaHeaderDto, FormulaHeader>();
        CreateMap<CreateFormulaLineDto, FormulaLine>();
        CreateMap<UpdateFormulaHeaderDto, FormulaHeader>()
            .ForMember(dest => dest.FormulaLines, opt => opt.Ignore());
        CreateMap<UpdateFormulaLineDto, FormulaLine>();
    }
}