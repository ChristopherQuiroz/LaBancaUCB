namespace LaBancaUCB.Core.DTOs;

public class UsuarioQueryFilter
{
    public string? Rol { get; set; }
    public bool? Activo { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}