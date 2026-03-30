using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no tiene un formato válido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria");
    }
}
