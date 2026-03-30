using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public DateTime Expiracion { get; set; }
}