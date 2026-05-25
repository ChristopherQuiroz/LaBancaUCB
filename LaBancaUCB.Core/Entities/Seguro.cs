using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Seguro : BaseEntity
{
    public override long Id { get => IdSeguro; set => IdSeguro = value; }

    public long IdSeguro { get; set; }
    public long IdCuenta { get; set; }
    public string TipoSeguro { get; set; } = null!;
    public decimal PrimaMensual { get; set; }
    public decimal Cobertura { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
}
