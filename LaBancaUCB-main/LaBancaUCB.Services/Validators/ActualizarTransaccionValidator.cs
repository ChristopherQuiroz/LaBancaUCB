using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class ActualizarTransaccionValidator : AbstractValidator<TransaccionDto>
{
    public ActualizarTransaccionValidator()
    {
        RuleFor(t => t.IdTransaccion)
            .GreaterThan(0).WithMessage("El ID de la transacción debe ser mayor que cero.");
        RuleFor(t => t.IdCuentaOrigen)
            .GreaterThan(0).WithMessage("El ID de la cuenta de origen debe ser mayor que cero.");
        RuleFor(t => t.IdCuentaDestino)
            .NotEmpty().WithMessage("El ID de la cuenta de destino es obligatorio.");
        RuleFor(t => t.Monto)
            .GreaterThan(0).WithMessage("El monto debe ser mayor que cero.");
        RuleFor(t => t.Moneda)
            .NotEmpty().WithMessage("La moneda es obligatoria.")
            .Must(moneda => moneda == "USD" || moneda == "EUR" || moneda == "BOB")
            .WithMessage("La moneda debe ser 'USD', 'EUR' o 'BOB'.");
        RuleFor(t => t.Tipo)
            .NotEmpty().WithMessage("El tipo de transacción es obligatorio.")
            .Must(tipo => tipo == "Transferencia" || tipo == "Depósito" || tipo == "Retiro")
            .WithMessage("El tipo de transacción debe ser 'Transferencia', 'Depósito' o 'Retiro'.");
        RuleFor(t => t.FechaHora)
            .NotEmpty().WithMessage("La fecha de la transacción es obligatoria.");
    }
}
