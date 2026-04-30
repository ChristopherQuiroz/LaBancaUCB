using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FluentValidation;
using AutoMapper;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Validators;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public async Task<IActionResult> GetAllMisBeneficiarios()
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var todos = await _beneficiarioService.GetAllBeneficiariosAsync();
        var misBeneficiarios = todos.Where(b => b.IdUsuarioOwner == idUsuario);

        return Ok(_mapper.Map<IEnumerable<BeneficiarioDto>>(misBeneficiarios));
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