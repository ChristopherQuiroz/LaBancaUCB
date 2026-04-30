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
public class SegurosController : ControllerBase
{
    private readonly IProductosService _productosService;

    public SegurosController(IProductosService productosService)
    {
        _productosService = productosService;
    }

    [HttpPost]
    public async Task<IActionResult> SolicitarSeguro([FromBody] CrearSeguroDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _productosService.SolicitarSeguroAsync(dto, idUsuario);
        return Ok(new { message = "Solicitud de seguro enviada correctamente." });
    }
}