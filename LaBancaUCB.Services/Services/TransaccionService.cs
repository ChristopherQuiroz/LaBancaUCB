using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Exceptions;

namespace LaBancaUCB.Services.Services;

public class TransaccionService : ITransaccionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransaccionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario)
    {
        var todasCuentas = await _unitOfWork.CuentaRepository.GetAllAsync();
        var cuentasUsuario = todasCuentas.Where(c => c.IdUsuario == idUsuario).ToList();

        var idsCuentas = cuentasUsuario.Select(c => c.IdCuenta).ToList();
        var numerosCuentas = cuentasUsuario.Select(c => c.NumeroCuenta).ToList();

        var todasTransacciones = await _unitOfWork.TransaccionRepository.GetAllAsync();

        var historial = todasTransacciones.Where(t =>
            idsCuentas.Contains(t.IdCuentaOrigen) ||
            numerosCuentas.Contains(t.IdCuentaDestino)
        ).OrderByDescending(t => t.FechaHora);

        return historial;
    }

    public async Task<Transaccion> CrearTransferenciaExteriorAsync(TransferenciaExteriorDto dto, long usuarioId)
    {
        var cantidadPendientes = await _unitOfWork.TransaccionRepository.GetCantidadPendientesAsync(usuarioId);
        if (cantidadPendientes >= 3)
        {
            throw new BusinessException("Seguridad: No puedes tener más de 3 transferencias pendientes de aprobación.", HttpStatusCode.BadRequest);
        }

        var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.CuentaOrigenId);
        if (cuenta == null)
            throw new BusinessException("Cuenta origen no encontrada en el sistema.", HttpStatusCode.NotFound);

        if (cuenta.IdUsuario != usuarioId)
            throw new BusinessException("Acceso denegado: La cuenta no te pertenece.", HttpStatusCode.Forbidden);

        if (cuenta.Saldo < dto.Monto)
            throw new BusinessException("Saldo insuficiente para realizar esta transferencia.", HttpStatusCode.BadRequest);

        cuenta.Saldo -= dto.Monto;
        _unitOfWork.CuentaRepository.Update(cuenta);

        var transaccion = new Transaccion
        {
            IdCuentaOrigen = dto.CuentaOrigenId,
            IdCuentaDestino = dto.CuentaDestino,
            NombreDestino = dto.BancoDestino + " - " + dto.PaisDestino,
            Tipo = "exterior",
            Monto = dto.Monto,
            Moneda = dto.MonedaDestino,
            TipoCambio = 0m,
            Glosa = dto.Referencia,
            Estado = "pendiente",
            FechaHora = dto.FechaProgramada ?? DateTime.UtcNow
        };

        await _unitOfWork.TransaccionRepository.AddAsync(transaccion);
        await _unitOfWork.SaveChangesAsync();

        return transaccion;
    }

    public async Task<IEnumerable<Transaccion>> ListarTransferenciasPorEstadoAsync(string? estado = null)
    {
        var todas = await _unitOfWork.TransaccionRepository.GetAllAsync();
        if (string.IsNullOrWhiteSpace(estado))
            return todas.OrderByDescending(t => t.FechaHora);

        return todas.Where(t => t.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(t => t.FechaHora);
    }

    public async Task AprobarTransferenciaAsync(long transaccionId, long adminId)
    {
        var trans = await _unitOfWork.TransaccionRepository.GetByIdAsync(transaccionId);
        if (trans == null)
            throw new BusinessException("Transacción no encontrada.", HttpStatusCode.NotFound);

        if (!trans.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase))
            throw new BusinessException("Solo se pueden aprobar transacciones que estén en estado 'pendiente'.", HttpStatusCode.BadRequest);

        trans.Estado = "completada";
        trans.FechaHora = DateTime.UtcNow;

        _unitOfWork.TransaccionRepository.Update(trans);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RechazarTransferenciaAsync(long transaccionId, string motivo, long adminId)
    {
        var trans = await _unitOfWork.TransaccionRepository.GetByIdAsync(transaccionId);
        if (trans == null)
            throw new BusinessException("Transacción no encontrada.", HttpStatusCode.NotFound);

        if (!trans.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase))
            throw new BusinessException("Solo se pueden rechazar transacciones que estén en estado 'pendiente'.", HttpStatusCode.BadRequest);

        var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(trans.IdCuentaOrigen);
        if (cuenta != null)
        {
            cuenta.Saldo += trans.Monto;
            _unitOfWork.CuentaRepository.Update(cuenta);
        }

        trans.Estado = "rechazada";
        trans.Glosa = (trans.Glosa ?? string.Empty) + " | Rechazo: " + (motivo ?? string.Empty);
        trans.FechaHora = DateTime.UtcNow;

        _unitOfWork.TransaccionRepository.Update(trans);
        await _unitOfWork.SaveChangesAsync();
    }
}