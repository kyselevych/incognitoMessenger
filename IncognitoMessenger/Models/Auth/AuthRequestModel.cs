namespace IncognitoMessenger.Models.Auth;

public class AuthRequestModel
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}