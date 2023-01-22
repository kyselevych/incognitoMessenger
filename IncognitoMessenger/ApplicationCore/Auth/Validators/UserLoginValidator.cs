using Business.Entities;
using FluentValidation;
using MssqlDatabase.Repositories;

namespace IncognitoMessenger.Validation.Validators;

public class UserLoginValidator : AbstractValidator<User>
{
    public UserLoginValidator(UserRepository userRepository)
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50)
            .Must(login => userRepository.GetByLogin(login) != null)
            .WithMessage("Login or password in invalid")
            .DependentRules(() =>
            {
                RuleFor(user => user.Password)
                    .NotEmpty()
                    .MinimumLength(6)
                    .MaximumLength(50)
                    .Must((user, password) =>
                    {
                        var userModel = userRepository.GetByLogin(user.Login);
                        return BCrypt.Net.BCrypt.Verify(password, userModel?.Password);
                    })
                    .WithMessage("Login or password in invalid");
            });

        
    }
}