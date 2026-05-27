namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para procesar un pago mediante código QR.
/// </summary>
public class ProcesarPagoQrDto
{
    /// <summary>
    /// Cuenta de origen desde la que se debita el dinero.
    /// </summary>
    /// <example>5002</example>
    public long IdCuentaOrigen { get; set; }

    /// <summary>
    /// Hash único del QR escaneado.
    /// </summary>
    /// <example>a1b2c3d4e5f6...</example>
    public string CodigoHash { get; set; } = null!;

    /// <summary>
    /// Monto a pagar (requerido si el QR tiene monto variable).
    /// </summary>
    /// <example>250.00</example>
    public decimal? MontoIngresado { get; set; }
}