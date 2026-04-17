using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.Extensions.Configuration;
namespace LaBancaUCB.Services.Services;

public class TransactionRequestService
{
    private readonly IBaseRepository<Transaccion> _transaccionRepository;
    private readonly IConfiguration _configuration;

    public TransactionRequestService(
        IBaseRepository<Transaccion> transaccionRepository,
        IConfiguration configuration)
    {
        _transaccionRepository = transaccionRepository;
        _configuration = configuration;
    }

    public async Task<TransactionRequestDto> ProcessTransactionRequestAsync(TransactionRequestDto transaccionDto)
    {
        var validator = new TransactionRequestValidator();
        var validationResult = await validator
            .ValidateAsync(transaccionDto);
        if (validationResult == null)
        {
            throw new Exception("Validation failed: No validation result.");
        }
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {errors}");
        }
        return transaccionDto;
    }
}
