using LaBancaUCB.Core.Enums;

namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para crear o actualizar un usuario (administrativo).
/// </summary>
public class SecurityDto
{
    /// <summary>
    /// Correo electrónico (usado como login).
    /// </summary>
    /// <example>admin@labanca.com</example>
    public string Login { get; set; } = null!;

    /// <summary>
    /// Contraseña en texto plano (será hasheada).
    /// </summary>
    /// <example>Admin123!</example>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    /// <example>Administrador del Sistema</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Rol asignado (admin, cliente, etc.).
    /// </summary>
    /// <example>admin</example>
    public RoleType? Role { get; set; }
}