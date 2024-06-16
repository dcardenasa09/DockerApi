using FluentValidation;

namespace Base.API.Application.Users.Domain.DTO.Validators;

public class UserValidator : AbstractValidator<UserDTO> {

    public UserValidator() {
    
        RuleFor(e => e.Name).NotEmpty().WithMessage("El campo Nombre no debe de estar vacio.");
        RuleFor(e => e.LastName).NotEmpty().WithMessage("El campo Apellido no debe de estar vacio.");

        RuleFor(e => e.Email).NotEmpty().WithMessage("El campo Correo Electrónico no debe de estar vacio.")
                             .EmailAddress().WithMessage("El formato del campo Correo Electrónico es incorrecto.");

        RuleFor(e => e.Password).NotEmpty().WithMessage("El password es obligatorio.")
                                .MinimumLength(8).WithMessage("El password debe de tener al menos 8 caractéres.")
                                .Matches("[A-Z]").WithMessage("El password debe de tener al menos una letra mayúscula.")
                                .Matches("[0-9]").WithMessage("El password debe de tener al menos un número.")
                                .Matches("[^a-zA-Z0-9]").WithMessage("El password debe de tener al menos un caractér especial.");
    }
}