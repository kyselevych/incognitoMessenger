namespace IncognitoMessenger.ApplicationCore.Chats.Models
{
    public class SendMessageModel
    {
        public int ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
