namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información de un usuario del sistema.
/// </summary>
public class UsuarioDto
{
    /// <summary>
    /// Identificador del usuario.
    /// </summary>
    /// <example>101</example>
    public long IdUsuario { get; set; }

    /// <summary>
    /// Correo electrónico.
    /// </summary>
    /// <example>juan.perez@example.com</example>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Nombre completo.
    /// </summary>
    /// <example>Juan Pérez Gómez</example>
    public string NombreCompleto { get; set; } = null!;

    /// <summary>
    /// Hash de la contraseña (no se devuelve normalmente).
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Rol: "admin" o "cliente".
    /// </summary>
    /// <example>cliente</example>
    public string Rol { get; set; } = null!;

    /// <summary>
    /// Indica si la cuenta está activa (no suspendida).
    /// </summary>
    /// <example>true</example>
    public bool? Activo { get; set; }

    /// <summary>
    /// Indica si la cuenta está bloqueada por intentos fallidos.
    /// </summary>
    /// <example>false</example>
    public bool? Bloqueado { get; set; }

    /// <summary>
    /// Fecha de creación del usuario.
    /// </summary>
    /// <example>2025-01-10T08:00:00Z</example>
    public string? FechaDeCreacion { get; set; }
}