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
public class SolicitudesController : ControllerBase
{
    private readonly ISolicitudService _solicitudService;
    private readonly IUnitOfWork _unitOfWork;

    public SolicitudesController(ISolicitudService solicitudService, IUnitOfWork unitOfWork)
    {
        _solicitudService = solicitudService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("tarjeta")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> SolicitarTarjeta([FromBody] TarjetaRequestDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            var solicitud = new Solicitud
            {
                IdUsuario = usuarioId,
                TipoSolicitud = "apertura_tarjeta",
                referenciaID = 0,
                Estado = "pendiente",
                IdAdmin = null,
                ObservacionAdmin = null,
                FechaCreacion = DateTime.UtcNow
            };

            await _solicitudService.InsertSolicitudAsync(solicitud);
            return CreatedAtAction(nameof(GetById), new { id = solicitud.IdSolicitud }, solicitud);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message, detail = ex.InnerException?.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> GetById(long id)
    {
        var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
        if (solicitud == null) return NotFound();
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();
        var usuarioId = long.Parse(claim.Value);
        if (solicitud.IdUsuario != usuarioId) return Forbid();
        return Ok(solicitud);
    }
}
