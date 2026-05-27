namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información de una cuenta bancaria.
/// </summary>
public class CuentaDto
{
    /// <summary>
    /// Identificador único de la cuenta.
    /// </summary>
    /// <example>5001</example>
    public long IdCuenta { get; set; }

    /// <summary>
    /// Número de cuenta (formato IBAN o local).
    /// </summary>
    /// <example>0001234567890123</example>
    public string NumeroCuenta { get; set; } = null!;

    /// <summary>
    /// Tipo de cuenta: "ahorro", "corriente", "empresarial".
    /// </summary>
    /// <example>ahorro</example>
    public string TipoCuenta { get; set; } = null!;

    /// <summary>
    /// Saldo actual de la cuenta.
    /// </summary>
    /// <example>12500.75</example>
    public decimal Saldo { get; set; }

    /// <summary>
    /// Moneda de la cuenta (ISO 4217).
    /// </summary>
    /// <example>PEN</example>
    public string Moneda { get; set; } = null!;

    /// <summary>
    /// Estado de la cuenta: "activa", "bloqueada", "suspendida", "cerrada".
    /// </summary>
    /// <example>activa</example>
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Fecha de apertura (formato ISO).
    /// </summary>
    /// <example>2025-01-10T09:00:00Z</example>
    public string? FechaApertura { get; set; }
}