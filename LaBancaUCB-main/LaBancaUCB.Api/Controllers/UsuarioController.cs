using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IMapper _mapper;
    private readonly CrearUsuarioValidator _crearValidator;
    private readonly ActualizarUsuarioValidator _actualizarValidator;

    public UsuarioController(
        IUsuarioService usuarioService,
        IMapper mapper,
        CrearUsuarioValidator crearValidator,
        ActualizarUsuarioValidator actualizarValidator)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
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
        if (usuario == null) return NotFound("Usuario no encontrado");
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        var response = new ApiResponse<UsuarioDto>(usuarioDto);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Insert(UsuarioDto usuarioDto)
    {
        try
        {
            var validatorResult = await _crearValidator.ValidateAsync(usuarioDto);
            if (!validatorResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validatorResult.Errors.Select(e => new
                    {
                        property = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.PasswordHash = usuarioDto.PasswordHash ?? "sin_password";
            await _usuarioService.InsertUsuarioAsync(usuario);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Created("", response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al crear el usuario",
                error = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long id, [FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            if (id != usuarioDto.IdUsuario)
                return BadRequest("El ID del usuario no coincide");

            var validatorResult = await _actualizarValidator.ValidateAsync(usuarioDto);
            if (!validatorResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validatorResult.Errors.Select(e => new
                    {
                        property = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.PasswordHash = usuarioDto.PasswordHash ?? "sin_password";
            await _usuarioService.UpdateUsuarioAsync(usuario);
            var response = new ApiResponse<UsuarioDto>(usuarioDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al actualizar el usuario",
                error = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        try
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error al eliminar el usuario",
                error = ex.Message
            });
        }
    }
}