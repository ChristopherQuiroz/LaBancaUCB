using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Core.Interfaces;

public interface ITransaccionRepository : IBaseRepository<Transaccion>
{
    Task<int> GetCantidadPendientesAsync(long idUsuario);
}
