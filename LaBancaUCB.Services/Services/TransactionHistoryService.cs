using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Validators;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace LaBancaUCB.Services.Services;

public class TransactionHistoryService : ITransactionHistoryService
{
    private readonly IBaseRepository<Transaccion> _transaccionRepository;
    private readonly IBaseRepository<Cuenta> _cuentaRepository;
    private readonly IConfiguration _configuration;

    public TransactionHistoryService(
        IBaseRepository<Transaccion> transaccionRepository,
        IBaseRepository<Cuenta> cuentaRepository,
        IConfiguration configuration)
    {
        _transaccionRepository = transaccionRepository;
        _cuentaRepository = cuentaRepository;
        _configuration = configuration;
    }

    public async Task<TransactionHistoryResponseDto> GetAllTransaccionHistoryAsync(int IdClient, TransactionRequestDto transaccionDto)
    {
        var todasCuentas = await _cuentaRepository.GetAllAsync();
        var cuentasCliente = todasCuentas.Where(c => c.IdUsuario == IdClient).ToList();

        var idsCuentas = cuentasCliente.Select(c => c.IdCuenta).ToList();
        var numerosCuentas = cuentasCliente.Select(c => c.NumeroCuenta).ToList();

        var todasTransacciones = await _transaccionRepository.GetAllAsync();

        var transacciones = todasTransacciones.Where(t =>
            idsCuentas.Contains(t.IdCuentaOrigen) ||
            numerosCuentas.Contains(t.IdCuentaDestino)
        ).OrderByDescending(t => t.FechaHora).ToList();

        var history = transacciones.Select(t => new TransactionRequestDto
        {
            TransactionId = t.IdTransaccion.ToString(),
            TransactionType = t.Tipo,
            NombreDestino = t.NombreDestino,
            Amount = t.Monto,
            FechaTransaccion = t.FechaHora.ToString("o")
        }).ToList();

        var response = new TransactionHistoryResponseDto
        {
            TransactionHistory = history
        };

        return response;
    }
}
