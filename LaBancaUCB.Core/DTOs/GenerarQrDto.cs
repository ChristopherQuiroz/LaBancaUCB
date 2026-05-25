namespace LaBancaUCB.Core.DTOs;

public class GenerarQrDto
{
    public long IdCuentaReceptora { get; set; }
    public decimal? MontoFijo { get; set; }
    public bool EsMontoVariable { get; set; }
    public string? Descripcion { get; set; }
}
