using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
    public async Task<ActionResult> GetMisCuentas([FromQuery] PaginationFilter filters)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(idUsuarioClaim) || !long.TryParse(idUsuarioClaim, out long idUsuario))
        {
            return Unauthorized(new { message = "Token inválido o usuario no identificado" });
        }

        var pagedCuentas = await _cuentaService.GetCuentasByUsuarioIdAsync(idUsuario, filters);
        var cuentasDto = _mapper.Map<IEnumerable<CuentaDto>>(pagedCuentas);

        var response = new Api.Response.ApiResponse<IEnumerable<CuentaDto>>(cuentasDto)
        {
            Meta = new Metadata
            {
                TotalCount = pagedCuentas.TotalCount,
                PageSize = pagedCuentas.PageSize,
                CurrentPage = pagedCuentas.CurrentPage,
                TotalPages = pagedCuentas.TotalPages,
                HasNextPage = pagedCuentas.HasNextPage,
                HasPreviousPage = pagedCuentas.HasPreviousPage,
                NextPageNumber = pagedCuentas.NextPageNumber,
                PreviousPageNumber = pagedCuentas.PreviousPageNumber
            }
        };

        return Ok(response);
    }
}