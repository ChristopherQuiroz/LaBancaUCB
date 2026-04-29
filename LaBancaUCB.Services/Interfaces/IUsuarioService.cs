using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(long id);
    Task InsertUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(Usuario usuario);
    Task DeleteUsuarioAsync(long id);
    Task ChangePasswordAsync(long usuarioId, string currentPassword, string newPassword);
}