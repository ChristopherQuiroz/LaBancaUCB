namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para que un administrador gestione (apruebe/rechace) un préstamo.
/// </summary>
public class GestionarPrestamoDto
{
    /// <summary>
    /// Monto aprobado (puede ser menor al solicitado).
    /// </summary>
    /// <example>5000.00</example>
    public decimal MontoAprobado { get; set; }

    /// <summary>
    /// Tasa de interés anual aplicada (en porcentaje).
    /// </summary>
    /// <example>12.5</example>
    public decimal TasaInteres { get; set; }

    /// <summary>
    /// Nuevo estado del préstamo: "aprobado", "rechazado", "pagado".
    /// </summary>
    /// <example>aprobado</example>
    public string Estado { get; set; } = null!;
}