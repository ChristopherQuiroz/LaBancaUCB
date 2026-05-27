namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos necesarios para cambiar la contraseña de un usuario.
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Contraseña actual del usuario.
    /// </summary>
    /// <example>MiClaveActual123</example>
    public string PasswordActual { get; set; } = null!;

    /// <summary>
    /// Nueva contraseña (mínimo 8 caracteres, debe incluir mayúscula, número y símbolo).
    /// </summary>
    /// <example>NuevaClave$456</example>
    public string PasswordNueva { get; set; } = null!;
}