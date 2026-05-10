using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class BeneficiarioDtoValidator : AbstractValidator<BeneficiarioDto>
{
    public BeneficiarioDtoValidator()
    {
        RuleFor(x => x.Alias)
            .NotEmpty().WithMessage("El alias es requerido")
            .MaximumLength(100);

        RuleFor(x => x.NumeroCuentaDestino)
            .NotEmpty().WithMessage("El número de cuenta es requerido")
            .Length(6, 34).WithMessage("El número de cuenta debe tener entre 6 y 34 caracteres");

        RuleFor(x => x.NombreTitular)
            .NotEmpty().WithMessage("El nombre del titular es requerido")
            .MaximumLength(200);

        RuleFor(x => x.BancoDestino)
            .NotEmpty().WithMessage("El banco destino es requerido cuando no es exterior")
            .When(x => !x.EsExterior);
    }
}
