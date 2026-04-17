namespace LaBancaUCB.Core.DTOs;

public class TransactionRequestDto
{
    public string TransactionId { get; set; }
    public string TransactionType { get; set; }
    public string NombreDestino { get; set; }
    public decimal Amount { get; set; }
    public string FechaTransaccion { get; set; }
}
