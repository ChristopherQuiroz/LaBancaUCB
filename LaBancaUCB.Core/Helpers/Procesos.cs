using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace LaBancaUCB.Core.Helpers;

public static class Procesos
{
    public static string? ParseFechaFlexible(string? fechaTexto)
    {
        if (string.IsNullOrWhiteSpace(fechaTexto)) return null;

        string[] formatos = {
            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "yyyy-MM-dd",
            "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd", "MM/dd/yyyy"
        };

        if (DateTime.TryParseExact(fechaTexto, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaValida))
        {
            return fechaValida.ToString("yyyy-MM-dd");
        }

        return null; 
    }
}
