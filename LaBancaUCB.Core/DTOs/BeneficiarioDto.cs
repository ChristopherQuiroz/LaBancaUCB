namespace LaBancaUCB.Core.DTOs;
public class BeneficiarioDto
{
    public long IdBeneficiario { get; set; }
    public long IdUsuarioOwner { get; set; }
    public string Alias { get; set; }
    public string NumeroCuentaDestino { get; set; }
    public string BancoDestino { get; set; }
    public string NombreTitular { get; set; }
    public bool EsExterior { get; set; }
    public string? CreadoEn { get; set; }
}
