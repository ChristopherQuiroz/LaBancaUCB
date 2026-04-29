using System;

namespace LaBancaUCB.Core.DTOs;

public class TarjetaRequestDto
{
    public long IdCuenta { get; set; }
    public string Tipo { get; set; } = null!; // 'debito' o 'credito'
}
