using System;

namespace LaBancaUCB.Core.DTOs;

public class TransferenciaExteriorDto
{
    public long CuentaOrigenId { get; set; }
    public decimal Monto { get; set; }
    public string MonedaOrigen { get; set; } = null!;
    public string MonedaDestino { get; set; } = null!;
    public string CuentaDestino { get; set; } = null!;
    public string BancoDestino { get; set; } = null!;
    public string PaisDestino { get; set; } = null!;
    public string Referencia { get; set; } = null!;
    public DateTime? FechaProgramada { get; set; }
}
