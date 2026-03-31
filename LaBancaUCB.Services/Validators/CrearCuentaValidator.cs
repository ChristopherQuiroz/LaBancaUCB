using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class CrearCuentaValidator : AbstractValidator<CuentaDto>
{
    public CrearCuentaValidator()
    {
        RuleFor(c => c.NumeroCuenta)
            .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
            .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres.");

        RuleFor(c => c.TipoCuenta)
            .NotEmpty().WithMessage("El tipo de cuenta es obligatorio.")
            .Must(tipo => tipo == "Ahorros" || tipo == "Corriente")
            .WithMessage("El tipo de cuenta debe ser 'Ahorros' o 'Corriente'.");

        RuleFor(c => c.Saldo)
            .GreaterThanOrEqualTo(0).WithMessage("El saldo no puede ser negativo.");

        RuleFor(c => c.Moneda)
            .NotEmpty().WithMessage("La moneda es obligatoria.")
            .Must(moneda => moneda == "USD" || moneda == "EUR" || moneda == "BOB")
            .WithMessage("La moneda debe ser 'USD', 'EUR' o 'BOB'.");

        RuleFor(c => c.Estado)
            .NotEmpty().WithMessage("El estado es obligatorio.")
            .Must(estado => estado == "Activo" || estado == "Inactivo")
            .WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
    }
}


