using AutoMapper;
using FluentValidation;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "cliente")]
[Produces("application/json")]
/// <summary>
/// Gestiona beneficiarios del usuario (listar e insertar).
/// </summary>
public class BeneficiariosController : ControllerBase
{
    private readonly IBeneficiarioService _beneficiarioService;
    private readonly IMapper _mapper;
    private readonly CrearBeneficiarioValidator _validator;

    public BeneficiariosController(IBeneficiarioService beneficiarioService, IMapper mapper, CrearBeneficiarioValidator validator)
    {
        _beneficiarioService = beneficiarioService;
        _mapper = mapper;
        _validator = validator;
    }

    /// <summary>
    /// Obtiene los beneficiarios del usuario autenticado (paginado).
    /// </summary>
    /// <response code="200">Retorna la lista paginada de beneficiarios</response>
    /// <response code="401">No autorizado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Api.Response.ApiResponse<IEnumerable<BeneficiarioDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllMisBeneficiarios([FromQuery] PaginationFilter filters)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var pagedBeneficiarios = await _beneficiarioService.GetAllBeneficiariosAsync(idUsuario, filters);

        var beneficiariosDto = _mapper.Map<IEnumerable<BeneficiarioDto>>(pagedBeneficiarios);

        var response = new Api.Response.ApiResponse<IEnumerable<BeneficiarioDto>>(beneficiariosDto)
        {
            Meta = new Metadata
            {
                TotalCount = pagedBeneficiarios.TotalCount,
                PageSize = pagedBeneficiarios.PageSize,
                CurrentPage = pagedBeneficiarios.CurrentPage,
                TotalPages = pagedBeneficiarios.TotalPages,
                HasNextPage = pagedBeneficiarios.HasNextPage,
                HasPreviousPage = pagedBeneficiarios.HasPreviousPage,
                NextPageNumber = pagedBeneficiarios.NextPageNumber,
                PreviousPageNumber = pagedBeneficiarios.PreviousPageNumber
            }
        };

        return Ok(response);
    }

    /// <summary>
    /// Inserta un nuevo beneficiario para el usuario autenticado.
    /// </summary>
    /// <response code="200">Beneficiario registrado exitosamente</response>
    /// <response code="400">Datos inválidos</response>
    /// <response code="401">No autorizado</response>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Insert([FromBody] BeneficiarioDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);

        var beneficiario = _mapper.Map<Beneficiario>(dto);
        beneficiario.IdUsuarioOwner = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        beneficiario.CreadoEn = System.DateTime.UtcNow;
        await _beneficiarioService.InsertBeneficiarioAsync(beneficiario);

        return Ok(new { message = "Beneficiario registrado exitosamente." });
    }
}