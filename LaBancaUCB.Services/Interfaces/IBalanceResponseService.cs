using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Interfaces;

public interface IBalanceResponseService
{
    Task<BalanceDto> GetBalanceAsync(BalanceDto balanceDto);
}
