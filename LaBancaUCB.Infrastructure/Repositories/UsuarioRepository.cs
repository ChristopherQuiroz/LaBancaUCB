using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly LaBancaUCBContext _context;

    public UsuarioRepository(LaBancaUCBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios;
    }
    public async Task<Usuario?> GetUsuarioByIdAsync(long id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        return usuario;
    }

    public async Task InsertUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}