using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Solicitud : BaseEntity
{
    public override long Id { get => IdSolicitud; set => IdSolicitud = value; }

    public long IdSolicitud { get; set; }
    public long IdUsuario { get; set; }
    public string TipoSolicitud { get; set; } = null!;
    public long ReferenciaId { get; set; }
    public string Estado { get; set; } = null!;
    public long? IdAdmin { get; set; }
    public string? ObservacionAdmin { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? GestionadaEn { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}