namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para solicitar una tarjeta (similar a SolicitarTarjetaDto, usado en otro endpoint).
/// </summary>
public class TarjetaRequestDto
{
    /// <summary>
    /// Cuenta asociada.
    /// </summary>
    /// <example>5001</example>
    public long IdCuenta { get; set; }

    /// <summary>
    /// Tipo: "debito" o "credito".
    /// </summary>
    /// <example>credito</example>
    public string Tipo { get; set; } = null!;
}