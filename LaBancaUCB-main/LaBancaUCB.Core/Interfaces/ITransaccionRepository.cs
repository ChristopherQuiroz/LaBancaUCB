using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Core.Interfaces;

public interface ITransaccionRepository
{
    Task<IEnumerable<Transaccion>> GetAllTransaccionesAsync();
    Task<Transaccion> GetTransaccionByIdAsync(long id);
    Task InsertTransaccionAsync(Transaccion transaccion);
    Task UpdateTransaccionAsync(Transaccion transaccion);
    Task DeleteTransaccionAsync(Transaccion transaccion);
}
