using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using System;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SegurosController : ControllerBase
{
    private readonly ISeguroService _seguroService;
    private readonly IUnitOfWork _unitOfWork;

    public SegurosController(ISeguroService seguroService, IUnitOfWork unitOfWork)
    {
        _seguroService = seguroService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Crear([FromBody] SeguroDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            var seguro = new Seguro
            {
                IdCuenta = dto.IdCuenta,
                TipoSeguro = dto.TipoSeguro,
                PrimaMensual = dto.PrimaMensual,
                Cobertura = dto.Cobertura,
                Estado = dto.Estado,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };

            await _seguroService.InsertSeguroAsync(seguro);
            return CreatedAtAction(nameof(Obtener), new { id = seguro.IdSeguro }, seguro);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message, detail = ex.InnerException?.Message });
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

            var todos = await _seguroService.GetAllSegurosAsync();
            var propios = todos.Where(s =>
            {
                var cuenta = _unitOfWork.CuentaRepository.GetByIdAsync(s.IdCuenta).Result;
                return cuenta != null && cuenta.IdUsuario == usuarioId;
            });

            return Ok(propios);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
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

            var existente = await _seguroService.GetSeguroByIdAsync(id);
            if (existente == null) return NotFound();

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(existente.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta asociada no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            return Ok(existente);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Actualizar(long id, [FromBody] SeguroDto dto)
    {
        try
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized();
            var usuarioId = long.Parse(claim.Value);

            var existente = await _seguroService.GetSeguroByIdAsync(id);
            if (existente == null) return NotFound();

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(existente.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta asociada no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            existente.TipoSeguro = dto.TipoSeguro;
            existente.PrimaMensual = dto.PrimaMensual;
            existente.Cobertura = dto.Cobertura;
            existente.Estado = dto.Estado;
            existente.FechaInicio = dto.FechaInicio;
            existente.FechaFin = dto.FechaFin;

            await _seguroService.UpdateSeguroAsync(existente);
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

            var existente = await _seguroService.GetSeguroByIdAsync(id);
            if (existente == null) return NotFound();

            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(existente.IdCuenta);
            if (cuenta == null) return BadRequest(new { error = "Cuenta asociada no encontrada" });
            if (cuenta.IdUsuario != usuarioId) return Forbid();

            await _seguroService.DeleteSeguroAsync(id);
            return Ok(new { message = "Eliminado" });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
