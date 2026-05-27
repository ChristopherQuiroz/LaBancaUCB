using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

/// <summary>
/// Controlador para que clientes soliciten seguros.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "cliente")]
[Produces("application/json")]
public class SegurosController : ControllerBase
{
    private readonly IProductosService _productosService;

    public SegurosController(IProductosService productosService)
    {
        _productosService = productosService;
    }

    /// <summary>
    /// Solicita un nuevo seguro (vida, auto, hogar, etc.).
    /// </summary>
    /// <param name="dto">Datos del seguro (tipo, cobertura, etc.).</param>
    /// <returns>Mensaje de confirmación.</returns>
    /// <response code="200">Solicitud de seguro registrada correctamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="401">No autenticado</response>
    /// <response code="403">No autorizado (rol no es cliente)</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> SolicitarSeguro([FromBody] CrearSeguroDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _productosService.SolicitarSeguroAsync(dto, idUsuario);
        return Ok(new { message = "Solicitud de seguro enviada correctamente." });
    }
}