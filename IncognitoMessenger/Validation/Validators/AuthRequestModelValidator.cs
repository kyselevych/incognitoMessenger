using FluentValidation;
using IncognitoMessenger.Models.Auth;

namespace IncognitoMessenger.Validation.Validators;

public class AuthRequestModelValidator : AbstractValidator<AuthRequestModel>
{
    public AuthRequestModelValidator()
    {
        RuleFor(model => model.AccessToken)
            .NotEmpty();

        RuleFor(model => model.RefreshToken)
            .NotEmpty();
    }
}