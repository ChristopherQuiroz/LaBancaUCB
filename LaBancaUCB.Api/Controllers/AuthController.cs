using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LaBancaUCB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly LoginDtoValidator _loginValidator;

    public AuthController(IAuthService authService, LoginDtoValidator loginValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var validatorResult = await _loginValidator.ValidateAsync(loginDto);
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

            var responseDto = await _authService.LoginAsync(loginDto);

            var response = new ApiResponse<LoginResponseDto>(responseDto!);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                message = "Error al iniciar sesión",
                error = ex.Message
            });
        }
    }
}
