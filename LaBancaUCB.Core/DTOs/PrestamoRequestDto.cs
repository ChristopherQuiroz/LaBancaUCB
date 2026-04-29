using System;

namespace LaBancaUCB.Core.DTOs;

public class PrestamoRequestDto
{
    public long IdCuenta { get; set; }
    public decimal MontoSolicitado { get; set; }
    public decimal TasaInteres { get; set; }
    public int PlazoMeses { get; set; }
}
