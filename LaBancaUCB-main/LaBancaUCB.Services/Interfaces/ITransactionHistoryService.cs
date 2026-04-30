using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Interfaces;

public interface ITransactionHistoryService
{
    Task<TransactionHistoryResponseDto> GetAllTransaccionHistoryAsync(int IdClient,TransactionRequestDto transaccionDto);
}
