namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para generar un código QR asociado a una cuenta para recibir pagos.
/// </summary>
public class GenerarQrDto
{
    /// <summary>
    /// Identificador de la cuenta que recibirá el pago.
    /// </summary>
    /// <example>5001</example>
    public long IdCuentaReceptora { get; set; }

    /// <summary>
    /// Monto fijo solicitado (si es variable, dejar nulo y marcar <see cref="EsMontoVariable"/> como true).
    /// </summary>
    /// <example>150.00</example>
    public decimal? MontoFijo { get; set; }

    /// <summary>
    /// Indica si el monto puede ser definido por el pagador al momento del pago.
    /// </summary>
    /// <example>true</example>
    public bool EsMontoVariable { get; set; }

    /// <summary>
    /// Descripción opcional del pago (ej. "Pago de servicios").
    /// </summary>
    /// <example>Factura luz marzo</example>
    public string? Descripcion { get; set; }
}