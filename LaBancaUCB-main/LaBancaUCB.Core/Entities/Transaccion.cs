using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Entities;

public partial class Transaccion : BaseEntity
{
    public override long Id
    {
        get => IdTransaccion;
        set => IdTransaccion = value;
    }

    public long IdTransaccion { get; set; }
    public long IdCuentaOrigen { get; set; }
    public string IdCuentaDestino { get; set; } = null!;
    public string NombreDestino { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public decimal Monto { get; set; }
    public string Moneda { get; set; } = null!;
    public decimal TipoCambio { get; set; }
    public string? Glosa { get; set; }
    public string Estado { get; set; } = null!;
    public string? ReferenciaQr { get; set; }
    public DateTime FechaHora { get; set; }
    public long? IdBeneficiario { get; set; }

    public virtual Cuenta IdCuentaOrigenNavigation { get; set; } = null!;
    public virtual Beneficiario? IdBeneficiarioNavigation { get; set; }
}