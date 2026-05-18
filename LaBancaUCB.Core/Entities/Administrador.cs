using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

/// <summary>
/// Representa la entidad Administrador en el sistema
/// </summary>
/// <remarks>
/// Esta entidad almacena la información principal del Administrador 
/// </remarks>
public partial class Administrador : BaseEntity
{
    /// <summary>
    /// Identificador único del Administrador
    /// </summary>
    /// <example>1</example>
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
