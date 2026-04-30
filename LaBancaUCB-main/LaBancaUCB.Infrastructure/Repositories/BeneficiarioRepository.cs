using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class BeneficiarioRepository
{
    private readonly LaBancaUCBContext _context;
    public BeneficiarioRepository(LaBancaUCBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Beneficiario>> GetAllBeneficiariosAsync()
    {
        var beneficiarios = await _context.Beneficiarios.ToListAsync();
        return beneficiarios;
    }

    public async Task<Beneficiario?> GetBeneficiarioByIdAsync(long id)
    {
        var beneficiario = await _context.Beneficiarios.FindAsync(id);
        return beneficiario;
    }

    public async Task InsertBeneficiarioAsync(Beneficiario beneficiario)
    {
        _context.Beneficiarios.Add(beneficiario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBeneficiarioAsync(Beneficiario beneficiario)
    {
        _context.Beneficiarios.Update(beneficiario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBeneficiarioAsync(Beneficiario beneficiario)
    {
        _context.Beneficiarios.Remove(beneficiario);
        await _context.SaveChangesAsync();
    }
}
