using System;
using System.Collections.Generic;

namespace LaBancaUCB.Core.Entities;

public partial class Cuenta : BaseEntity
{
    public override long Id
    {
        get => IdCuenta;
        set => IdCuenta = value;
    }

    public long IdCuenta { get; set; }
    public long IdUsuario { get; set; }
    public string NumeroCuenta { get; set; } = null!;
    public string TipoCuenta { get; set; } = null!;
    public decimal Saldo { get; set; }
    public string Moneda { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public DateTime? FechaApertura { get; set; }
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    public virtual ICollection<Seguro> Seguros { get; set; } = new List<Seguro>();
}