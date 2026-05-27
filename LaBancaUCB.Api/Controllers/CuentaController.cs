using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CuentaController : ControllerBase
{
    private readonly ICuentaService _cuentaService;
    private readonly IMapper _mapper;

    public CuentaController(ICuentaService cuentaService, IMapper mapper)
    {
        _cuentaService = cuentaService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene las cuentas asociadas al usuario autenticado, con paginación.
    /// </summary>
    /// <param name="filters">Filtros de paginación (pageNumber, pageSize).</param>
    /// <returns>Lista paginada de cuentas del usuario.</returns>
    /// <response code="200">Retorna la lista paginada de cuentas</response>
    /// <response code="401">Token inválido o usuario no identificado</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CuentaDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("mis-cuentas")]
    public async Task<ActionResult> GetMisCuentas([FromQuery] PaginationFilter filters)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
        {
            return Unauthorized(new { message = "Token inválido o usuario no identificado" });
        }

        // Get all accounts for the user (service only accepts user id)
        var cuentas = await _cuentaService.GetCuentasByUsuarioIdAsync(idUsuario);

        // Create a paged list from the IEnumerable result using the provided pagination filters
        var pagedCuentas = PagedList<Core.Entities.Cuenta>.Create(cuentas, filters.PageNumber, filters.PageSize);

        var cuentasDto = _mapper.Map<IEnumerable<CuentaDto>>(pagedCuentas);

        var response = new Api.Response.ApiResponse<IEnumerable<CuentaDto>>(cuentasDto)
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
}