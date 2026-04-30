using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class CrearBeneficiarioValidator : AbstractValidator<BeneficiarioDto>
{
    public CrearBeneficiarioValidator()
    {
        RuleFor(x => x.Alias).NotEmpty().MaximumLength(100);
        RuleFor(x => x.NumeroCuentaDestino).NotEmpty().MaximumLength(20);
        RuleFor(x => x.NombreTitular).NotEmpty().MaximumLength(200);
    }
}

