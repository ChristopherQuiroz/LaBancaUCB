using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaBancaUCB.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CuentaController : ControllerBase
{
    private readonly ICuentaService _cuentaService;
    private readonly IMapper _mapper;

    public CuentaController(ICuentaService cuentaService, IMapper mapper)
    {
        _cuentaService = cuentaService;
        _mapper = mapper;
    }

    [HttpGet("mis-cuentas")]
    public async Task<ActionResult> GetMisCuentas()
    {
        try
        {
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
            {
                return Unauthorized(new { message = "Token inválido o usuario no identificado" });
            }

            var cuentas = await _cuentaService.GetCuentasByUsuarioIdAsync(idUsuario);

            var cuentasDto = _mapper.Map<IEnumerable<CuentaDto>>(cuentas);

            var response = new ApiResponse<IEnumerable<CuentaDto>>(cuentasDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al consultar las cuentas",
                error = ex.Message
            });
        }
    }
}