using FluentValidation;

namespace Base.API.Application.Transactions.Domain.DTO;

public class TransactionValidator : AbstractValidator<TransactionDTO> {
    public TransactionValidator() {
        RuleFor(x => x.SavingAccountId).NotEmpty().WithMessage("Se debe seleccionar una 'Cuenta de Ahorro' para poder continuar.");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Se debe seleccionar un 'Tipo de movimiento' para poder continuar.");
    }
}