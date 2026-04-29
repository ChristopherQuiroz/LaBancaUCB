using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeneficiariosController : ControllerBase
{
    private readonly IBeneficiarioService _beneficiarioService;

    public BeneficiariosController(IBeneficiarioService beneficiarioService)
    {
        _beneficiarioService = beneficiarioService;
    }

    [HttpPost]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Crear([FromBody] BeneficiarioDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var beneficiario = new Beneficiario
            {
                IdUsuarioOwner = usuarioId,
                Alias = dto.Alias,
                NumeroCuentaDestino = dto.NumeroCuentaDestino,
                BancoDestino = dto.BancoDestino,
                NombreTitular = dto.NombreTitular,
                EsExterior = dto.EsExterior,
                CreadoEn = DateTime.UtcNow
            };

            await _beneficiarioService.InsertBeneficiarioAsync(beneficiario);
            return CreatedAtAction(nameof(Obtener), new { id = beneficiario.Id }, beneficiario);
        }
        catch (DbUpdateException dbEx)
        {
            return BadRequest(new { error = dbEx.Message, detail = dbEx.InnerException?.Message });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message, detail = ex.InnerException?.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Obtener(long id)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var existente = await _beneficiarioService.GetBeneficiarioByIdAsync(id);
            if (existente == null) return NotFound();
            if (existente.IdUsuarioOwner != usuarioId) return Forbid();

            return Ok(existente);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var todos = await _beneficiarioService.GetAllBeneficiariosAsync();
            var propios = todos.Where(b => b.IdUsuarioOwner == usuarioId);
            return Ok(propios);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Actualizar(long id, [FromBody] BeneficiarioDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var existente = await _beneficiarioService.GetBeneficiarioByIdAsync(id);
            if (existente == null) return NotFound();
            if (existente.IdUsuarioOwner != usuarioId) return Forbid();

            existente.Alias = dto.Alias;
            existente.NumeroCuentaDestino = dto.NumeroCuentaDestino;
            existente.BancoDestino = dto.BancoDestino;
            existente.NombreTitular = dto.NombreTitular;
            existente.EsExterior = dto.EsExterior;

            await _beneficiarioService.UpdateBeneficiarioAsync(existente);
            return Ok(existente);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Eliminar(long id)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var existente = await _beneficiarioService.GetBeneficiarioByIdAsync(id);
            if (existente == null) return NotFound();
            if (existente.IdUsuarioOwner != usuarioId) return Forbid();

            await _beneficiarioService.DeleteBeneficiarioAsync(id);
            return Ok(new { message = "Eliminado" });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
