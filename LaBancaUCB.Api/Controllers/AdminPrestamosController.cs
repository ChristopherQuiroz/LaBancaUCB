using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

/// <summary>
/// Controlador exclusivo para administradores que gestionen préstamos
/// </summary>
[ApiController]
[Route("api/admin/prestamos")]
[Authorize(Roles = "admin")]
[Produces("application/json")]
public class AdminPrestamosController : ControllerBase
{
    private readonly IPrestamoService _prestamoService;

    public AdminPrestamosController(IPrestamoService prestamoService)
    {
        _prestamoService = prestamoService;
    }

    /// <summary>
    /// Obtiene una lista paginada de préstamos.
    /// </summary>
    /// <param name="pageNumber">Número de página (por defecto 1).</param>
    /// <param name="pageSize">Tamaño de página (por defecto 10).</param>
    /// <returns>Lista paginada de préstamos con metadata.</returns>
    /// <response code="200">Retorna la lista paginada de préstamos</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var filters = new PaginationFilter
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var pagedPrestamos = await _prestamoService.GetAllPrestamosAsync(filters);

        var response = new ApiResponse<object>(pagedPrestamos);
        response.Meta = new Metadata
        {
            TotalCount = pagedPrestamos.TotalCount,
            PageSize = pagedPrestamos.PageSize,
            CurrentPage = pagedPrestamos.CurrentPage,
            TotalPages = pagedPrestamos.TotalPages,
            HasNextPage = pagedPrestamos.HasNextPage,
            HasPreviousPage = pagedPrestamos.HasPreviousPage
        };

        return Ok(response);
    }

    /// <summary>
    /// Gestiona (aprueba/rechaza) un préstamo.
    /// </summary>
    /// <param name="id">Id del préstamo.</param>
    /// <param name="dto">Datos para gestión del préstamo.</param>
    /// <returns>Resultado de la operación.</returns>
    /// <response code="200">Préstamo gestionado correctamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="404">Préstamo no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPut("{id}/gestionar")]
    public async Task<IActionResult> Gestionar(long id, [FromBody] GestionarPrestamoDto dto)
    {
        if (dto is null)
            return BadRequest(new { message = "Request body is required." });

        var prestamo = await _prestamoService.GetPrestamoByIdAsync(id);
        if (prestamo is null)
            return NotFound(new { message = "Préstamo no encontrado." });

        prestamo.MontoAprobado = dto.MontoAprobado;
        prestamo.TasaInteres = dto.TasaInteres;
        prestamo.Estado = dto.Estado;

        await _prestamoService.UpdatePrestamoAsync(prestamo);

        return Ok(new { message = "Préstamo gestionado exitosamente." });
    }
}