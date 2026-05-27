using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/pagos-qr")]
[Authorize(Roles = "cliente")] // Solo los clientes pueden pagar
[Produces("application/json")]
public class PagosQrController : ControllerBase
{
    private readonly IPagoQrService _pagoQrService;

    public PagosQrController(IPagoQrService pagoQrService)
    {
        _pagoQrService = pagoQrService;
    }

    /// <summary>
    /// Procesa un pago a través de código QR.
    /// </summary>
    /// <param name="dto">Datos necesarios para procesar el pago.</param>
    /// <returns>Mensaje de resultado.</returns>
    /// <response code="200">Pago procesado correctamente</response>
    /// <response code="401">Token inválido</response>
    /// <response code="400">Datos inválidos</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [HttpPost("pagar")]
    public async Task<IActionResult> PagarConQr([FromBody] ProcesarPagoQrDto dto)
    {
        var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimId == null) return Unauthorized(new { message = "Token inválido." });
        var idUsuario = long.Parse(claimId.Value);

        await _pagoQrService.ProcesarPagoAsync(dto, idUsuario);

        return Ok(new { message = "Pago QR realizado con éxito." });
    }

    /// <summary>
    /// Genera un código QR para iniciar un pago.
    /// </summary>
    /// <param name="dto">Datos necesarios para generar el QR.</param>
    /// <returns>Código/generado.</returns>
    /// <response code="200">Código generado correctamente</response>
    /// <response code="401">Token inválido</response>
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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