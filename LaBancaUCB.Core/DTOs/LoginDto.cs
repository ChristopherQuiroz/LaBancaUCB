namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Credenciales de inicio de sesión.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    /// <example>usuario@example.com</example>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Contraseña del usuario.
    /// </summary>
    /// <example>MiClave123</example>
    public string Password { get; set; } = null!;
}