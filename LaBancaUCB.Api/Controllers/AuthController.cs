using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using System.Threading.Tasks;

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
        await _loginValidator.ValidateAndThrowAsync(loginDto);
        var responseDto = await _authService.LoginAsync(loginDto);

        var response = new ApiResponse<LoginResponseDto>(responseDto!);
        return Ok(response);
    }
}