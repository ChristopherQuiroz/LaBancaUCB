using System;

namespace LaBancaUCB.Core.DTOs;

public class SeguroDto
{
    public long IdSeguro { get; set; }
    public long IdCuenta { get; set; }
    public string TipoSeguro { get; set; } = null!;
    public decimal PrimaMensual { get; set; }
    public decimal Cobertura { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}
