using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class ActualizarUsuarioValidator : AbstractValidator<UsuarioDto>
{
    public ActualizarUsuarioValidator()
    {
        RuleFor(x => x.IdUsuario)
            .GreaterThan(0).WithMessage("El ID del usuario es obligatorio y debe ser mayor a cero")
            .NotEmpty().WithMessage("El ID del usuario no puede estar vacío");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no tiene un formato válido")
            .MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es obligatorio")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres")
            .MaximumLength(200).WithMessage("El nombre no puede exceder los 200 caracteres");

        RuleFor(x => x.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio")
            .Must(r => r == "cliente" || r == "admin")
            .WithMessage("El rol debe ser 'cliente' o 'admin'");
    }
}
