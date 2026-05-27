using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
/// <summary>
/// Endpoints para crear transferencias y consultar historial de transacciones.
/// </summary>
public class TransaccionesController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionesController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    /// <summary>
    /// Crea una transferencia al exterior (cliente).
    /// </summary>
    /// <response code="200">Transferencia creada</response>
    /// <response code="401">No autorizado</response>
    /// <response code="400">Datos inválidos</response>
    [HttpPost("transferencia-exterior")]
    [Authorize(Roles = "cliente")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(object))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CrearTransferenciaExterior([FromBody] TransferenciaExteriorDto dto)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var usuarioId = long.Parse(claim.Value);
        var result = await _transaccionService.CrearTransferenciaExteriorAsync(dto, usuarioId);

        return Ok(result);
    }

    /// <summary>
    /// Historial de transacciones del usuario autenticado (paginado).
    /// </summary>
    /// <response code="200">Retorna la lista paginada de transacciones</response>
    /// <response code="401">No autorizado</response>
    [HttpGet("history")]
    [Authorize(Roles = "cliente")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Api.Response.ApiResponse<object>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetHistorial([FromQuery] TransaccionQueryFilter filters)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var usuarioId = long.Parse(claim.Value);

        var pagedTransacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(usuarioId, filters);

        var response = new Api.Response.ApiResponse<object>(pagedTransacciones)
        {
            Meta = new Core.CustomEntities.Metadata
            {
                TotalCount = pagedTransacciones.TotalCount,
                PageSize = pagedTransacciones.PageSize,
                CurrentPage = pagedTransacciones.CurrentPage,
                TotalPages = pagedTransacciones.TotalPages,
                HasNextPage = pagedTransacciones.HasNextPage,
                HasPreviousPage = pagedTransacciones.HasPreviousPage,
                NextPageNumber = pagedTransacciones.NextPageNumber,
                PreviousPageNumber = pagedTransacciones.PreviousPageNumber
            }
        };

        return Ok(response);
    }
}