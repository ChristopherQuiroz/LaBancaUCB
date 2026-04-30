using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

public class CuentaDto
{
    public long IdCuenta { get; set; }
    public string NumeroCuenta { get; set; } = null!;
    public string TipoCuenta { get; set; } = null!;
    public decimal Saldo { get; set; }
    public string Moneda { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string? FechaApertura { get; set; }
}
