using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
namespace LaBancaUCB.Services.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IBaseRepository<Usuario> _usuarioRepository;

    public UsuarioService(IBaseRepository<Usuario> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        return await _usuarioRepository.GetAllAsync();
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(long id)
    {
        return await _usuarioRepository.GetByIdAsync(id);
    }

    public async Task InsertUsuarioAsync(Usuario usuario)
    {
        if (ContienePalabraNoPermitida(usuario.NombreCompleto))
            throw new Exception("El contenido no es permitido");

        await _usuarioRepository.AddAsync(usuario);
    }

    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(usuario.IdUsuario);
        if (usuarioExistente == null)
            throw new Exception("El usuario no existe");

        await _usuarioRepository.UpdateAsync(usuario);
    }

    public async Task DeleteUsuarioAsync(long id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            throw new Exception("El usuario no existe");

        await _usuarioRepository.DeleteAsync(id);
    }

    private readonly string[] _palabrasNoPermitidas =
    {
        "violencia", "odio", "maricon", "pornografia", "pete"
    };

    private bool ContienePalabraNoPermitida(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return false;

        foreach (var palabra in _palabrasNoPermitidas)
        {
            if (texto.Contains(palabra, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
