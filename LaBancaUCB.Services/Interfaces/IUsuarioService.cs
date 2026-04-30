using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace LaBancaUCB.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(long id);
    Task InsertUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(Usuario usuario);
    Task DeleteUsuarioAsync(long id);
    Task ChangePasswordAsync(long idUsuario, ChangePasswordDto dto);
}