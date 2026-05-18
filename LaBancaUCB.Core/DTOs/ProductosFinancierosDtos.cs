using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

/// <summary>
/// DTO utilizado para crear diferentes tipos de seguros para ofrecer al cliente.
/// </summary>
/// <remarks>
/// 
/// </remarks>
public class CrearSeguroDto
{
    public long IdCuenta { get; set; }
    public string TipoSeguro { get; set; } = null!;
    public decimal PrimaMensual { get; set; }
    public decimal Cobertura { get; set; }
}

/// <summary>
/// DTO utilizado para crear un prestamo por parte del administrador.
/// </summary>
/// <remarks>
/// 
/// </remarks>
public class CrearPrestamoDto
{
    public long IdCuenta { get; set; }
    public decimal MontoSolicitado { get; set; }
    public decimal TasaInteres { get; set; }
    public int PlazoMeses { get; set; }
}

/// <summary>
/// DTO utilizado para enviar la solicitud de creacion de tarjeta de credito.
/// </summary>
/// <remarks>
/// 
/// </remarks>
public class SolicitarTarjetaDto
{
    public long IdCuenta { get; set; }
    public string Tipo { get; set; } = null!; 
}

/// <summary>
/// DTO utilizado para enviar la acción de gestión (aprobar/rechazar) sobre una solicitud.
/// </summary>
/// <remarks>
/// El administrador envía este objeto en el cuerpo de la petición PUT.
/// </remarks>
public class GestionarSolicitudDto
{
    /// <summary>
    /// Nuevo estado de la solicitud: "aprobado" o "rechazado".
    /// </summary>
    /// <example>aprobado</example>
    [SwaggerSchema("Estado a establecer ", Nullable = false)]
    public string Estado { get; set; } = null!;

    /// <summary>
    /// Mensaje del administrador que menciona el por que del estado gestionado
    /// </summary>
    /// <example>Su solicitud ha sido aprobada exitosamente</example>
    [SwaggerSchema("")]
    public string? ObservacionAdmin { get; set; }
}