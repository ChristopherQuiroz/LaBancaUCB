using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;
public partial class Tarjeta : BaseEntity
{
    public override long Id
    {
        get => IdTarjeta;
        set => IdTarjeta = value;
    }

    public long IdTarjeta { get; set; }
    public long IdCuenta { get; set; }
    public string tipo { get; set; } = null!;
    public string numeroEnmascarado { get; set; } = null!;
    public DateTime fechaVencimiento { get; set; }
    public string estado { get; set; } = null!;
    public DateTime fechaCreacion { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Beneficiario? Beneficiario { get; set; }

}
