using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "admin")]
public class TransaccionesAdminController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionesAdminController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? estado, [FromQuery] PaginationFilter filters)
    {
        var pagedTransacciones = await _transaccionService.ListarTransferenciasPorEstadoAsync(estado, filters);

        var response = new ApiResponse<object>(pagedTransacciones)
        {
            Meta = new Metadata
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

    [HttpPost("{id}/aprobar")]
    public async Task<IActionResult> Aprobar(long id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var adminId = long.Parse(claim.Value);
        await _transaccionService.AprobarTransferenciaAsync(id, adminId);

        return Ok(new { message = "Aprobada" });
    }

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