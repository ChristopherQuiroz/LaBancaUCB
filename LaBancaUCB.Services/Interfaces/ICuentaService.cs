using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Services.Interfaces;

public interface ICuentaService
{
    Task<IEnumerable<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario);
    Task CambiarEstadoCuentaAsync(long idCuenta, long idAdmin, string ipOrigen, EstadoCuentaDto dto);
    Task<PagedList<Cuenta>> GetCuentasPaginadasAsync(CuentaQueryFilterDto filters);
}