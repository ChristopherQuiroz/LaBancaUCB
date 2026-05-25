using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

public class CrearSeguroDto
{
    public long IdCuenta { get; set; }
    public string TipoSeguro { get; set; } = null!;
    public decimal PrimaMensual { get; set; }
    public decimal Cobertura { get; set; }
}

public class CrearPrestamoDto
{
    public long IdCuenta { get; set; }
    public decimal MontoSolicitado { get; set; }
    public decimal TasaInteres { get; set; }
    public int PlazoMeses { get; set; }
}

public class SolicitarTarjetaDto
{
    public long IdCuenta { get; set; }
    public string Tipo { get; set; } = null!; 
}

public class GestionarSolicitudDto
{
    public string Estado { get; set; } = null!; 
    public string? ObservacionAdmin { get; set; }
}