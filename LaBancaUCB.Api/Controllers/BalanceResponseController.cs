using FluentValidation;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        await _balanceValidator.ValidateAndThrowAsync(balanceDto);

        var responseDto = await _balanceResponseService.GetBalanceAsync(balanceDto);
        return Ok(responseDto);
    }
}