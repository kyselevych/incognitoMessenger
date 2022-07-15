using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.Models.Auth;

public class AuthResponseModel
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public UserAuthResponseModel User { get; set; } = new UserAuthResponseModel();
}