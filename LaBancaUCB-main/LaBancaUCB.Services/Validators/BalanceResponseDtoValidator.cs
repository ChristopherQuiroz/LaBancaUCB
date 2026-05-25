using FluentValidation;
using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Validators;

public class BalanceResponseDtoValidator : AbstractValidator<BalanceDto>
{
    public BalanceResponseDtoValidator()
    {
        RuleFor(x => x.IdUsuario)
            .NotEmpty().WithMessage("El IdUsuario no puede estar vacío.")
            .Must(id => long.TryParse(id.ToString(), out _)).WithMessage("El IdUsuario debe ser un número válido.");
        RuleFor(x => x.IdCuenta)
            .NotEmpty().WithMessage("El IdCuenta no puede estar vacío.")
            .Must(id => long.TryParse(id.ToString(), out _)).WithMessage("El IdCuenta debe ser un número válido.");
        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El NombreCompleto no puede estar vacío.");
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("El Balance debe ser un número positivo o cero.");
    }

    public object IdUsuario { get; set; }
    public object IdCuenta { get; set; }
    public object NombreCompleto { get; set; }
}
