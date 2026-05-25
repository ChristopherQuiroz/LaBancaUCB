using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class TransferenciaExteriorDtoValidator : AbstractValidator<TransferenciaExteriorDto>
{
    public TransferenciaExteriorDtoValidator()
    {
        RuleFor(x => x.CuentaOrigenId).GreaterThan(0);

        RuleFor(x => x.Monto)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0")
            .LessThanOrEqualTo(1000000).WithMessage("El monto excede el límite permitido");

        RuleFor(x => x.MonedaOrigen).NotEmpty();
        RuleFor(x => x.MonedaDestino).NotEmpty();
        RuleFor(x => x.CuentaDestino).NotEmpty();
        RuleFor(x => x.BancoDestino).NotEmpty();
        RuleFor(x => x.PaisDestino).NotEmpty();
        RuleFor(x => x.Referencia).MaximumLength(200);
    }
}
