using FluentValidation;
using LaBancaUCB.Core.DTOs;
namespace LaBancaUCB.Services.Validators;

public class TransactionHistoryValidator
{
    public class TransactionHistoryRequestValidator : AbstractValidator<TransactionHistoryResponseDto>
    {
        public TransactionHistoryRequestValidator()
        {
            RuleFor(x => x.TransactionHistory)
                .NotEmpty().WithMessage("TransactionHistory is required.")
                .ForEach(transaction =>
                {
                    transaction.SetValidator(new TransactionRequestValidator());
                });
        }
    }
}
