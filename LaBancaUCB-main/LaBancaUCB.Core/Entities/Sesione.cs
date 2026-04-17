using System;
using System.Collections.Generic;

namespace LaBancaUCB.Core.Entities;

public partial class Sesione : BaseEntity
{
    public override long Id
    {
        get => IdSesion;
        set => IdSesion = value;
    }

    public long IdSesion { get; set; }
    public long IdUsuario { get; set; }
    public Guid TokenJti { get; set; }
    public string? IpOrigen { get; set; }
    public bool? Activo { get; set; }
    public DateTime ExpiradoEn { get; set; }
    public DateTime? CreadoEn { get; set; }
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}