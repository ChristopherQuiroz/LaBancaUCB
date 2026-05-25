using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface IProductosService
{
    Task SolicitarSeguroAsync(CrearSeguroDto dto, long idUsuario);
    Task SolicitarPrestamoAsync(CrearPrestamoDto dto, long idUsuario);
    Task SolicitarTarjetaAsync(SolicitarTarjetaDto dto, long idUsuario);
}
