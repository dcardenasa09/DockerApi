using FluentValidation;

namespace Base.API.Application.SavingAccounts.Domain.DTO;

public class SavingAccountValidator : AbstractValidator<SavingAccountDTO> {
    public SavingAccountValidator() {
        RuleFor(user => user.AccountNumber).NotEmpty().WithMessage("El campo 'AccountNumber' no puede estar vacío.");
        RuleFor(user => user.ClientId).NotEmpty().WithMessage("Se debe seleccionar un 'Cliente'.")
                                      .GreaterThan(0).WithMessage("Se debe seleccionar un 'Cliente'.");
    }
}