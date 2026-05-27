namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Filtros para listar solicitudes (préstamos, seguros, tarjetas).
/// </summary>
public class SolicitudQueryFilter
{
    /// <summary>
    /// Filtrar por estado: "pendiente", "aprobado", "rechazado".
    /// </summary>
    /// <example>pendiente</example>
    public string? Estado { get; set; }

    /// <summary>
    /// Tamaño de página (máximo 50).
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Número de página.
    /// </summary>
    /// <example>1</example>
    public int PageNumber { get; set; } = 1;
}