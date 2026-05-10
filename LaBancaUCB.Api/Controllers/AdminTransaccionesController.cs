using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using System.Threading.Tasks;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "admin")]
public class TransaccionesAdminController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionesAdminController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? estado)
    {
        var lista = await _transaccionService.ListarTransferenciasPorEstadoAsync(estado);
        return Ok(lista);
    }

    [HttpPost("{id}/aprobar")]
    public async Task<IActionResult> Aprobar(long id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var adminId = long.Parse(claim.Value);
        await _transaccionService.AprobarTransferenciaAsync(id, adminId);

        return Ok(new { message = "Aprobada" });
    }

    [HttpPost("{id}/rechazar")]
    public async Task<IActionResult> Rechazar(long id, [FromBody] AdminTransferActionDto dto)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var adminId = long.Parse(claim.Value);
        await _transaccionService.RechazarTransferenciaAsync(id, dto?.Motivo ?? "", adminId);

        return Ok(new { message = "Rechazada" });
    }
}