using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class CodigoQr : BaseEntity
{
    public override long Id { get => IdQr; set => IdQr = value; }

    public long IdQr { get; set; }
    public long IdCuentaReceptora { get; set; }
    public string CodigoHash { get; set; } = null!;
    public decimal? MontoFijo { get; set; }
    public bool EsMontoVariable { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
    public DateTime ExpiraEn { get; set; }
    public DateTime CreadoEn { get; set; }

    public virtual Cuenta IdCuentaReceptoraNavigation { get; set; } = null!;
}