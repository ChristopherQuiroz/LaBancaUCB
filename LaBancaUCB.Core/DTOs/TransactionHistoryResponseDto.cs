namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Respuesta con el historial de transacciones.
/// </summary>
public class TransactionHistoryResponseDto
{
    /// <summary>
    /// Lista de transacciones.
    /// </summary>
    public List<TransactionRequestDto> TransactionHistory { get; set; } = new();
}