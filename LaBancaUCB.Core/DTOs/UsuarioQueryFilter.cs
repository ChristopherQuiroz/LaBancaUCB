namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Filtros para listar usuarios (solo administradores).
/// </summary>
public class UsuarioQueryFilter
{
    /// <summary>
    /// Filtrar por rol.
    /// </summary>
    /// <example>cliente</example>
    public string? Rol { get; set; }

    /// <summary>
    /// Filtrar por estado activo.
    /// </summary>
    /// <example>true</example>
    public bool? Activo { get; set; }

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