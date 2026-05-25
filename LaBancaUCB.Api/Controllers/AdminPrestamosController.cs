using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/prestamos")]
[Authorize(Roles = "admin")]
public class AdminPrestamosController : ControllerBase
{
    private readonly IPrestamoService _prestamoService;

    public AdminPrestamosController(IPrestamoService prestamoService)
    {
        _prestamoService = prestamoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedPrestamos = await _prestamoService.GetPrestamosPaginadosAsync(pageNumber, pageSize);

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

    [HttpPut("{id}/gestionar")]
    public async Task<IActionResult> Gestionar(long id, [FromBody] GestionarPrestamoDto dto)
    {
        await _prestamoService.ActualizarPrestamoAsync(id, dto);
        return Ok(new { message = "Préstamo gestionado exitosamente." });
    }
}