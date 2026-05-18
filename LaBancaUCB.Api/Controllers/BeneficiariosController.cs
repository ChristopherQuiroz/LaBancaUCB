using AutoMapper;
using FluentValidation;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "cliente")]
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

    [HttpGet]
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

    [HttpPost]
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