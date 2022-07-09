using Business.Repositories;
using FluentValidation;
using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Validation.Validators;

public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
{
    public UserLoginModelValidator(IUserRepository userRepository)
    {
        RuleFor(user => user.Login)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50)
            .Must(login => userRepository.GetByLogin(login) != null)
            .WithMessage("Login is not found!")
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
                    .WithMessage("Password is invalid!");
            });

        
    }
}