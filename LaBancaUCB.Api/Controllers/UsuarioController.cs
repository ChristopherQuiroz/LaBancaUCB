using AutoMapper;
using FluentValidation; 
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IMapper _mapper;
    private readonly CrearUsuarioValidator _crearValidator;
    private readonly ActualizarUsuarioValidator _actualizarValidator;
    private readonly ChangePasswordDtoValidator _changePasswordValidator;

    public UsuarioController(
        IUsuarioService usuarioService,
        IMapper mapper,
        CrearUsuarioValidator crearValidator,
        ActualizarUsuarioValidator actualizarValidator,
        ChangePasswordDtoValidator changePasswordValidator)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
        _changePasswordValidator = changePasswordValidator;
    }

    [HttpPost("change-password")]
    [Authorize(Roles = "cliente")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        await _changePasswordValidator.ValidateAndThrowAsync(dto);

        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();
        var usuarioId = long.Parse(claim.Value);

        await _usuarioService.ChangePasswordAsync(usuarioId, dto.CurrentPassword, dto.NewPassword);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(long id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null) return NotFound(new { Message = "Usuario no encontrado" });

        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        var response = new ApiResponse<UsuarioDto>(usuarioDto);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Insert(UsuarioDto usuarioDto)
    {
        await _crearValidator.ValidateAndThrowAsync(usuarioDto);

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.PasswordHash = usuarioDto.PasswordHash ?? "sin_password";

        await _usuarioService.InsertUsuarioAsync(usuario);

        var response = new ApiResponse<UsuarioDto>(usuarioDto);
        return Created("", response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long id, [FromBody] UsuarioDto usuarioDto)
    {
        if (id != usuarioDto.IdUsuario)
            return BadRequest(new { Message = "El ID del usuario no coincide con la URL" });

        await _actualizarValidator.ValidateAndThrowAsync(usuarioDto);

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.PasswordHash = usuarioDto.PasswordHash ?? "sin_password";

        await _usuarioService.UpdateUsuarioAsync(usuario);

        var response = new ApiResponse<UsuarioDto>(usuarioDto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _usuarioService.DeleteUsuarioAsync(id);
        return NoContent();
    }
}