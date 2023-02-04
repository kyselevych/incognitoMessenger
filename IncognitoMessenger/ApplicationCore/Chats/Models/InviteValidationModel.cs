namespace IncognitoMessenger.ApplicationCore.Chats.Models
{
    public class InviteValidationModel
    {
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
