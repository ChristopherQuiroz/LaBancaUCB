namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Parámetros base para paginación.
/// </summary>
public class PaginationFilter
{
    /// <summary>
    /// Cantidad de elementos por página.
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Número de página (empieza en 1).
    /// </summary>
    /// <example>1</example>
    public int PageNumber { get; set; } = 1;
}