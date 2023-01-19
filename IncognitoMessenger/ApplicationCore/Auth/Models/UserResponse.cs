namespace IncognitoMessenger.Models.User;

public class UserResponse
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Nickname { get; set; } = string.Empty;
}