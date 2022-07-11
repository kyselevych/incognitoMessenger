using Business.Entities;

namespace IncognitoMessenger.Services.Token;

public interface ITokenService
{
    string BuildToken(UserModel user);
}