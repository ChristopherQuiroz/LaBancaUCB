namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Información de un beneficiario registrado por un usuario para realizar transferencias frecuentes.
/// </summary>
public class BeneficiarioDto
{
    /// <summary>
    /// Identificador único del beneficiario.
    /// </summary>
    /// <example>202</example>
    public long IdBeneficiario { get; set; }

    /// <summary>
    /// Identificador del usuario propietario (quien registró al beneficiario).
    /// </summary>
    /// <example>101</example>
    public long IdUsuarioOwner { get; set; }

    /// <summary>
    /// Alias opcional que el usuario asigna al beneficiario.
    /// </summary>
    /// <example>Hermano</example>
    public string? Alias { get; set; }

    /// <summary>
    /// Número de cuenta de destino del beneficiario.
    /// </summary>
    /// <example>000123456789</example>
    public string NumeroCuentaDestino { get; set; } = null!;

    /// <summary>
    /// Nombre del banco destino.
    /// </summary>
    /// <example>Banco de Crédito</example>
    public string? BancoDestino { get; set; }

    /// <summary>
    /// Nombre completo del titular de la cuenta destino.
    /// </summary>
    /// <example>María López</example>
    public string NombreTitular { get; set; } = null!;

    /// <summary>
    /// Indica si la cuenta destino es en el extranjero.
    /// </summary>
    /// <example>false</example>
    public bool EsExterior { get; set; }

    /// <summary>
    /// Fecha de creación del beneficiario (formato ISO).
    /// </summary>
    /// <example>2025-03-20T14:30:00Z</example>
    public string? CreadoEn { get; set; }
}