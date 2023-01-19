using Business.Entities;
using FluentValidation;
using MssqlDatabase.Repositories;

namespace IncognitoMessenger.Validation.Validators;

public class UserRegisterValidator: AbstractValidator<User>
{
    public UserRegisterValidator(UserRepository userRepository)
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50)
            .Must(login => userRepository.GetByLogin(login) == null)
            .WithMessage("A user with this login already exists!");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(50);

        RuleFor(user => user.Nickname)
            .NotEmpty()
            .MaximumLength(50);
    }
}