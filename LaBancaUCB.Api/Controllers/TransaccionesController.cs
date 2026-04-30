using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using System.Threading.Tasks;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransaccionesController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionesController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    [HttpPost("transferencia-exterior")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> CrearTransferenciaExterior([FromBody] TransferenciaExteriorDto dto)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var usuarioId = long.Parse(claim.Value);
        var result = await _transaccionService.CrearTransferenciaExteriorAsync(dto, usuarioId);

        return Ok(result);
    }

    [HttpGet("history")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> GetHistorial()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();

        var usuarioId = long.Parse(claim.Value);
        var historial = await _transaccionService.GetHistorialByUsuarioIdAsync(usuarioId);

        return Ok(historial);
    }
}