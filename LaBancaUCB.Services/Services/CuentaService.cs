using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class CuentaService : ICuentaService
{
    private readonly IBaseRepository<Cuenta> _cuentaRepository;

    public CuentaService(IBaseRepository<Cuenta> cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<IEnumerable<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario)
    {
        var todasLasCuentas = await _cuentaRepository.GetAllAsync();

        var cuentasDelUsuario = todasLasCuentas.Where(c => c.IdUsuario == idUsuario);

        return cuentasDelUsuario;
    }
}
