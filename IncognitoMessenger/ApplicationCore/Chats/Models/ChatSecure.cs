using IncognitoMessenger.Models.User;

namespace IncognitoMessenger.ApplicationCore.Chats.Models
{
    public class ChatSecure
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int UserId { get; set; }

        public ICollection<MessageSecure> Messages { get; set; }

        public ICollection<UserSecure> Users { get; set; }
    }
}
