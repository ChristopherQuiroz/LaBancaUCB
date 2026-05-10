using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

public class SesioneMenu : Profile
{
    public SesioneMenu()
    {
        CreateMap<Sesione, SesioneDto>()
            .ForMember(dest => dest.CreadoEn,
                opt => opt.MapFrom(src => src.CreadoEn.HasValue
                    ? src.CreadoEn.Value.ToString("dd/MM/yyyy")
                    : null));
        CreateMap<SesioneDto, Sesione>();
    }
}
