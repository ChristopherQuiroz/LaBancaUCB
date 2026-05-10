using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Interfaces;

public interface ITransactionRequestService
{
    Task<TransactionRequestDto> GetTransactionAsync(long transactionId);
}
