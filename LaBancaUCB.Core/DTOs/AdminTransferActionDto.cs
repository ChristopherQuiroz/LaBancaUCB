namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para que un administrador realice una acción sobre una transferencia (ej. aprobar, rechazar).
/// </summary>
public class AdminTransferActionDto
{
    /// <summary>
    /// Identificador de la transacción a gestionar.
    /// </summary>
    /// <example>10045</example>
    public long TransaccionId { get; set; }

    /// <summary>
    /// Motivo de la acción (obligatorio si es rechazo, opcional si es aprobación).
    /// </summary>
    /// <example>Fondos insuficientes en cuenta origen.</example>
    public string? Motivo { get; set; }
}