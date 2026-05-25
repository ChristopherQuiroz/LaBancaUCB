using System.Net;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Helpers;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Services.Services;

public class TransaccionService : ITransaccionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public TransaccionService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<PagedList<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario, TransaccionQueryFilter? filters = null)
    {
        var todasCuentas = await _unitOfWork.CuentaRepository.GetAllAsync();
        var cuentasUsuario = todasCuentas.Where(c => c.IdUsuario == idUsuario).ToList();

        var idsCuentas = cuentasUsuario.Select(c => c.IdCuenta).ToList();
        var numerosCuentas = cuentasUsuario.Select(c => c.NumeroCuenta).ToList();

        var todasTransacciones = await _unitOfWork.TransaccionRepository.GetAllAsync();

        var query = todasTransacciones.Where(t =>
            idsCuentas.Contains(t.IdCuentaOrigen) ||
            numerosCuentas.Contains(t.IdCuentaDestino)
        );

        if (filters != null)
        {
            if (!string.IsNullOrWhiteSpace(filters.Estado))
            {
                query = query.Where(x => x.Estado.ToLower() == filters.Estado.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filters.Tipo))
            {
                query = query.Where(x => x.Tipo.ToLower() == filters.Tipo.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filters.Glosa))
            {
                query = query.Where(x => x.Glosa != null && x.Glosa.ToLower().Contains(filters.Glosa.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filters.Fecha))
            {
                var fechaLimpia = Procesos.ParseFechaFlexible(filters.Fecha);
                if (fechaLimpia != null)
                {
                    var fechaFiltro = DateTime.ParseExact(fechaLimpia, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    query = query.Where(x => x.FechaHora.Date == fechaFiltro.Date);
                }
            }
        }

        var transaccionesOrdenadas = query.OrderByDescending(t => t.FechaHora);

        int pageNumber = filters?.PageNumber ?? 1;
        int pageSize = filters?.PageSize ?? 10;

        var pagedTransacciones = PagedList<Transaccion>.Create(transaccionesOrdenadas, pageNumber, pageSize);

        return pagedTransacciones;
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
        {
            throw new BusinessException("Cuenta origen no encontrada en el sistema.", HttpStatusCode.NotFound);
        }

        if (cuenta.IdUsuario != usuarioId)
        {
            throw new BusinessException("Acceso denegado: La cuenta no te pertenece.", HttpStatusCode.Forbidden);
        }

        if (cuenta.Saldo < dto.Monto)
        {
            throw new BusinessException("Saldo insuficiente para realizar esta transferencia.", HttpStatusCode.BadRequest);
        }

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
        {
            return todas.OrderByDescending(t => t.FechaHora);
        }

        return todas.Where(t => t.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(t => t.FechaHora);
    }

    public async Task AprobarTransferenciaAsync(long transaccionId, long adminId)
    {
        var trans = await _unitOfWork.TransaccionRepository.GetByIdAsync(transaccionId);

        if (trans == null)
        {
            throw new BusinessException("Transacción no encontrada.", HttpStatusCode.NotFound);
        }

        if (!trans.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase))
        {
            throw new BusinessException("Solo se pueden aprobar transacciones que estén en estado 'pendiente'.", HttpStatusCode.BadRequest);
        }

        // Actualizamos el estado de la transacción
        trans.Estado = "completada";
        trans.FechaHora = DateTime.UtcNow;

        _unitOfWork.TransaccionRepository.Update(trans);
        await _unitOfWork.SaveChangesAsync();

        //NOTIFICACIÓN CU-18 
        var cuentaOrigen = await _unitOfWork.CuentaRepository.GetByIdAsync(trans.IdCuentaOrigen);
        if (cuentaOrigen != null)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(cuentaOrigen.IdUsuario);
            if (usuario != null)
            {
                string cuerpoHtml = $@"
                <h2 style='color: #003366;'>Comprobante de Transferencia Aprobada</h2>
                <p>Estimado/a <strong>{usuario.NombreCompleto}</strong>,</p>
                <p>Nos complace informarte que tu transferencia al exterior por <strong>{trans.Monto} {trans.Moneda}</strong> ha sido procesada con éxito.</p>
                <br>
                <p><strong>Detalles de la operación:</strong></p>
                <ul>
                    <li><strong>Destino:</strong> {trans.NombreDestino}</li>
                    <li><strong>Monto:</strong> {trans.Monto} {trans.Moneda}</li>
                    <li><strong>Glosa:</strong> {trans.Glosa}</li>
                    <li><strong>Fecha:</strong> {trans.FechaHora:dd/MM/yyyy HH:mm}</li>
                </ul>
                <br>
                <p>Gracias por confiar en La Banca UCB.</p>";

                try
                {
                    await _emailService.EnviarComprobanteAsync(usuario.Email, "Transferencia Aprobada - La Banca UCB", cuerpoHtml);
                }
                catch
                {

                }
            }
        }
    }

    public async Task RechazarTransferenciaAsync(long transaccionId, string motivo, long adminId)
    {
        var trans = await _unitOfWork.TransaccionRepository.GetByIdAsync(transaccionId);

        if (trans == null)
        {
            throw new BusinessException("Transacción no encontrada.", HttpStatusCode.NotFound);
        }

        if (!trans.Estado.Equals("pendiente", StringComparison.OrdinalIgnoreCase))
        {
            throw new BusinessException("Solo se pueden rechazar transacciones que estén en estado 'pendiente'.", HttpStatusCode.BadRequest);
        }

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