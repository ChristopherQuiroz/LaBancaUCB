using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    private readonly IPrestamoService _prestamoService;
    private readonly ISolicitudService _solicitudService;
    private readonly IUnitOfWork _unitOfWork;

    public PrestamosController(IPrestamoService prestamoService, ISolicitudService solicitudService, IUnitOfWork unitOfWork)
    {
        _prestamoService = prestamoService;
        _solicitudService = solicitudService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("solicitar")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> SolicitarPrestamo([FromBody] PrestamoRequestDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            var prestamo = new Prestamo
            {
                IdCuenta = dto.IdCuenta,
                MontoSolicitado = dto.MontoSolicitado,
                MontoAprobado = 0,
                TasaInteres = dto.TasaInteres,
                plazoMeses = dto.PlazoMeses,
                estado = "solicitado",
                solicitadoEn = DateTime.UtcNow
            };

            await _prestamoService.InsertPrestamoAsync(prestamo);

            var solicitud = new Solicitud
            {
                IdUsuario = usuarioId,
                TipoSolicitud = "prestamo",
                referenciaID = prestamo.IdPrestamo,
                Estado = "pendiente",
                IdAdmin = null,
                ObservacionAdmin = null,
                FechaCreacion = DateTime.UtcNow
            };

            await _solicitudService.InsertSolicitudAsync(solicitud);

            return CreatedAtAction("GetById", "Solicitudes", new { id = solicitud.IdSolicitud }, solicitud);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message, detail = ex.InnerException?.Message });
        }
    }
}
