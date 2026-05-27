namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para realizar una transferencia internacional.
/// </summary>
public class TransferenciaExteriorDto
{
    /// <summary>
    /// Cuenta origen (id).
    /// </summary>
    /// <example>5001</example>
    public long CuentaOrigenId { get; set; }

    /// <summary>
    /// Monto a transferir.
    /// </summary>
    /// <example>1000.00</example>
    public decimal Monto { get; set; }

    /// <summary>
    /// Moneda de origen (ej. PEN).
    /// </summary>
    /// <example>PEN</example>
    public string MonedaOrigen { get; set; } = null!;

    /// <summary>
    /// Moneda de destino (ej. USD).
    /// </summary>
    /// <example>USD</example>
    public string MonedaDestino { get; set; } = null!;

    /// <summary>
    /// Número de cuenta destino (internacional).
    /// </summary>
    /// <example>CH1234567890123456789</example>
    public string CuentaDestino { get; set; } = null!;

    /// <summary>
    /// Nombre del banco destino.
    /// </summary>
    /// <example>Bank of America</example>
    public string BancoDestino { get; set; } = null!;

    /// <summary>
    /// País destino (código ISO 3166-1 alpha-2).
    /// </summary>
    /// <example>US</example>
    public string PaisDestino { get; set; } = null!;

    /// <summary>
    /// Referencia o concepto.
    /// </summary>
    /// <example>Envío familiar</example>
    public string Referencia { get; set; } = null!;

    /// <summary>
    /// Fecha programada (opcional, si es futura).
    /// </summary>
    /// <example>2025-04-01T00:00:00Z</example>
    public DateTime? FechaProgramada { get; set; }
}