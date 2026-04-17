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

        CreateMap<TransactionRequestDto, Transaccion>()
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Amount));

        CreateMap<Transaccion, TransactionRequestDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Monto))
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.IdTransaccion.ToString()))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.FechaTransaccion, opt => opt.MapFrom(src => src.FechaHora.ToString("o")));
    }
}