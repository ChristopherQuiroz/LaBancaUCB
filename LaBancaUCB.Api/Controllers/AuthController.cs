using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
/// <summary>
/// Endpoints de autenticación pública (login).
/// </summary>
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly LoginDtoValidator _loginValidator;

    public AuthController(IAuthService authService, LoginDtoValidator loginValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// Realiza login y retorna datos de sesión/token.
    /// </summary>
    /// <response code="200">Login exitoso</response>
    /// <response code="400">Request inválido</response>
    /// <response code="401">Credenciales inválidas</response>
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoginResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        await _loginValidator.ValidateAndThrowAsync(loginDto);
        var responseDto = await _authService.LoginAsync(loginDto);

        var response = new ApiResponse<LoginResponseDto>(responseDto!);
        return Ok(response);
    }
}