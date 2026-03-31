using FluentValidation;
using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Validators;

public class ActualizarCuentaValidator : AbstractValidator<CuentaDto>
{
    public ActualizarCuentaValidator()
    {
        RuleFor(c => c.IdCuenta)
            .GreaterThan(0).WithMessage("El IdCuenta debe ser mayor que 0.");
        RuleFor(c => c.NumeroCuenta)
            .NotEmpty().WithMessage("El NumeroCuenta es obligatorio.")
            .MaximumLength(20).WithMessage("El NumeroCuenta no puede exceder los 20 caracteres.");
        RuleFor(c => c.TipoCuenta)
            .NotEmpty().WithMessage("El TipoCuenta es obligatorio.")
            .MaximumLength(50).WithMessage("El TipoCuenta no puede exceder los 50 caracteres.");
        RuleFor(c => c.Saldo)
            .GreaterThanOrEqualTo(0).WithMessage("El Saldo debe ser mayor o igual a 0.");
        RuleFor(c => c.Moneda)
            .NotEmpty().WithMessage("La Moneda es obligatoria.")
            .MaximumLength(10).WithMessage("La Moneda no puede exceder los 10 caracteres.");
        RuleFor(c => c.Estado)
            .NotEmpty().WithMessage("El Estado es obligatorio.")
            .MaximumLength(20).WithMessage("El Estado no puede exceder los 20 caracteres.");
        RuleFor(c => c.FechaApertura)
            .NotEmpty().WithMessage("La FechaApertura es obligatoria.")
            .Must(BeAValidDate).WithMessage("La FechaApertura debe ser una fecha válida.");
    }
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}
