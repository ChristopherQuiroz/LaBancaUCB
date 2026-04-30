using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Prestamo : BaseEntity
{
    public override long Id { get => IdPrestamo; set => IdPrestamo = value; }

    public long IdPrestamo { get; set; }
    public long IdCuenta { get; set; }
    public decimal MontoSolicitado { get; set; }
    public decimal MontoAprobado { get; set; }
    public decimal TasaInteres { get; set; }
    public int PlazoMeses { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime SolicitadoEn { get; set; }
    public DateTime? AprobadoEn { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
}
