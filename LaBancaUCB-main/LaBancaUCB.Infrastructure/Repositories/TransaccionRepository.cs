using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class TransaccionRepository
{
    private readonly LaBancaUCBContext _context;
    public TransaccionRepository(LaBancaUCBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Transaccion>> GetAllTransaccionesAsync()
    {
        var transacciones = await _context.Transacciones.ToListAsync();
        return transacciones;
    }

    public async Task<Transaccion?> GetTransaccionByIdAsync(long id)
    {
        var transaccion = await _context.Transacciones.FindAsync(id);
        return transaccion;
    }

    public async Task InsertTransaccion(Transaccion transaccion)
    {
        _context.Transacciones.Add(transaccion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTransaccion(Transaccion transaccion)
    {
        _context.Transacciones.Update(transaccion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransaccion(Transaccion transaccion)
    {
        _context.Transacciones.Remove(transaccion);
        await _context.SaveChangesAsync();
    }
}
