using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Tarjeta : BaseEntity
{
    public override long Id { get => IdTarjeta; set => IdTarjeta = value; }

    public long IdTarjeta { get; set; }
    public long IdCuenta { get; set; }
    public string Tipo { get; set; } = null!;
    public string NumeroEnmascarado { get; set; } = null!;
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
}