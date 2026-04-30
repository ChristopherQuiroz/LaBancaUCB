using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces;

public interface ICuentaRepository
{
    Task<IEnumerable<Cuenta>> GetAllCuentasAsync();
    Task<Cuenta?> GetCuentaByIdAsync(long id);
    Task InsertCuentaAsync(Cuenta cuenta);
    Task UpdateCuentaAsync(Cuenta cuenta);
    Task DeleteCuentaAsync(Cuenta cuenta);
    Task<decimal> GetSumaSaldosDapperAsync(long idUsuario);
}
