using AutoMapper;
using FluentValidation; 
using LaBancaUCB.Api.Response;
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
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
/// <summary>
/// Gestión de usuarios (endpoints administrables).
/// </summary>
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

    /// <summary>
    /// Obtiene la lista paginada de usuarios (solo admin).
    /// </summary>
    /// <response code="200">Retorna la lista paginada de usuarios</response>
    /// <response code="401">No autorizado</response>
    /// <response code="500">Error interno del servidor</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UsuarioDto>>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] UsuarioQueryFilter filters)
    {
        var pagedUsuarios = await _usuarioService.GetAllUsuariosAsync(filters);
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(pagedUsuarios);
        var response = new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto);
        
        response.Meta = new Metadata
        {
            TotalCount = pagedUsuarios.TotalCount,
            PageSize = pagedUsuarios.PageSize,
            CurrentPage = pagedUsuarios.CurrentPage,
            TotalPages = pagedUsuarios.TotalPages,
            HasNextPage = pagedUsuarios.HasNextPage,
            HasPreviousPage = pagedUsuarios.HasPreviousPage,
            NextPageNumber = pagedUsuarios.NextPageNumber,
            PreviousPageNumber = pagedUsuarios.PreviousPageNumber
        };

        return Ok(response);
    }

    /// <summary>
    /// Obtiene un usuario por Id (solo admin).
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <response code="200">Retorna el usuario solicitado</response>
    /// <response code="404">Usuario no encontrado</response>
    /// <response code="401">No autorizado</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UsuarioDto>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(long id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        if (usuario == null) return NotFound(new { Message = "Usuario no encontrado" });

        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        var response = new ApiResponse<UsuarioDto>(usuarioDto);
        return Ok(response);
    }

    /// <summary>
    /// Inserta un nuevo usuario (solo admin).
    /// </summary>
    /// <response code="201">Usuario creado correctamente</response>
    /// <response code="400">Datos inválidos</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ApiResponse<UsuarioDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

    /// <summary>
    /// Actualiza un usuario (solo admin).
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <response code="200">Usuario actualizado</response>
    /// <response code="400">Datos inválidos</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UsuarioDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

    /// <summary>
    /// Elimina un usuario (solo admin).
    /// </summary>
    /// <param name="id">Identificador del usuario</param>
    /// <response code="204">Eliminado correctamente</response>
    /// <response code="401">No autorizado</response>
    [Authorize(Roles = "admin")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _usuarioService.DeleteUsuarioAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Cambia la contraseña del usuario autenticado.
    /// </summary>
    /// <response code="200">Contraseña actualizada</response>
    /// <response code="401">No autorizado</response>
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var idUsuario = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await _usuarioService.ChangePasswordAsync(idUsuario, dto);

        return Ok(new { Message = "Contraseña actualizada correctamente" });
    }
}