using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class EstadoCuentaValidator : AbstractValidator<EstadoCuentaDto>
{
    public EstadoCuentaValidator()
    {
        RuleFor(x => x.Accion)
            .NotEmpty().WithMessage("La acción no puede estar vacía.")
            .Must(a => a == "bloquear" || a == "desbloquear")
            .WithMessage("La acción debe ser 'bloquear' o 'desbloquear'.");

        RuleFor(x => x.Motivo)
            .NotEmpty().WithMessage("El motivo es obligatorio para fines de auditoría.");
    }
}
