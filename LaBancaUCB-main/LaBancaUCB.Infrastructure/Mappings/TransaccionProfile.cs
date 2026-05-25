using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Infrastructure.Mappings;

public class TransaccionProfile : Profile
{
    public TransaccionProfile()
    {
        CreateMap<Transaccion, TransaccionDto>()
            .ForMember(dest => dest.FechaHora,
                opt => opt.MapFrom(src => src.FechaHora.ToString("dd/MM/yyyy HH:mm:ss")));

        CreateMap<TransaccionDto, Transaccion>();
    }
}