using FluentValidation;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LaBancaUCB.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BalanceResponseController : ControllerBase
{
    private readonly IBalanceResponseService _balanceResponseService;
    private readonly IValidator<BalanceDto> _balanceValidator;

    public BalanceResponseController(
        IBalanceResponseService balanceResponseService,
        IValidator<BalanceDto> balanceValidator)
    {
        _balanceResponseService = balanceResponseService;
        _balanceValidator = balanceValidator;
    }

    [HttpPost("get-balance")]
    public async Task<ActionResult> GetBalance([FromBody] BalanceDto balanceDto)
    {
        try
        {
            var validationResult = await _balanceValidator.ValidateAsync(balanceDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }
            var responseDto = await _balanceResponseService.GetBalanceAsync(balanceDto);
            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                Message = $"An error occurred while processing the request: {ex.Message}",
                error = ex.Message
            });
        }
    }
}
