namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Respuesta después de un login exitoso, contiene el token JWT.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Token JWT para autenticación en futuras peticiones.
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Correo del usuario autenticado.
    /// </summary>
    /// <example>usuario@example.com</example>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    /// <example>Juan Pérez</example>
    public string NombreCompleto { get; set; } = null!;

    /// <summary>
    /// Rol del usuario: "admin", "cliente".
    /// </summary>
    /// <example>cliente</example>
    public string Rol { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de expiración del token.
    /// </summary>
    /// <example>2025-12-31T23:59:59Z</example>
    public DateTime Expiracion { get; set; }
}