using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public async Task<ActionResult> GetHistorial([FromQuery] TransaccionQueryFilter filters)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
        {
            return Unauthorized(new { message = "Token inválido o usuario no identificado" });
        }

        var transacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario, filters);
        var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(transacciones);
        var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);

        return Ok(response);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("history/{idUsuario}")]
    public async Task<ActionResult> GetHistorialPorCliente(long idUsuario)
    {
        var transacciones = await _transaccionService.GetHistorialByUsuarioIdAsync(idUsuario);
        var transaccionesDto = _mapper.Map<IEnumerable<TransaccionDto>>(transacciones);
        var response = new ApiResponse<IEnumerable<TransaccionDto>>(transaccionesDto);

        return Ok(response);
    }
}