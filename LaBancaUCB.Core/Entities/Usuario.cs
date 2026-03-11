namespace LaBancaUCB.Core.Entities;

public partial class Usuario
{
    public long IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool? Activo { get; set; }

    public bool Bloqueado { get; set; }

    public DateTime FechaDeCreacion { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public virtual ICollection<Beneficiario> Beneficiarios { get; set; } = new List<Beneficiario>();

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Sesione> Sesiones { get; set; } = new List<Sesione>();
}
