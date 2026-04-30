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
public class SolicitudesController : ControllerBase
{
    private readonly IProductosService _productosService;

    public SolicitudesController(IProductosService productosService)
    {
        _productosService = productosService;
    }

    [HttpPost("tarjeta")]
    public async Task<IActionResult> SolicitarTarjeta([FromBody] SolicitarTarjetaDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _productosService.SolicitarTarjetaAsync(dto, idUsuario);
        return Ok(new { message = "Solicitud de tarjeta enviada correctamente." });
    }
}