namespace LaBancaUCB.Core.DTOs;

public class UsuarioDto
{
    public long IdUsuario { get; set; }
    public string Email { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string? PasswordHash { get; set; }
    public string Rol { get; set; } = null!;
    public bool? Activo { get; set; }
    public bool? Bloqueado { get; set; }
    public string? FechaDeCreacion { get; set; }
}