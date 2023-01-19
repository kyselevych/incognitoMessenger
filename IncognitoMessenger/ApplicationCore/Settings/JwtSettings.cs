namespace IncognitoMessenger.Infrastructure.Settings
{
    public class JwtSettings
    {
        public static string SectionName = "JwtSettings";

        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpiryTimeInMinutes { get; set; }
        public int RefreshTokenExpiryTimeInDays { get; set; }
    }
}
