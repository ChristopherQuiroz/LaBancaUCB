namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Representación simplificada de una transacción para el historial.
/// </summary>
public class TransactionRequestDto
{
    /// <summary>
    /// Identificador de la transacción.
    /// </summary>
    /// <example>6001</example>
    public string TransactionId { get; set; } = null!;

    /// <summary>
    /// Tipo: "transferencia", "pago_qr", etc.
    /// </summary>
    /// <example>transferencia</example>
    public string TransactionType { get; set; } = null!;

    /// <summary>
    /// Nombre del destino.
    /// </summary>
    /// <example>Carlos López</example>
    public string NombreDestino { get; set; } = null!;

    /// <summary>
    /// Monto.
    /// </summary>
    /// <example>150.00</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// Fecha de la transacción.
    /// </summary>
    /// <example>2025-03-15T10:00:00Z</example>
    public string FechaTransaccion { get; set; } = null!;
}