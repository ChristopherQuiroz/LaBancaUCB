using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

public class TransaccionMenu : Profile
{
    public TransaccionMenu()
    {
        CreateMap<Transaccion, TransaccionDto>()
            .ForMember(dest => dest.FechaHora,
                opt => opt.MapFrom(src => src.FechaHora.ToString("dd/MM/yyyy")));
        CreateMap<TransaccionDto, Transaccion>();
    }
}
