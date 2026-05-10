using System;
using System.Net;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class ProductosService : IProductosService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductosService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task ValidarCuenta(long idCuenta, long idUsuario)
    {
        var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(idCuenta);
        if (cuenta == null)
            throw new BusinessException("La cuenta indicada no existe.", HttpStatusCode.NotFound);

        if (cuenta.IdUsuario != idUsuario)
            throw new BusinessException("Acceso denegado: La cuenta no te pertenece.", HttpStatusCode.Forbidden);
    }

    public async Task SolicitarSeguroAsync(CrearSeguroDto dto, long idUsuario)
    {
        await ValidarCuenta(dto.IdCuenta, idUsuario);

        var seguro = new Seguro
        {
            IdCuenta = dto.IdCuenta,
            TipoSeguro = dto.TipoSeguro,
            PrimaMensual = dto.PrimaMensual,
            Cobertura = dto.Cobertura,
            Estado = "pendiente",
            FechaInicio = DateTime.UtcNow,
            FechaFin = DateTime.UtcNow.AddYears(1)
        };
        await _unitOfWork.SeguroRepository.AddAsync(seguro);
        await _unitOfWork.SaveChangesAsync();

        var solicitud = new Solicitud
        {
            IdUsuario = idUsuario,
            TipoSolicitud = "seguro",
            ReferenciaId = seguro.IdSeguro,
            Estado = "pendiente",
            FechaCreacion = DateTime.UtcNow
        };
        await _unitOfWork.SolicitudRepository.AddAsync(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SolicitarPrestamoAsync(CrearPrestamoDto dto, long idUsuario)
    {
        await ValidarCuenta(dto.IdCuenta, idUsuario);

        var prestamo = new Prestamo
        {
            IdCuenta = dto.IdCuenta,
            MontoSolicitado = dto.MontoSolicitado,
            MontoAprobado = 0,
            TasaInteres = dto.TasaInteres,
            PlazoMeses = dto.PlazoMeses,
            Estado = "solicitado",
            SolicitadoEn = DateTime.UtcNow
        };
        await _unitOfWork.PrestamoRepository.AddAsync(prestamo);
        await _unitOfWork.SaveChangesAsync();

        var solicitud = new Solicitud
        {
            IdUsuario = idUsuario,
            TipoSolicitud = "prestamo",
            ReferenciaId = prestamo.IdPrestamo,
            Estado = "pendiente",
            FechaCreacion = DateTime.UtcNow
        };
        await _unitOfWork.SolicitudRepository.AddAsync(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SolicitarTarjetaAsync(SolicitarTarjetaDto dto, long idUsuario)
    {
        await ValidarCuenta(dto.IdCuenta, idUsuario);

        var tarjeta = new Tarjeta
        {
            IdCuenta = dto.IdCuenta,
            Tipo = dto.Tipo,
            NumeroEnmascarado = "**** **** **** 0000",
            FechaVencimiento = DateTime.UtcNow.AddYears(4),
            Estado = "solicitada",
            FechaCreacion = DateTime.UtcNow
        };
        await _unitOfWork.TarjetaRepository.AddAsync(tarjeta);
        await _unitOfWork.SaveChangesAsync(); 

        var solicitud = new Solicitud
        {
            IdUsuario = idUsuario,
            TipoSolicitud = "tarjeta",
            ReferenciaId = tarjeta.IdTarjeta, 
            Estado = "pendiente",
            FechaCreacion = DateTime.UtcNow
        };
        await _unitOfWork.SolicitudRepository.AddAsync(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }
}