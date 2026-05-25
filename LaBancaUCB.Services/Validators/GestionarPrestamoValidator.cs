using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class GestionarPrestamoValidator : AbstractValidator<GestionarPrestamoDto>
{
    public GestionarPrestamoValidator()
    {
        RuleFor(x => x.MontoAprobado)
            .GreaterThanOrEqualTo(0).WithMessage("El monto aprobado no puede ser un valor negativo.");

        RuleFor(x => x.TasaInteres)
            .GreaterThan(0).WithMessage("La tasa de interés debe ser mayor a cero.");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("El estado es obligatorio.")
            .Must(e => e == "aprobado" || e == "rechazado" || e == "mora" || e == "pagado")
            .WithMessage("El estado provisto no es un estado de préstamo válido.");
    }
}