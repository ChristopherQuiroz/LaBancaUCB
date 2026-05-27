namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Filtros para listar transacciones de un usuario.
/// </summary>
public class TransaccionQueryFilter
{
    /// <summary>
    /// Estado de la transacción.
    /// </summary>
    /// <example>completada</example>
    public string? Estado { get; set; }

    /// <summary>
    /// Tipo de transacción.
    /// </summary>
    /// <example>transferencia</example>
    public string? Tipo { get; set; }

    /// <summary>
    /// Texto a buscar en la glosa.
    /// </summary>
    /// <example>servicio</example>
    public string? Glosa { get; set; }

    /// <summary>
    /// Fecha específica (formato YYYY-MM-DD).
    /// </summary>
    /// <example>2025-03-15</example>
    public string? Fecha { get; set; }

    /// <summary>
    /// Tamaño de página.
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Número de página.
    /// </summary>
    /// <example>1</example>
    public int PageNumber { get; set; } = 1;
}