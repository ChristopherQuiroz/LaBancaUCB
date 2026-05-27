namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// Datos para cambiar el estado de una cuenta (bloquear, activar, suspender).
/// </summary>
public class EstadoCuentaDto
{
    /// <summary>
    /// Acción a realizar: "bloquear", "activar", "suspender".
    /// </summary>
    /// <example>bloquear</example>
    public string Accion { get; set; } = null!;

    /// <summary>
    /// Motivo del cambio de estado (obligatorio si la acción es bloquear o suspender).
    /// </summary>
    /// <example>Fraude detectado</example>
    public string Motivo { get; set; } = null!;
}