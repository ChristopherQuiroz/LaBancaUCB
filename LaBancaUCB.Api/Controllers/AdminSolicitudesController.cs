using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace LaBancaUCB.Api.Controllers;

/// <summary>
/// Controlador exclusivo para administradores que gestionen solicitudes de usuario
/// </summary>
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

    /// <summary>
    /// Obtiene una lista paginada de solicitudes aplicando filtros.
    /// </summary>
    /// <remarks>
    /// Este método utiliza un mapeador para convertir las solicitudes recuperadas en DTOs que luego se devuelven junto con la información de paginación.
    /// </remarks>
    /// <param name="filters">Filtros como número de página, tamaño, estado, etc.</param>
    /// <returns>Una respuesta con la lista paginada de <see cref="AdminManageSolicitudDto"/>.</returns>
    /// <response code="200">Retorna la lista de [AdminManageSolicitudDto]</response>
    /// <response code="404">No existen registros con los filtros especificados</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType(
        (int)HttpStatusCode.OK,
        Type = typeof(ApiResponse<IEnumerable<AdminManageSolicitudDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] SolicitudQueryFilter filters)
    {
        var pagedSolicitudes = await _adminService.GetSolicitudesAsync(filters);

        var response = new ApiResponse<object>(pagedSolicitudes);

        response.Meta = new Metadata
        {
            TotalCount = pagedSolicitudes.TotalCount,
            PageSize = pagedSolicitudes.PageSize,
            CurrentPage = pagedSolicitudes.CurrentPage,
            TotalPages = pagedSolicitudes.TotalPages,
            HasNextPage = pagedSolicitudes.HasNextPage,
            HasPreviousPage = pagedSolicitudes.HasPreviousPage,
            NextPageNumber = pagedSolicitudes.NextPageNumber,
            PreviousPageNumber = pagedSolicitudes.PreviousPageNumber
        };

        return Ok(response);
    }

    /// <summary>
    /// Obtiene una solicitud específica por su identificador.
    /// </summary>
    /// <remarks>
    /// Este método busca la solicitud en la base de datos y la mapea a un DTO.
    /// </remarks>
    /// <param name="id">ID único de la solicitud.</param>
    /// <returns>El DTO de la solicitud encontrada.</returns>
    /// <response code="200">Retorna la solicitud encontrada</response>
    /// <response code="404">No existe una solicitud con ese ID</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType(
        (int)HttpStatusCode.OK,
        Type = typeof(ApiResponse<IEnumerable<AdminManageSolicitudDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Produces("application/json")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(long id)
    {
        var solicitud = await _adminService.GetSolicitudByIdAsync(id);
        if (solicitud == null) return NotFound(new { message = "Solicitud no encontrada" });

        return Ok(solicitud);
    }

    /// <summary>
    /// Aprueba o rechaza una solicitud (cambia su estado).
    /// </summary>
    /// <remarks>
    /// Este método actualiza el estado de la solicitud en el sistema. 
    /// Solo los administradores pueden ejecutar esta acción.
    /// </remarks>
    /// <param name="id">ID de la solicitud a gestionar.</param>
    /// <param name="dto">Objeto que contiene el nuevo estado (aprobado/rechazado).</param>
    /// <returns>Mensaje de éxito.</returns>
    /// <response code="200">La solicitud fue gestionada exitosamente</response>
    /// <response code="400">Datos inválidos o estado no permitido</response>
    /// <response code="404">Solicitud no encontrada</response>
    /// <response code="500">Error interno del servidor</response>
    [ProducesResponseType(
        (int)HttpStatusCode.OK,
        Type = typeof(ApiResponse<IEnumerable<AdminManageSolicitudDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Produces("application/json")]
    [HttpPut("{id}/manage")]
    public async Task<IActionResult> Gestionar(long id, [FromBody] GestionarSolicitudDto dto)
    {
        await _adminService.GestionarSolicitudAsync(id, dto);
        return Ok(new { message = $"La solicitud fue {dto.Estado} exitosamente." });
    }
}