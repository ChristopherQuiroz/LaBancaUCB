using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.Extensions.Configuration;

namespace LaBancaUCB.Services.Services;

public class BalanceResponseService
{
    private readonly IBaseRepository<Cuenta> _cuentaResponseRepository;
    private readonly IConfiguration _configuration;

    public BalanceResponseService(
        IBaseRepository<Cuenta> cuentaResponseRepository,
        IConfiguration configuration)
    {
        _cuentaResponseRepository = cuentaResponseRepository;
        _configuration = configuration;
    }

    public async Task<BalanceDto> GetBalanceByAccountAsync(Cuenta cuenta)
    {
        var balance = await _cuentaResponseRepository.GetByIdAsync(cuenta.Id);
        if (balance == null)
        {
            throw new Exception("Cuenta no encontrada");
        }
        return new BalanceDto { Balance = balance.Saldo };
    }
}
