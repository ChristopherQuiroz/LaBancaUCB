using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

/// <summary>
/// Controlador para que clientes soliciten productos como tarjetas de crédito/débito.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "cliente")]
[Produces("application/json")]
public class SolicitudesController : ControllerBase
{
    private readonly IProductosService _productosService;

    public SolicitudesController(IProductosService productosService)
    {
        _productosService = productosService;
    }

    /// <summary>
    /// Solicita una nueva tarjeta (crédito o débito).
    /// </summary>
    /// <param name="dto">Datos de la solicitud de tarjeta (tipo, línea de crédito, etc.).</param>
    /// <returns>Mensaje de confirmación.</returns>
    /// <response code="200">Solicitud de tarjeta registrada correctamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="401">No autenticado</response>
    /// <response code="403">No autorizado (rol no es cliente)</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost("tarjeta")]
    public async Task<IActionResult> SolicitarTarjeta([FromBody] SolicitarTarjetaDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _productosService.SolicitarTarjetaAsync(dto, idUsuario);
        return Ok(new { message = "Solicitud de tarjeta enviada correctamente." });
    }
}