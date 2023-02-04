using Business.Entities;

namespace IncognitoMessenger.ApplicationCore.Chats.Models
{
    public class ChatCard
    {
        public Chat Chat { get; set; } = null!;

        public Message? LastMessage { get; set; }
    }
}
