using FluentValidation;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;

namespace LaBancaUCB.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
/// <summary>
/// Endpoints para consultar el balance mediante un DTO de petición.
/// </summary>
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

    /// <summary>
    /// Obtiene el balance para los datos suministrados en el DTO.
    /// </summary>
    /// <response code="200">Retorna el objeto con la información de balance</response>
    /// <response code="400">Request inválido</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPost("get-balance")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(object))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> GetBalance([FromBody] BalanceDto balanceDto)
    {
        await _balanceValidator.ValidateAndThrowAsync(balanceDto);

        var responseDto = await _balanceResponseService.GetBalanceAsync(balanceDto);
        return Ok(responseDto);
    }
}