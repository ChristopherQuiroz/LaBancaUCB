using AutoMapper;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        return Ok(usuariosDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(long id)
    {
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuario == null) return NotFound("Usuario no encontrado");
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        return Ok(usuarioDto);
    }

    [HttpPost]
    public async Task<ActionResult> Insert(UsuarioDto usuarioDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.PasswordHash = usuarioDto.PasswordHash ?? "sin_password";
        await _usuarioRepository.InsertUsuarioAsync(usuario);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long id, [FromBody] UsuarioDto usuarioDto)
    {
        if (id != usuarioDto.IdUsuario)
            return BadRequest("El ID del usuario no coincide");

        var usuarioExistente = await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuarioExistente == null) return NotFound("Usuario no encontrado");

        usuarioExistente.Email = usuarioDto.Email;
        usuarioExistente.NombreCompleto = usuarioDto.NombreCompleto;
        usuarioExistente.Rol = usuarioDto.Rol;
        usuarioExistente.Activo = usuarioDto.Activo ?? usuarioExistente.Activo;
        usuarioExistente.Bloqueado = usuarioDto.Bloqueado ?? usuarioExistente.Bloqueado;

        await _usuarioRepository.UpdateUsuarioAsync(usuarioExistente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuario == null) return NotFound("Usuario no encontrado");

        await _usuarioRepository.DeleteUsuarioAsync(usuario);
        return NoContent();
    }
}