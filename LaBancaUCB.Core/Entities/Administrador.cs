using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Administrador : BaseEntity
{
    public override long Id
    {
        get => IdAdministrador;
        set => IdAdministrador = value;
    }

    public long IdAdministrador { get; set; }
    public long IdUsuario { get; set; }
    public string NivelAcceso { get; set; } = null!;
    public string Departamento { get; set; } = null!;
    public bool Activo { get; set; }
    public DateTime AsignadoEn { get; set; }
}
