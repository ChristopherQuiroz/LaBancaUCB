using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

/// <summary>
/// Controlador exclusivo para administradores que gestionan cuentas de clientes.
/// </summary>
[ApiController]
[Route("api/admin/cuentas")]
[Authorize(Roles = "admin")]
[Produces("application/json")]
public class AdminCuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;
    private readonly IValidator<EstadoCuentaDto> _validator;
    private readonly IMapper _mapper;

    public AdminCuentasController(
        ICuentaService cuentaService,
        IValidator<EstadoCuentaDto> validator,
        IMapper mapper)
    {
        _cuentaService = cuentaService;
        _validator = validator;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene una lista paginada de cuentas aplicando filtros opcionales.
    /// </summary>
    /// <param name="filters">Filtros como número de cuenta, tipo, estado, etc.</param>
    /// <param name="Estado">El estado en el que se encuentra la cuenta.</param>
    /// <param name="PageSize">Tamaño de página</param>
    /// <param name="PageNumber">Número de página</param>
    /// <param name="UsuarioId">Id del usuario</param>
    /// <returns>Lista paginada de cuentas con metadatos.</returns>
    /// <response code="200">Retorna la lista paginada de cuentas</response>
    /// <response code="401">No autenticado</response>
    /// <response code="403">No autorizado (rol no es admin)</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CuentaDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> ListarCuentas([FromQuery] CuentaQueryFilterDto filters)
    {
        var pagedCuentas = await _cuentaService.GetCuentasPaginadasAsync(filters);

        var cuentasDto = _mapper.Map<IEnumerable<CuentaDto>>(pagedCuentas);

        var response = new ApiResponse<IEnumerable<CuentaDto>>(cuentasDto)
        {
            Meta = new Metadata
            {
                TotalCount = pagedCuentas.TotalCount,
                PageSize = pagedCuentas.PageSize,
                CurrentPage = pagedCuentas.CurrentPage,
                TotalPages = pagedCuentas.TotalPages,
                HasNextPage = pagedCuentas.HasNextPage,
                HasPreviousPage = pagedCuentas.HasPreviousPage,
                NextPageNumber = pagedCuentas.NextPageNumber,
                PreviousPageNumber = pagedCuentas.PreviousPageNumber
            }
        };

        return Ok(response);
    }

    /// <summary>
    /// Cambia el estado de una cuenta (ej. bloquear, desbloquear, suspender).
    /// </summary>
    /// <param name="id">Identificador único de la cuenta.</param>
    /// <param name="dto">Objeto con la acción a realizar (bloquear, activar, etc.).</param>
    /// <returns>Mensaje de éxito.</returns>
    /// <response code="200">Estado de la cuenta cambiado exitosamente</response>
    /// <response code="400">Datos inválidos o validación fallida</response>
    /// <response code="401">No autenticado</response>
    /// <response code="403">No autorizado</response>
    /// <response code="404">Cuenta no encontrada</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPut("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(long id, [FromBody] EstadoCuentaDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);

        var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimId == null) return Unauthorized(new { message = "Token inválido o expirado." });

        var idAdmin = long.Parse(claimId.Value);

        var ipOrigen = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";

        await _cuentaService.CambiarEstadoCuentaAsync(id, idAdmin, ipOrigen, dto);

        return Ok(new { message = $"Cuenta {dto.Accion} exitosamente." });
    }
}