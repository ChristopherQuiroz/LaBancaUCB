namespace LaBancaUCB.Core.DTOs;

public class ProcesarPagoQrDto
{
    public long IdCuentaOrigen { get; set; }
    public string CodigoHash { get; set; } = null!;
    // Si el QR dice que el monto es variable, el cliente debe enviarlo aquí.
    public decimal? MontoIngresado { get; set; }
}
