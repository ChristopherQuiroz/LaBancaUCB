using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Services.Services;

public class CuentaService : ICuentaService
{
    private readonly IBaseRepository<Cuenta> _cuentaRepository;

    public CuentaService(IBaseRepository<Cuenta> cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<PagedList<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario, PaginationFilter filters)
    {
        var todasLasCuentas = await _cuentaRepository.GetAllAsync();

        var cuentasDelUsuario = todasLasCuentas.Where(c => c.IdUsuario == idUsuario)
                                               .OrderByDescending(c => c.FechaApertura);

        return PagedList<Cuenta>.Create(cuentasDelUsuario, filters.PageNumber, filters.PageSize);
    }
}
