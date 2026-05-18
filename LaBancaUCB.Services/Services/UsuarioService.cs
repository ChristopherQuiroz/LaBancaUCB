using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.CustomEntities;
using System.Net;

namespace LaBancaUCB.Services.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<Usuario>> GetAllUsuariosAsync(UsuarioQueryFilter filters)
    {
        var todos = await _unitOfWork.UsuarioRepository.GetAllAsync();
        var query = todos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Rol))
        {
            query = query.Where(u => u.Rol.Equals(filters.Rol, StringComparison.OrdinalIgnoreCase));
        }
        if (filters.Activo.HasValue)
        {
            query = query.Where(u => u.Activo == filters.Activo.Value);
        }

        var usuariosOrdenados = query.OrderByDescending(u => u.FechaDeCreacion);

        var pagedUsuarios = PagedList<Usuario>.Create(usuariosOrdenados, filters.PageNumber, filters.PageSize);

        return pagedUsuarios;
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(long id)
    {
        return await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
    }

    public async Task InsertUsuarioAsync(Usuario usuario)
    {
        if (ContienePalabraNoPermitida(usuario.NombreCompleto))
            throw new BusinessException("El contenido del nombre no es permitido por políticas de seguridad.", HttpStatusCode.BadRequest);

        await _unitOfWork.UsuarioRepository.AddAsync(usuario);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateUsuarioAsync(Usuario usuario)
    {
        var usuarioExistente = await _unitOfWork.UsuarioRepository.GetByIdAsync(usuario.IdUsuario);
        if (usuarioExistente == null)
            throw new BusinessException("El usuario que intentas actualizar no existe.", HttpStatusCode.NotFound);

        _unitOfWork.UsuarioRepository.Update(usuario);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteUsuarioAsync(long id)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            throw new BusinessException("El usuario que intentas eliminar no existe.", HttpStatusCode.NotFound);

        await _unitOfWork.UsuarioRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
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

    public async Task ChangePasswordAsync(long idUsuario, ChangePasswordDto dto)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(idUsuario);
        if (usuario == null)
            throw new BusinessException("Usuario no encontrado.", HttpStatusCode.NotFound);

        if (usuario.PasswordHash != dto.PasswordActual)
            throw new BusinessException("La contraseña actual es incorrecta.", HttpStatusCode.BadRequest);

        if (dto.PasswordActual == dto.PasswordNueva)
            throw new BusinessException("La nueva contraseña no puede ser igual a la anterior.", HttpStatusCode.BadRequest);

        usuario.PasswordHash = dto.PasswordNueva;
        _unitOfWork.UsuarioRepository.Update(usuario);
        await _unitOfWork.SaveChangesAsync();
    }
}