using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Prestamo : BaseEntity
{
    public override long Id
    {
        get => IdPrestamo;
        set => IdPrestamo = value;
    }

    public long IdPrestamo { get; set; }
    public long IdCuenta { get; set; }
    public decimal MontoSolicitado { get; set; }
    public decimal MontoAprobado { get; set; }
    public decimal TasaInteres { get; set; }
    public int plazoMeses { get; set; }
    public string estado { get; set; } = null!;
    public DateTime solicitadoEn { get; set; }
    public DateTime? aprobadoEn { get; set; }
}
