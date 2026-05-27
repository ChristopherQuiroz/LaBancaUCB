namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información de una sesión activa de usuario.
/// </summary>
public class SesioneDto
{
    /// <summary>
    /// Identificador de la sesión.
    /// </summary>
    /// <example>4001</example>
    public long IdSesion { get; set; }

    /// <summary>
    /// Usuario asociado.
    /// </summary>
    /// <example>101</example>
    public long IdUsuario { get; set; }

    /// <summary>
    /// JTI (JWT ID) del token.
    /// </summary>
    /// <example>abc123-def456</example>
    public string TokenJti { get; set; } = null!;

    /// <summary>
    /// Dirección IP desde donde se inició la sesión.
    /// </summary>
    /// <example>192.168.1.100</example>
    public string IpOrigen { get; set; } = null!;

    /// <summary>
    /// Indica si la sesión sigue activa.
    /// </summary>
    /// <example>true</example>
    public bool Activo { get; set; }

    /// <summary>
    /// Fecha de expiración (formato ISO).
    /// </summary>
    /// <example>2025-12-31T23:59:59Z</example>
    public string ExpiradoEn { get; set; } = null!;

    /// <summary>
    /// Fecha de creación (formato ISO).
    /// </summary>
    /// <example>2025-03-01T10:00:00Z</example>
    public string? CreadoEn { get; set; }
}