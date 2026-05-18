namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Representa una solicitud que será administrada por un administrador.
/// </summary>
/// <remarks>
/// Este DTO contiene la información necesaria para que el administrador visualice y gestione la solicitud.
/// </remarks>
public class AdminManageSolicitudDto
{
    /// <summary>
    /// Identificador único de la solicitud.
    /// </summary>
    /// <example>12075</example>
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Estado actual de la solicitud (pendiente, aprobado, rechazado).
    /// </summary>
    /// <example>pendiente</example>
    public string? ObservacionAdmin { get; set; }

    /// <summary>
    /// Fecha en que se creó la solicitud.
    /// </summary>
    /// <example>2025-03-15T10:30:00Z</example>
    public decimal? MontoAprobado { get; set; }
}
