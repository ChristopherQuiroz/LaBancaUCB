namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Filtros para la búsqueda paginada de cuentas.
/// </summary>
public class CuentaQueryFilterDto
{
    /// <summary>
    /// Filtrar por estado de la cuenta.
    /// </summary>
    /// <example>activa</example>
    public string? Estado { get; set; }

    /// <summary>
    /// Cantidad de registros por página (por defecto 10, máximo 50).
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Número de página a consultar (empieza en 1).
    /// </summary>
    /// <example>1</example>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Identificador del usuario titular de la cuenta.
    /// </summary>
    /// <example>101</example>
    public long? UsuarioId { get; set; }
}