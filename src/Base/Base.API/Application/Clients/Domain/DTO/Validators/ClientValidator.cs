using FluentValidation;

namespace Base.API.Application.Clients.Domain.DTO;

public class ClientValidator : AbstractValidator<ClientDTO> {
    public ClientValidator() {
        RuleFor(user => user.Name).NotEmpty().WithMessage("El campo 'Nombre' no puede estar vacío.");
        RuleFor(user => user.LastName).NotEmpty().WithMessage("El campo 'Appellido paterno' no puede estar vacío.");
        RuleFor(user => user.Phone).NotEmpty().WithMessage("El campo 'Teléfono' no puede estar vacío.")
                                   .Length(10).WithMessage("El campo 'Teléfono' ndebe tener exactamente 10 caracteres.");
        
        RuleFor(user => user.Email).NotEmpty().WithMessage("El campo 'Email' no puede estar vacío.")
                                   .EmailAddress().WithMessage("El formato del campo 'Email' es incorrecto.");
    }
}