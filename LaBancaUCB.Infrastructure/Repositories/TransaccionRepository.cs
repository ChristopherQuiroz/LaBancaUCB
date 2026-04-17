using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class TransaccionRepository : BaseRepository<Transaccion>, ITransaccionRepository
{
    private readonly LaBancaUCBContext _context;

    public TransaccionRepository(LaBancaUCBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> GetCantidadPendientesAsync(long idUsuario)
    {
        return await _context.Transacciones
            .CountAsync(t => t.IdCuentaOrigenNavigation.IdUsuario == idUsuario
                          && t.Estado == "pendiente");
    }
}