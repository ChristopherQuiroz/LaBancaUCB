namespace LaBancaUCB.Core.DTOs;

public class AdminManageSolicitudDto
{
    public string Estado { get; set; } = null!;
    public string? ObservacionAdmin { get; set; }
    public decimal? MontoAprobado { get; set; }
}
