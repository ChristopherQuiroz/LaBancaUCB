using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using System.Threading.Tasks;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "admin")]
[Produces("application/json")]
/// <summary>
/// Endpoints para gestión de transferencias por parte de administradores.
/// </summary>
public class TransaccionesAdminController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionesAdminController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    /// <summary>
    /// Lista transferencias filtradas por estado.
    /// </summary>
    /// <param name="estado">Estado en el que se encuentra la transaccion</param>
    /// <response code="200">Retorna la lista de transferencias</response>
    /// <response code="401">No autorizado</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? estado)
    {
        var lista = await _transaccionService.ListarTransferenciasPorEstadoAsync(estado);
        return Ok(lista);
    }

    /// <summary>
    /// Aprueba una transferencia.
    /// </summary>
    /// <param name="id">Identificador del usuario al que se le va a aprobar la transaccion</param>
    /// <response code="200">Transferencia aprobada</response>
    /// <response code="401">No autorizado</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost("{id}/aprobar")]
    public async Task<IActionResult> Aprobar(long id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var adminId = long.Parse(claim.Value);
        await _transaccionService.AprobarTransferenciaAsync(id, adminId);

        return Ok(new { message = "Aprobada" });
    }

    /// <summary>
    /// Rechaza una transferencia con motivo.
    /// </summary>
    /// <param name="id">Identificador del usuario al que se le va a rechazar la transaccion</param>
    /// <response code="200">Transferencia rechazada</response>
    /// <response code="401">No autorizado</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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