using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public class AuditoriaCuenta
{
    public long IdAuditoriaCuenta { get; set; }
    public long IdCuenta { get; set; }
    public long IdAdministrador { get; set; }
    public string Accion { get; set; } = null!;
    public string Motivo { get; set; } = null!;
    public long IpOrigen { get; set; }
    public DateTime EjecutadoEn { get; set; }
}
