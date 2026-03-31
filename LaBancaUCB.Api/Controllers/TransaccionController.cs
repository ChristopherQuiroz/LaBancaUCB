using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LaBancaUCB.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransaccionController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;
    private readonly IMapper _mapper;

    public TransaccionController(ITransaccionService transaccionService, IMapper mapper)
    {
        _transaccionService = transaccionService;
        _mapper = mapper;
    }

    [HttpGet("history")]
    public async Task<ActionResult> GetHistorial()
    {
        try
        {
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
            {
                return Unauthorized(new { message = "Token inválido o usuario no identificado" });
            }

            var transacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario);
            var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(transacciones);

            var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al consultar el historial de movimientos",
                error = ex.Message
            });
        }

    }

    [Authorize(Roles = "admin")]
    [HttpGet("history/{idUsuario}")]
    public async Task<ActionResult> GetHistorialPorCliente(long idUsuario)
    {
        try
        {
            var transacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario);

            var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(transacciones);

            var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al consultar el historial del cliente",
                error = ex.Message
            });
        }
    }
}
