using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

public class TransaccionQueryFilter
{
    public string? Estado { get; set; }
    public string? Tipo { get; set; }
    public string? Glosa { get; set; }
    public string? Fecha { get; set; }
}