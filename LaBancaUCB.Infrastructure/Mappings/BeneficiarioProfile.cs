using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

internal class BeneficiarioProfile : Profile
{
    public BeneficiarioProfile()
    {
        CreateMap<Beneficiario, BeneficiarioDto>()
            .ForMember(dest => dest.CreadoEn,
                opt => opt.MapFrom(src => src.CreadoEn.HasValue
                    ? src.CreadoEn.Value.ToString("dd/MM/yyyy")
                    : null));
        CreateMap<BeneficiarioDto, Beneficiario>();
    }
}
