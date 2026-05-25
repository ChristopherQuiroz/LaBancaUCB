using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public class AuditoriaCuenta : BaseEntity
{
    public override long Id
    {
        get => IdAuditoriaCuenta;
        set => IdAuditoriaCuenta = value;
    }

    public long IdAuditoriaCuenta { get; set; }
    public long IdCuenta { get; set; }
    public long IdAdministrador { get; set; }
    public string Accion { get; set; } = null!;
    public string Motivo { get; set; } = null!;
    public string? IpOrigen { get; set; }
    public DateTime EjecutadoEn { get; set; }
}