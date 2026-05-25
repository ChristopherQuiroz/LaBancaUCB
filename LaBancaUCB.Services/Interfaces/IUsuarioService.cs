using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Interfaces;

public interface IUsuarioService
{
    Task<PagedList<Usuario>> GetAllUsuariosAsync(UsuarioQueryFilter filters);
    Task<Usuario?> GetUsuarioByIdAsync(long id);
    Task InsertUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(Usuario usuario);
    Task DeleteUsuarioAsync(long id);
    Task ChangePasswordAsync(long idUsuario, ChangePasswordDto dto);
}