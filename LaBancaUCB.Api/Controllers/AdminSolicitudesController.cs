using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/solicitudes")] 
[Authorize(Roles = "admin")]     
public class AdminSolicitudesController : ControllerBase
{
    private readonly IAdminSolicitudesService _adminService;

    public AdminSolicitudesController(IAdminSolicitudesService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? estado)
    {
        var lista = await _adminService.GetSolicitudesAsync(estado);
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(long id)
    {
        var solicitud = await _adminService.GetSolicitudByIdAsync(id);
        if (solicitud == null) return NotFound(new { message = "Solicitud no encontrada" });

        return Ok(solicitud);
    }

    [HttpPut("{id}/manage")]
    public async Task<IActionResult> Gestionar(long id, [FromBody] GestionarSolicitudDto dto)
    {
        await _adminService.GestionarSolicitudAsync(id, dto);
        return Ok(new { message = $"La solicitud fue {dto.Estado} exitosamente." });
    }
}