using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class TransaccionService : ITransaccionService
{
    private readonly IBaseRepository<Transaccion> _transaccionRepository;
    private readonly IBaseRepository<Cuenta> _cuentaRepository;

    public TransaccionService(
        IBaseRepository<Transaccion> transaccionRepository,
        IBaseRepository<Cuenta> cuentaRepository)
    {
        _transaccionRepository = transaccionRepository;
        _cuentaRepository = cuentaRepository;
    }

    public async Task<IEnumerable<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario)
    {
        var todasCuentas = await _cuentaRepository.GetAllAsync();
        var cuentasUsuario = todasCuentas.Where(c => c.IdUsuario == idUsuario).ToList();

        var idsCuentas = cuentasUsuario.Select(c => c.IdCuenta).ToList();
        var numerosCuentas = cuentasUsuario.Select(c => c.NumeroCuenta).ToList();

        var todasTransacciones = await _transaccionRepository.GetAllAsync();

        var historial = todasTransacciones.Where(t =>
            idsCuentas.Contains(t.IdCuentaOrigen) ||
            numerosCuentas.Contains(t.IdCuentaDestino)
        ).OrderByDescending(t => t.FechaHora); 

        return historial;
    }
}
