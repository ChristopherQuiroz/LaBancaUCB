using FluentValidation;
using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Validators;

public class TransactionRequestValidator : AbstractValidator<TransactionRequestDto>
{
    public TransactionRequestValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("TransactionId is required.");
        RuleFor(x => x.TransactionType)
            .NotEmpty().WithMessage("TransactionType is required.");
        RuleFor(x => x.NombreDestino)
            .NotEmpty().WithMessage("NombreDestino is required.");
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(x => x.FechaTransaccion)
            .NotEmpty().WithMessage("FechaTransaccion is required.")
            .Must(BeAValidDate).WithMessage("FechaTransaccion must be a valid date.");
    }
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}
