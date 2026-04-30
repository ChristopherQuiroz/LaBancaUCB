using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

public class CuentaProfile : Profile
{
    public CuentaProfile()
    {
        CreateMap<Cuenta, CuentaDto>()
            .ForMember(dest => dest.FechaApertura,
                opt => opt.MapFrom(src => src.FechaApertura.HasValue
                    ? src.FechaApertura.Value.ToString("dd/MM/yyyy")
                    : null));

        CreateMap<CuentaDto, Cuenta>();
    }
}
