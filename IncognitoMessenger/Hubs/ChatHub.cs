using Business.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IncognitoMessenger.Hubs
{
    
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.Group(message.ChatId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public Task Join(string chatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public Task Disconnect(string chatId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
