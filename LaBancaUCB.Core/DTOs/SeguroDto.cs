namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información detallada de un seguro contratado.
/// </summary>
public class SeguroDto
{
    /// <summary>
    /// Identificador del seguro.
    /// </summary>
    /// <example>3001</example>
    public long IdSeguro { get; set; }

    /// <summary>
    /// Cuenta asociada.
    /// </summary>
    /// <example>5001</example>
    public long IdCuenta { get; set; }

    /// <summary>
    /// Tipo de seguro.
    /// </summary>
    /// <example>vida</example>
    public string TipoSeguro { get; set; } = null!;

    /// <summary>
    /// Prima mensual.
    /// </summary>
    /// <example>120.50</example>
    public decimal PrimaMensual { get; set; }

    /// <summary>
    /// Cobertura máxima.
    /// </summary>
    /// <example>100000.00</example>
    public decimal Cobertura { get; set; }

    /// <summary>
    /// Estado: "activo", "cancelado", "vencido".
    /// </summary>
    /// <example>activo</example>
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Fecha de inicio del seguro.
    /// </summary>
    /// <example>2025-01-01T00:00:00Z</example>
    public DateTime FechaInicio { get; set; }

    /// <summary>
    /// Fecha de fin del seguro.
    /// </summary>
    /// <example>2026-01-01T00:00:00Z</example>
    public DateTime FechaFin { get; set; }
}