using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs;

public class ChangePasswordDto
{
    public string PasswordActual { get; set; } = null!;
    public string PasswordNueva { get; set; } = null!;
}