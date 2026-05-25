using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly LaBancaUCBContext _context;
    private readonly IDapperContext _dapper; 

    public CuentaRepository(LaBancaUCBContext context, IDapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public async Task<IEnumerable<Cuenta>> GetAllCuentasAsync()
    {
        return await _context.Cuentas.ToListAsync();
    }

    public async Task<Cuenta?> GetCuentaByIdAsync(long id)
    {
        return await _context.Cuentas.FindAsync(id);
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
    public async Task<decimal> GetSumaSaldosDapperAsync(long idUsuario)
    {
        string sql = "SELECT COALESCE(SUM(saldo), 0) FROM Cuentas WHERE id_usuario = @Id";

        return await _dapper.QuerySingleOrDefaultAsync<decimal>(sql, new { Id = idUsuario });
    }
}