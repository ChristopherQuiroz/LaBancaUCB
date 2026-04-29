using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/solicitudes")]
[Authorize(Roles = "admin")]
public class AdminSolicitudesController : ControllerBase
{
    private readonly ISolicitudService _solicitudService;
    private readonly IPrestamoService _prestamoService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminSolicitudesController(ISolicitudService solicitudService, IPrestamoService prestamoService, IUnitOfWork unitOfWork)
    {
        _solicitudService = solicitudService;
        _prestamoService = prestamoService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? estado)
    {
        var todas = await _solicitudService.GetAllSolicitudesAsync();
        var filtradas = string.IsNullOrWhiteSpace(estado) ? todas : todas.Where(s => s.Estado == estado);
        return Ok(filtradas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(long id)
    {
        var s = await _solicitudService.GetSolicitudByIdAsync(id);
        if (s == null) return NotFound();
        return Ok(s);
    }

    [HttpPut("{id}/manage")]
    public async Task<IActionResult> Gestionar(long id, [FromBody] AdminManageSolicitudDto dto)
    {
        try
        {
            var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
            if (solicitud == null) return NotFound();

            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            // encontrar registro de administrador para este usuario
            var administradores = await _unitOfWork.AdministradorRepository.GetAllAsync();
            var admin = administradores.FirstOrDefault(a => a.IdUsuario == usuarioId);
            solicitud.IdAdmin = admin?.IdAdministrador;
            solicitud.ObservacionAdmin = dto.ObservacionAdmin;
            solicitud.Estado = dto.Estado;
            solicitud.GestionadaEn = DateTime.UtcNow;

            // si es prestamo y se aprueba, actualizar prestamo
            if (solicitud.TipoSolicitud == "prestamo" && dto.MontoAprobado.HasValue && dto.Estado == "aprobada")
            {
                var prestamo = await _prestamoService.GetPrestamoByIdAsync(solicitud.referenciaID);
                if (prestamo != null)
                {
                    prestamo.MontoAprobado = dto.MontoAprobado.Value;
                    prestamo.estado = "aprobado";
                    prestamo.aprobadoEn = DateTime.UtcNow;
                    await _prestamoService.UpdatePrestamoAsync(prestamo);
                }
            }

            await _solicitudService.UpdateSolicitudAsync(solicitud);
            return Ok(solicitud);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message, detail = ex.InnerException?.Message });
        }
    }
}
