using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Core.CustomEntities;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
/// <summary>
/// Endpoints de transacciones y historial para usuarios y administradores.
/// </summary>
public class TransaccionController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;
    private readonly IMapper _mapper;

    public TransaccionController(ITransaccionService transaccionService, IMapper mapper)
    {
        _transaccionService = transaccionService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene el historial de transacciones del usuario autenticado.
    /// </summary>
    /// <response code="200">Retorna la lista paginada de transacciones</response>
    /// <response code="401">Token inválido o usuario no identificado</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TransaccionDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("history")]
    public async Task<ActionResult> GetHistorial([FromQuery] TransaccionQueryFilter filters)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
        {
            return Unauthorized(new { message = "Token inválido o usuario no identificado" });
        }

        var pagedTransacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario, filters);

        var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(pagedTransacciones);

        var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);

        var metadata = new Metadata
        {
            TotalCount = pagedTransacciones.TotalCount,
            PageSize = pagedTransacciones.PageSize,
            CurrentPage = pagedTransacciones.CurrentPage,
            TotalPages = pagedTransacciones.TotalPages,
            HasNextPage = pagedTransacciones.HasNextPage,
            HasPreviousPage = pagedTransacciones.HasPreviousPage,
            NextPageNumber = pagedTransacciones.NextPageNumber,
            PreviousPageNumber = pagedTransacciones.PreviousPageNumber
        };

        response.Meta = metadata;

        return Ok(response);
    }

    /// <summary>
    /// Obtiene el historial de un cliente por su Id (solo admin).
    /// </summary>
    /// <param name="idUsuario">Identificador del usuario al que se desea ver su historial </param>
    /// <response code="200">Retorna el historial del cliente</response>
    /// <response code="401">No autorizado</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TransaccionDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpGet("history/{idUsuario}")]
    public async Task<ActionResult> GetHistorialPorCliente(long idUsuario)
    {
        var transacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario);
        var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(transacciones);
        var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);

        return Ok(response);
    }
}