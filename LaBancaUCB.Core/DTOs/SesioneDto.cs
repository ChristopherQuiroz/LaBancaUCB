namespace LaBancaUCB.Core.DTOs;

public class SesioneDto
{
    public long IdSesion { get; set; }
    public long IdUsuario { get; set; }
    public string TokenJti { get; set; }
    public string IpOrigen { get; set; }
    public bool Activo { get; set; }
    public string ExpiradoEn { get; set; }
    public string? CreadoEn { get; set; }
}
