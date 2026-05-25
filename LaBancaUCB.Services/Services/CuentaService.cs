using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using System.Net;

namespace LaBancaUCB.Services.Services;

public class CuentaService : ICuentaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CuentaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario)
    {
        var todasLasCuentas = await _unitOfWork.CuentaRepository.GetAllAsync();
        return todasLasCuentas.Where(c => c.IdUsuario == idUsuario);
    }

    public async Task CambiarEstadoCuentaAsync(long idCuenta, long idAdmin, string ipOrigen, EstadoCuentaDto dto)
    {
        var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(idCuenta);
        if (cuenta == null)
            throw new BusinessException("Cuenta no encontrada.", HttpStatusCode.NotFound);

        cuenta.Estado = dto.Accion == "bloquear" ? "bloqueada" : "activada";
        _unitOfWork.CuentaRepository.Update(cuenta);

        var auditoria = new AuditoriaCuenta
        {
            IdCuenta = idCuenta,
            IdAdministrador = idAdmin,
            Accion = dto.Accion,
            Motivo = dto.Motivo,
            IpOrigen = ipOrigen,
            EjecutadoEn = DateTime.UtcNow
        };

        await _unitOfWork.AuditoriaCuentaRepository.AddAsync(auditoria);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedList<Cuenta>> GetCuentasPaginadasAsync(CuentaQueryFilterDto filters)
    {
        var todas = await _unitOfWork.CuentaRepository.GetAllAsync();
        var query = todas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Estado))
        {
            query = query.Where(c => c.Estado.Equals(filters.Estado, StringComparison.OrdinalIgnoreCase));
        }

        var cuentasOrdenadas = query.OrderByDescending(c => c.FechaApertura);

        return PagedList<Cuenta>.Create(cuentasOrdenadas, filters.PageNumber, filters.PageSize);
    }
}