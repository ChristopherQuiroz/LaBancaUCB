namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para solicitar un nuevo préstamo.
/// </summary>
public class PrestamoRequestDto
{
    /// <summary>
    /// Identificador de la cuenta donde se depositará el préstamo.
    /// </summary>
    /// <example>5001</example>
    public long IdCuenta { get; set; }

    /// <summary>
    /// Monto solicitado.
    /// </summary>
    /// <example>10000.00</example>
    public decimal MontoSolicitado { get; set; }

    /// <summary>
    /// Tasa de interés anual propuesta (en porcentaje).
    /// </summary>
    /// <example>12.0</example>
    public decimal TasaInteres { get; set; }

    /// <summary>
    /// Plazo en meses para pagar el préstamo.
    /// </summary>
    /// <example>24</example>
    public int PlazoMeses { get; set; }
}