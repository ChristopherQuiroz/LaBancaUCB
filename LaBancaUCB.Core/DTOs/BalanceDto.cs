namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Representa el balance de una cuenta, incluyendo datos del titular y la cuenta.
/// </summary>
public class BalanceDto
{
    /// <summary>
    /// Identificador del usuario titular.
    /// </summary>
    /// <example>101</example>
    public long IdUsuario { get; set; }

    /// <summary>
    /// Identificador de la cuenta.
    /// </summary>
    /// <example>5001</example>
    public long IdCuenta { get; set; }

    /// <summary>
    /// Nombre completo del titular.
    /// </summary>
    /// <example>Juan Pérez Gómez</example>
    public string NombreCompleto { get; set; } = null!;

    /// <summary>
    /// Saldo actual de la cuenta.
    /// </summary>
    /// <example>12500.75</example>
    public decimal Balance { get; set; }
}