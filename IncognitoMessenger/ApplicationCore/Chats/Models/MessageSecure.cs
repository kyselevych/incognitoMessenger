using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.ApplicationCore.Chats.Models
{
    public class MessageSecure
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public int ChatId { get; set; }

        public UserSecure User { get; set; } = null!;
    }
}
