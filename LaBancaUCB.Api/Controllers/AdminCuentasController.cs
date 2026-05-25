using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/admin/cuentas")]
[Authorize(Roles = "admin")]
public class AdminCuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;
    private readonly IValidator<EstadoCuentaDto> _validator;
    private readonly IMapper _mapper;

    public AdminCuentasController(
        ICuentaService cuentaService,
        IValidator<EstadoCuentaDto> validator,
        IMapper mapper)
    {
        _cuentaService = cuentaService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ListarCuentas([FromQuery] CuentaQueryFilterDto filters)
    {
        var pagedCuentas = await _cuentaService.GetCuentasPaginadasAsync(filters);

        var cuentasDto = _mapper.Map<IEnumerable<CuentaDto>>(pagedCuentas);

        var response = new ApiResponse<IEnumerable<CuentaDto>>(cuentasDto)
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

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(long id, [FromBody] EstadoCuentaDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);

        var claimId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimId == null) return Unauthorized(new { message = "Token inválido o expirado." });

        var idAdmin = long.Parse(claimId.Value);

        var ipOrigen = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";

        await _cuentaService.CambiarEstadoCuentaAsync(id, idAdmin, ipOrigen, dto);

        return Ok(new { message = $"Cuenta {dto.Accion} exitosamente." });
    }
}