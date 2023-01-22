namespace IncognitoMessenger.ApplicationCore.Auth.Models
{
    public class AccessToken
    {
        public string Key { get; set; } = string.Empty;

        public int ExpiryTime { get; set; }
    }
}
