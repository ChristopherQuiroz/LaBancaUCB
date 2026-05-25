using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/pagos-qr")]
[Authorize(Roles = "cliente")] // Solo los clientes pueden pagar
public class PagosQrController : ControllerBase
{
    private readonly IPagoQrService _pagoQrService;

    public PagosQrController(IPagoQrService pagoQrService)
    {
        _pagoQrService = pagoQrService;
    }

    [HttpPost("pagar")]
    public async Task<IActionResult> PagarConQr([FromBody] ProcesarPagoQrDto dto)
    {
        var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimId == null) return Unauthorized(new { message = "Token inválido." });
        var idUsuario = long.Parse(claimId.Value);

        await _pagoQrService.ProcesarPagoAsync(dto, idUsuario);

        return Ok(new { message = "Pago QR realizado con éxito." });
    }

    [HttpPost("generar")]
    public async Task<IActionResult> GenerarQr([FromBody] GenerarQrDto dto)
    {
        var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimId == null) return Unauthorized(new { message = "Token inválido." });
        var idUsuario = long.Parse(claimId.Value);

        var codigoGenerado = await _pagoQrService.GenerarQrAsync(dto, idUsuario);

        return Ok(new
        {
            message = "Código QR generado exitosamente.",
            codigoHash = codigoGenerado
        });
    }
}