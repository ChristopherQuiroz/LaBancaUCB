using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ForMember(dest => dest.FechaDeCreacion,
                opt => opt.MapFrom(src => src.FechaDeCreacion.HasValue
                    ? src.FechaDeCreacion.Value.ToString("dd/MM/yyyy")
                    : null));

        CreateMap<UsuarioDto, Usuario>();
    }
}