using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class SesioneRepository
{
    private readonly LaBancaUCBContext _context;
    public SesioneRepository(LaBancaUCBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Sesione>> GetAllSesionesAsync()
    {
        var sesiones = await _context.Sesiones.ToListAsync();
        return sesiones;
    }
    public async Task<Sesione?> GetSesioneByIdAsync(long id)
    {
        var sesione = await _context.Sesiones.FindAsync(id);
        return sesione;
    }
    public async Task InsertSesioneAsync(Sesione sesione)
    {
        _context.Sesiones.Add(sesione);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateSesioneAsync(Sesione sesione)
    {
        _context.Sesiones.Update(sesione);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteSesioneAsync(Sesione sesione)
    {
        _context.Sesiones.Remove(sesione);
        await _context.SaveChangesAsync();
    }
}
