using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public class TarjetaCredito
{
    public long IdTarjetaCredito { get; set; }
    public long IdTarjeta { get; set; }
    public string NumeroTarjeta { get; set; } = null!;
    public decimal limiteCredito { get; set; }
    public decimal saldoUtilizado { get; set; }
    public decimal tasaInteres { get; set; }
    public DateOnly fechaCorte { get; set; }
    public DateOnly fechaPago { get; set; }
    public virtual Tarjeta IdTarjetaNavigation { get; set; } = null!;
}
