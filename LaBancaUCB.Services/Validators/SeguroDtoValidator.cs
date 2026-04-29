using FluentValidation;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Validators;

public class SeguroDtoValidator : AbstractValidator<SeguroDto>
{
    public SeguroDtoValidator()
    {
        RuleFor(x => x.IdCuenta)
            .GreaterThan(0).WithMessage("La cuenta es requerida");

        RuleFor(x => x.TipoSeguro)
            .NotEmpty().WithMessage("El tipo de seguro es requerido");

        RuleFor(x => x.PrimaMensual)
            .GreaterThanOrEqualTo(0).WithMessage("La prima mensual debe ser >= 0");

        RuleFor(x => x.Cobertura)
            .GreaterThan(0).WithMessage("La cobertura debe ser mayor que 0");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("El estado es requerido");

        RuleFor(x => x.FechaFin)
            .GreaterThan(x => x.FechaInicio).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio");
    }
}
