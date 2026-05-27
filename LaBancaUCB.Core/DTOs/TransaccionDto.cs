namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información de una transacción (transferencia, pago, etc.).
/// </summary>
public class TransaccionDto
{
    /// <summary>
    /// Identificador de la transacción.
    /// </summary>
    /// <example>6001</example>
    public long IdTransaccion { get; set; }

    /// <summary>
    /// Cuenta origen (id).
    /// </summary>
    /// <example>5001</example>
    public long IdCuentaOrigen { get; set; }

    /// <summary>
    /// Cuenta destino (número de cuenta).
    /// </summary>
    /// <example>000987654321</example>
    public string IdCuentaDestino { get; set; } = null!;

    /// <summary>
    /// Nombre del titular destino.
    /// </summary>
    /// <example>Ana Rodríguez</example>
    public string NombreDestino { get; set; } = null!;

    /// <summary>
    /// Tipo de transacción: "transferencia", "pago_qr", "retiro", "depósito".
    /// </summary>
    /// <example>transferencia</example>
    public string Tipo { get; set; } = null!;

    /// <summary>
    /// Monto de la transacción.
    /// </summary>
    /// <example>250.00</example>
    public decimal Monto { get; set; }

    /// <summary>
    /// Moneda de la transacción.
    /// </summary>
    /// <example>PEN</example>
    public string Moneda { get; set; } = null!;

    /// <summary>
    /// Tipo de cambio aplicado (si aplica).
    /// </summary>
    /// <example>3.75</example>
    public decimal TipoCambio { get; set; }

    /// <summary>
    /// Glosa o descripción.
    /// </summary>
    /// <example>Pago de servicios</example>
    public string? Glosa { get; set; }

    /// <summary>
    /// Estado: "completada", "pendiente", "rechazada".
    /// </summary>
    /// <example>completada</example>
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Referencia del QR si fue un pago con QR.
    /// </summary>
    /// <example>qr_hash_abc123</example>
    public string? ReferenciaQr { get; set; }

    /// <summary>
    /// Fecha y hora de la transacción (formato ISO).
    /// </summary>
    /// <example>2025-03-15T14:30:00Z</example>
    public string? FechaHora { get; set; }
}