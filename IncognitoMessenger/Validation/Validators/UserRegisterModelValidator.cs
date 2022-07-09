using Business.Repositories;
using FluentValidation;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Validation.Validators;

public class UserRegisterModelValidator: AbstractValidator<UserRegisterModel>
{
    public UserRegisterModelValidator(IUserRepository userRepository)
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50)
            .Must(login => userRepository.GetByLogin(login) == null)
            .WithMessage("A user with this login already exists!");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);

        RuleFor(user => user.Pseudonym)
            .NotEmpty()
            .MaximumLength(50);
    }
}