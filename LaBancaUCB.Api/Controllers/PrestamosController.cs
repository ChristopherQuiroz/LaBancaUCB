using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "cliente")]
public class PrestamosController : ControllerBase
{
    private readonly IProductosService _productosService;

    public PrestamosController(IProductosService productosService)
    {
        _productosService = productosService;
    }

    [HttpPost("solicitar")]
    public async Task<IActionResult> SolicitarPrestamo([FromBody] CrearPrestamoDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _productosService.SolicitarPrestamoAsync(dto, idUsuario);
        return Ok(new { message = "Solicitud de préstamo enviada correctamente." });
    }
}
