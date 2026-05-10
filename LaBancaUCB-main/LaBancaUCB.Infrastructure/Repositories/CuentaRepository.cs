using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class CuentaRepository
{
    private readonly LaBancaUCBContext _context;
    public CuentaRepository(LaBancaUCBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Cuenta>> GetAllCuentasAsync()
    {
        var cuentas = await _context.Cuentas.ToListAsync();
        return cuentas;
    }
    public async Task<Cuenta?> GetCuentaByIdAsync(long id)
    {
        var cuenta = await _context.Cuentas.FindAsync(id);
        return cuenta;
    }
    public async Task InsertCuentaAsync(Cuenta cuenta)
    {
        _context.Cuentas.Add(cuenta);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCuentaAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteCuentaAsync(Cuenta cuenta)
    {
        _context.Cuentas.Remove(cuenta);
        await _context.SaveChangesAsync();
    }
}
