namespace LaBancaUCB.Core.Entities;

public partial class Beneficiario
{
    public long IdBeneficiario { get; set; }

    public long IdUsuarioOwner { get; set; }

    public string Alias { get; set; } = null!;

    public string NumeroCuentaDestino { get; set; } = null!;

    public string? BancoDestino { get; set; }

    public string NombreTitular { get; set; } = null!;

    public bool EsExterior { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual Usuario IdUsuarioOwnerNavigation { get; set; } = null!;
}
