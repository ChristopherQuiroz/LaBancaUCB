using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(long id);
    Task InsertUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(Usuario usuario);
    Task DeleteUsuarioAsync(Usuario usuario);
}